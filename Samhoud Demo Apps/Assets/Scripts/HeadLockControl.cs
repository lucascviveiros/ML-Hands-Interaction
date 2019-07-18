using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadLockControl : MonoBehaviour
{
#region Public Variables
    public GameObject Camera;
    //public Text myText;
#endregion

    #region Private Variables
    private const float DISTANCE = 0.5f;
    //private float SPEED = 5.0f;

    #endregion

    #region Public Methods
    public void HardHeadLock(GameObject obj)
    {
        obj.transform.position = Camera.transform.position + Camera.transform.forward * DISTANCE;
        obj.transform.rotation = Camera.transform.rotation;
    }
    public void HeadLock(GameObject obj, float speed)
    {
        speed = Time.deltaTime * speed;
        Vector3 posTo = Camera.transform.position + (Camera.transform.forward * DISTANCE);
        obj.transform.position = Vector3.SlerpUnclamped(obj.transform.position, posTo, speed);
        Quaternion rotTo = Quaternion.LookRotation(obj.transform.position - Camera.transform.position);
        obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, rotTo, speed);
    }
    #endregion

   // private void Update()
   // {
    ///    myText.transform.position = Camera.transform.position + Camera.transform.forward * DISTANCE;
   //    myText.transform.rotation = Camera.transform.rotation;
   // }
}
