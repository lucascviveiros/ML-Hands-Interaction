using System;
using System.Collections.Generic;

namespace UnityEngine.XR.MagicLeap
{
    /// Automatically requests specified privileges. Exposes delegate for when the requests are done.
    public class PrivilegeRequester : MonoBehaviour
    {
        public enum PrivilegeState
        {
            //Requester has not started
            Off,

            //Failed to start because a privilege system failure
            StartFailed,

            //Requester has started requesting privileges
            Started,

            //All privileges have been requested. Waiting on results
            Requested,

            //All privileges were granted
            Succeeded,

            //One or more of the privileges were denied, or some other privilege failure request occured
            Failed
        }

        PrivilegeState _state = PrivilegeState.Off;

        /// Current state of the requester.    
        public PrivilegeState State
        {
            get { return _state; }
        }

        public event Action<MLResult> OnPrivilegesDone = delegate { };

        // TODO: Disable editor edit when state is StartFailed, Started, Requested, Failed or Succeeded states.
        [SerializeField] [Tooltip("Requested privileges. " +
            "Can also be modified via script using Privileges property. " +
            "Should only be changed in Editor mode. Changes with the component active will not be immediately reflected in behavior.")]
        MLRuntimeRequestPrivilegeId[] _privileges;

        /// Requested privileges. Should be set on Awake.
        public MLRuntimeRequestPrivilegeId[] Privileges
        {
            get { return _privileges; }
            set { _privileges = value; }
        }

        readonly List<MLPrivilegeId> _privilegesToRequest = new List<MLPrivilegeId>();
        readonly List<MLPrivilegeId> _privilegesGranted = new List<MLPrivilegeId>();

        #region Unity Methods

        /// Start the Privileges API and set the Privilege State
        void Start()
        {
            MLResult result = MLPrivileges.Start();
            if (result.IsOk)
            {
                _privilegesToRequest.AddRange(Array.ConvertAll(_privileges, tempPrivilege => (MLPrivilegeId)tempPrivilege));
                _state = PrivilegeState.Started;
            }
            else
            {
                Debug.LogErrorFormat("Error: PrivilegeRequester failed starting MLPrivileges, disabling script. Reason: {0}", result);
                _state = PrivilegeState.StartFailed;
                OnPrivilegesDone(result);
                enabled = false;
            }
        }

        /// Move through the privilege stages
        void Update()
        {
            if (_state != PrivilegeState.Succeeded)
            {
                UpdatePrivilege();
            }
        }

        /// If the Privileges API is running, stop it.
        void OnDestroy()
        {
            if (MLPrivileges.IsStarted)
            {
                MLPrivileges.Stop();
            }
        }

        void OnApplicationPause(bool pause)
        {
            if (pause && _state != PrivilegeState.Off)
            {
                _privilegesGranted.Clear();
                _state = PrivilegeState.Started;
            }
        }
        #endregion

        #region Private Methods
        /// Handle the privilege states.
        private void UpdatePrivilege()
        {
            switch (_state)
            {
                /// Privilege API has been started successfully, ready to make requests.
                case PrivilegeState.Started:
                {
                    RequestPrivileges();
                    break;
                }
                /// Privilege requests have been made, wait until all privileges are granted before enabling the feature that requires privileges.
                case PrivilegeState.Requested:
                {
                    foreach (MLPrivilegeId priv in _privilegesToRequest)
                    {
                        if (!_privilegesGranted.Contains(priv))
                        {
                            return;
                        }
                    }
                    _state = PrivilegeState.Succeeded;
                    OnPrivilegesDone(MLResult.ResultOk);
                    break;
                }
                /// Privileges have been denied, respond appropriately.
                case PrivilegeState.Failed:
                {
                    OnPrivilegesDone(new MLResult(MLResultCode.PrivilegeDenied));
                    enabled = false;
                    break;
                }
            }
        }

        /// Request each needed privilege.
        private void RequestPrivileges()
        {
            foreach (MLPrivilegeId priv in _privilegesToRequest)
            {
                MLResult result = MLPrivileges.RequestPrivilegeAsync(priv, HandlePrivilegeAsyncRequest);
                if (!result.IsOk)
                {
                    Debug.LogErrorFormat("Error: PrivilegeRequester failed requesting {0} privilege. Reason: {1}", priv, result);
                    _state = PrivilegeState.Failed;
                    return;
                }
            }

            _state = PrivilegeState.Requested;
        }
        #endregion

        #region Event Handlers

        /// Handles the result that is received from the query to the Privilege API.
        private void HandlePrivilegeAsyncRequest(MLResult result, MLPrivilegeId privilegeId)
        {
            if (result.Code == MLResultCode.PrivilegeGranted)
            {
                _privilegesGranted.Add(privilegeId);
                Debug.LogFormat("{0} Privilege Granted", privilegeId);
            }
            else
            {
                Debug.LogErrorFormat("{0} Privilege Error: {1}, disabling example.", privilegeId, result);
                _state = PrivilegeState.Failed;
            }
        }
        #endregion
    }
}
