using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoIA : MonoBehaviour {
	
	public Transform myTransform;
	private GameObject player;
	public float f_MoveSpeed = 8.0f;

	public GameObject bulletPrefab;
	public GameObject bulletSpawn;

	void Start () {
		player = GameObject.Find ("PlayerShip");
	}
	
	void FixedUpdate () {
		transform.LookAt(player.transform);
		transform.position += transform.forward * f_MoveSpeed * Time.deltaTime;
	}

}
