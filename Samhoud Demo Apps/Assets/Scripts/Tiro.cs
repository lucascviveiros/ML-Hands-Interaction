using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class Tiro : MonoBehaviour {

	public GameObject bulletPrefab;
	public GameObject bulletSpawn;
    private MLInputController _controller;

    private void Awake()
    {
        MLInput.Start();
        _controller = MLInput.GetController(MLInput.Hand.Left);
    }

    void Update () 
    {
        if (_controller.IsBumperDown || Input.GetKey(KeyCode.Space)) 
        {
			Fire();
		}
	}

	void Fire(){
		GameObject bullet = Instantiate(bulletPrefab, new Vector3(0, 0, 0), Quaternion.identity);
		Rigidbody rb = bullet.GetComponent<Rigidbody>();
		bullet.transform.position = bulletSpawn.transform.position;
		rb.velocity = bulletSpawn.transform.forward * 10.0f;
		Destroy (bullet, 15.0f);
	}

}
