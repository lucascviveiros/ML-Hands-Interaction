using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;

public class GestureController : MonoBehaviour
{
    private bool OKHandPose = false;
    private float speed = 30.0f;  // Speed of the GameObject
    private float distance = 2.0f; // Distance between Main Camera and GameObject
    public GameObject myCube; // Reference to the GameObject
    private MLHandKeyPose[] gestures; // Holds the different gestures we will look for
    public Text textCanvas;

    void Awake()
    {
        MLHands.Start();

        gestures = new MLHandKeyPose[4];
        gestures[0] = MLHandKeyPose.Ok;
        gestures[1] = MLHandKeyPose.Fist;
        gestures[2] = MLHandKeyPose.OpenHandBack;
        gestures[3] = MLHandKeyPose.Finger;
        MLHands.KeyPoseManager.EnableKeyPoses(gestures, true, false);

        myCube = GameObject.Find("Cube"); 

    }

    void OnDestroy()
    {
        MLHands.Stop();
    }

    void Update()
    {
        if (OKHandPose)
        {
            if (GetGesture(MLHands.Left, MLHandKeyPose.OpenHandBack) || GetGesture(MLHands.Right, MLHandKeyPose.OpenHandBack))
            {
                myCube.transform.Rotate(Vector3.up, +speed * Time.deltaTime);
                textCanvas.text = "OpenHandBack";
            }

            if (GetGesture(MLHands.Left, MLHandKeyPose.Fist) || GetGesture(MLHands.Right, MLHandKeyPose.Fist))
            {
                myCube.transform.Rotate(Vector3.up, -speed * Time.deltaTime);
                textCanvas.text = "Fist";

            }

            if (GetGesture(MLHands.Left, MLHandKeyPose.Finger))
            {
                myCube.transform.Rotate(Vector3.right, +speed * Time.deltaTime);
                textCanvas.text = "Left Finger";

            }

            if (GetGesture(MLHands.Right, MLHandKeyPose.Finger))
            {
                myCube.transform.Rotate(Vector3.right, -speed * Time.deltaTime);
                textCanvas.text = "Right Finger";
            }
        }
        else
        {
            if (GetGesture(MLHands.Left, MLHandKeyPose.Ok) || GetGesture(MLHands.Right, MLHandKeyPose.Ok))
            {
                OKHandPose = true;
                myCube.SetActive(true);
                myCube.transform.position = transform.position + transform.forward * distance;
                myCube.transform.rotation = transform.rotation;
                textCanvas.text = "Ok!";
            }
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

    public void changeText(int pose)
    {
        switch (pose)
        {
            case 0:
                break;
            case 1:
                break;
        }
    }

}

