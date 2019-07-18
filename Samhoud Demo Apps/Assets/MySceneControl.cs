using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using UnityEngine.UI;

public class MySceneControl : MonoBehaviour
{
    public MLPersistentBehavior persistentBehavior;
    public GameObject _myObject, _camera, _myEye;
    public MLHandKeyPose[] gestures;
    public enum HandPoses { Ok, Finger, Thumb, OpenHandBack, Fist, NoPose };
    public HandPoses pose = HandPoses.NoPose;
    public MLInputController _controller;
    public MLHand Hand;
    private Vector3 headlook; // where you're looking

    private const float _distance = 1.0f;

    void Start()
    {
        //Initiate Control
        MLInput.Start();
        _controller = MLInput.GetController(MLInput.Hand.Left);
        // MLInput.OnControllerButtonDown += OnButtonDown;

        //Initate Eyes
        MLEyes.Start();

        //Initiate Hands
        MLHands.Start();

        //Initiate Array Gestures
        gestures = new MLHandKeyPose[10];
        gestures[0] = MLHandKeyPose.Fist; //Close hand gesture
        gestures[1] = MLHandKeyPose.Thumb; //Positive gesture
        gestures[2] = MLHandKeyPose.C; //"C" gesture
        gestures[3] = MLHandKeyPose.Finger; //Forefinger gesture
        gestures[4] = MLHandKeyPose.OpenHandBack; //Open hand back gesture
        gestures[5] = MLHandKeyPose.L; //"L" gesture
        gestures[6] = MLHandKeyPose.Pinch; //pinch gesture
        gestures[7] = MLHandKeyPose.Ok; //"ok" gesture
        gestures[8] = MLHandKeyPose.NoPose; //hand detected but no pose
        gestures[9] = MLHandKeyPose.NoHand; //No hand detected

        MLHands.KeyPoseManager.EnableKeyPoses(gestures, true, false);
        _camera = GameObject.Find("Main Camera");

        _myEye = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _myEye.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        _myEye.GetComponent<Renderer>().material.color = Color.red;
        _myEye.GetComponent<Collider>();
    }

    void Update()
    {
        if (_controller.TriggerValue > 0.2f || GetGesture(MLHands.Left, MLHandKeyPose.Finger))
        {
            if (MLEyes.IsStarted)
            {
                headlook = MLEyes.FixationPoint - _camera.transform.position;

                RaycastHit _hit;
                if (Physics.Raycast(_camera.transform.position, headlook, out _hit))
                {
                    _myEye.transform.position = _hit.point;
                    _myEye.transform.LookAt(_hit.normal + _hit.point);

                    if (_hit.collider.name == "WKZ")
                    {
                        _myObject.transform.position = _controller.Position + _camera.transform.forward * _distance;
                        _myObject.transform.rotation = _camera.transform.rotation;
                        persistentBehavior.UpdateBinding();
                    }
                }
            }
            //_myObject.transform.position = _controller.Position + _camera.transform.forward * _distance;
            //_myObject.transform.rotation = _camera.transform.rotation;
            //_myObject.transform.LookAt(_camera.transform.position);
        }

        if (_controller.IsBumperDown)
        {
        }
    }

    bool GetGesture(MLHand hand, MLHandKeyPose type)
    {
        if (hand != null)
        {
            if (hand.KeyPose == type)
            {
                if (hand.KeyPoseConfidence > 0.9f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void OnDestroy()
    {
        MLEyes.Stop();
        MLInput.Stop();
        MLHands.Stop();
    }

}
