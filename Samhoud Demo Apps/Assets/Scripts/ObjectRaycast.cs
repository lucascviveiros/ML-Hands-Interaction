using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class ObjectRaycast : MonoBehaviour
{
    public GameObject _thePortal;
    public GameObject _camera;
    public MLInputController _controller;
    private const float _distance = 2.0f;

    private void Awake()
    {
        _camera = GameObject.Find("Main Camera");
    }

    void Start()
    {
        MLInput.Start();
        MLInput.OnControllerButtonDown += OnPressButton;
        _controller = MLInput.GetController(MLInput.Hand.Left);
    }

    void Update()
    {
        transform.position = _controller.Position; //Vector3
        transform.rotation = _controller.Orientation; //quaterion
    }

    void OnPressButton(byte controllerID, MLInputControllerButton button)
    {
        RaycastHit hit;
        if (Physics.Raycast(_controller.Position, transform.forward, out hit)) //origin direction, out hit, maxDistance
        {
            if (button == MLInputControllerButton.Bumper)
            {
                GameObject myInstObj = Instantiate(_thePortal, hit.point, Quaternion.Euler(0, _camera.transform.eulerAngles.y + 180, 0));
            }
        }
    }

    void OnDestroy()
    {
        MLInput.Stop();
        MLInput.OnControllerButtonDown -= OnPressButton;
    }

}