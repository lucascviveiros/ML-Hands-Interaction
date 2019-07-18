using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;

public class TestScript : MonoBehaviour
{
    public Text debugText;
    public MLInputController _controller;
    // Start is called before the first frame update
    void Start()
    {
        debugText.text = "Hello Debug";
        MLInput.Start();
        _controller = MLInput.GetController(MLInput.Hand.Left);
    }

    // Update is called once per frame
    void Update()
    {
        if (_controller.TriggerValue > 0.2) 
        {
            debugText.text = "trigger action";
        }
        if (_controller.Connected) 
        {
            debugText.text = "Controller connected";
        }
        if (_controller.IsBumperDown) 
        {
            debugText.text = "bumper action";
        }
    }

    void OnDestroy()
    {
        MLInput.Stop();
    }
}
