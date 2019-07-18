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

namespace UnityEngine.XR.MagicLeap
{
    [RequireComponent(typeof(Canvas))]
    public class HeadposeCanvas : MonoBehaviour
    {
        #region Public Variables
        [Tooltip("The distance from the camera that this object should be placed.")]
        public float CanvasDistance = 1.5f;

        [Tooltip("The speed at which this object changes its position.")]
        public float PositionLerpSpeed = 5f;

        [Tooltip("The speed at which this object changes its rotation.")]
        public float RotationLerpSpeed = 5f;
        #endregion

        #region Private Varibles
        private Canvas _canvas;
        private Camera _camera;
        #endregion

        #region Unity Methods

        void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _camera = _canvas.worldCamera;

            // Disable this component if
            // it failed to initialize properly.
            /*if (_canvas == null)
            {
                Debug.LogError("Error: HeadposeCanvas._canvas is not set, disabling script.");
                enabled = false;
                return;
            }
            if (_camera == null)
            {
                Debug.LogError("Error: HeadposeCanvas._camera is not set, disabling script.");
                enabled = false;
                return;
            }*/
        }

        /// Update position and rotation of this canvas object to face the camera using lerp for smoothness.
        void Update()
        {
            // Move the object CanvasDistance units in front of the camera.
            float posSpeed = Time.deltaTime * PositionLerpSpeed;
            Vector3 posTo = _camera.transform.position + (_camera.transform.forward * CanvasDistance);
            transform.position = Vector3.SlerpUnclamped(transform.position, posTo, posSpeed);

            // Rotate the object to face the camera.
            float rotSpeed = Time.deltaTime * RotationLerpSpeed;
            Quaternion rotTo = Quaternion.LookRotation(transform.position - _camera.transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotTo, rotSpeed);
        }
        #endregion
    }
}
