// %BANNER_BEGIN%
// ---------------------------------------------------------------------
// %COPYRIGHT_BEGIN%
//
// Copyright (c) 2019 Magic Leap, Inc. All Rights Reserved.
// Use of this file is governed by the Creator Agreement, located
// here: https://id.magicleap.com/creator-terms
//
// %COPYRIGHT_END%
// ---------------------------------------------------------------------
// %BANNER_END%

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.XR.MagicLeap
{
    /// KeyPoseTypes flags enumeration. This enumeration lists the MLHandKeyPose enumerations as Flags so that
    /// more than one keyposes can be easily selected from the inspector.
    [Flags]
    public enum KeyPoseTypes
    {
        Finger = (1 << MLHandKeyPose.Finger),
        Fist = (1 << MLHandKeyPose.Fist),
        Pinch = (1 << MLHandKeyPose.Pinch),
        Thumb = (1 << MLHandKeyPose.Thumb),
        L = (1 << MLHandKeyPose.L),
        OpenHandBack = (1 << MLHandKeyPose.OpenHandBack),
        Ok = (1 << MLHandKeyPose.Ok),
        C = (1 <<  MLHandKeyPose.C),
        NoPose = (1 <<  MLHandKeyPose.NoPose)
    }

    /// Component used to communicate with the MLHands API and manage
    /// which KeyPoses are currently being tracked by each hand.
    /// KeyPoses can be added and removed from the tracker during runtime.
    public class HandTracking : MonoBehaviour
    {
        #region Private Variables
        [Space, SerializeField, MagicLeapBitMask(typeof(KeyPoseTypes)), Tooltip("All KeyPoses to be tracked")]
        private KeyPoseTypes _trackedKeyPoses;

        [SerializeField]
        private MLKeyPointFilterLevel _keyPointFilterLevel = MLKeyPointFilterLevel.ExtraSmoothed;

        [SerializeField]
        private MLPoseFilterLevel _PoseFilterLevel = MLPoseFilterLevel.ExtraRobust;
        #endregion

        #region Public Properties
        public KeyPoseTypes TrackedKeyPoses { get; private set; }
        #endregion

        #region Unity Methods

        /// Initializes and finds references to all relevant components in the
        /// scene and registers required events.
        void OnEnable()
        {
            MLResult result = MLHands.Start();
            if (!result.IsOk)
            {
                Debug.LogErrorFormat("Error: HandTracking failed starting MLHands, disabling script. Reason: {0}", result);
                enabled = false;
                return;
            }

            UpdateKeyPoseStates(true);

            MLHands.KeyPoseManager.SetKeyPointsFilterLevel(_keyPointFilterLevel);
            MLHands.KeyPoseManager.SetPoseFilterLevel(_PoseFilterLevel);
        }

        /// Stops the communication to the MLHands API and unregisters required events.
        void OnDisable()
        {
            if (MLHands.IsStarted)
            {
                // Disable all KeyPoses if MLHands was started
                // and is about to stop
                UpdateKeyPoseStates(false);
                MLHands.Stop();
            }
        }

        void Update()
        {
            if ((_trackedKeyPoses ^ TrackedKeyPoses) != 0)
            {
                UpdateKeyPoseStates(true);
            }
        }
        #endregion

        #region Public Methods
       
        public void AddKeyPose(KeyPoseTypes keyPose)
        {
            if ((keyPose & _trackedKeyPoses) != keyPose)
            {
                _trackedKeyPoses |= keyPose;
                UpdateKeyPoseStates(true);
            }
        }

        public void RemoveKeyPose(KeyPoseTypes keyPose)
        {
            if ((keyPose & _trackedKeyPoses) == keyPose)
            {
                _trackedKeyPoses ^= keyPose;
                UpdateKeyPoseStates(true);
            }
        }
        #endregion

        #region Private Methods

        private MLHandKeyPose[] GetKeyPoseTypes()
        {
            int[] enumValues = (int[])Enum.GetValues(typeof(KeyPoseTypes));
            List<MLHandKeyPose> keyPoses = new List<MLHandKeyPose>();

            TrackedKeyPoses = 0;
            KeyPoseTypes current;
            for (int i = 0; i < enumValues.Length; ++i)
            {
                current = (KeyPoseTypes)enumValues[i];
                if ((_trackedKeyPoses & current) == current)
                {
                    TrackedKeyPoses |= current;
                    keyPoses.Add((MLHandKeyPose)i);
                }
            }

            return keyPoses.ToArray();
        }

        private void UpdateKeyPoseStates(bool enableState = true)
        {
            MLHandKeyPose[] keyPoseTypes = GetKeyPoseTypes();

            // Early out in case there are no KeyPoses to enable.
            if (keyPoseTypes.Length == 0)
            {
                MLHands.KeyPoseManager.DisableAllKeyPoses();
                return;
            }

            bool status = MLHands.KeyPoseManager.EnableKeyPoses(keyPoseTypes, enableState, true);
            if (!status)
            {
                Debug.LogError("Error: HandTracking failed enabling tracked KeyPoses, disabling script.");
                enabled = false;
                return;
            }
        }
        #endregion
    }
}
