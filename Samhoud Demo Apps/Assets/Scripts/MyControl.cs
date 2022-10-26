using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using UnityEngine.UI;

public class MyControl : MonoBehaviour
{
    public MLPersistentBehavior persistentBehavior;
    public GameObject _myObject, _camera;
    public MLInputController _controller;
    private const float _distance = 1.0f;
    public Text triggerText;

    private void Awake()
    {
        MLInput.Start();
        _controller = MLInput.GetController(MLInput.Hand.Right);
        _camera = GameObject.Find("Main Camera");
        _myObject = GameObject.Find("WKZ");
        triggerText.text = "No button pressed yet";
    }

    private void OnDestroy()
    {
        MLInput.Stop();
    }

    private void Update()
    {
        if (_controller.TriggerValue > 0.2f)
        {
            _myObject.transform.position = _controller.Position + _camera.transform.forward * _distance;
            _myObject.transform.rotation = _camera.transform.rotation;
            persistentBehavior.UpdateBinding();
            triggerText.text = "Trigger Pressed";
        }
        triggerText.text = "Trigger no pressed";

        if (_controller.IsBumperDown) 
        {
            triggerText.text = "Bumper Pressed";
        }
    }

}
