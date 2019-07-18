using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
	public GameObject bulletPrefab;
	public GameObject bulletSpawn;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Fire();
		}
	}

	void Fire()
	{
		GameObject bullet = Instantiate(Resources.Load("LaserGreen", typeof(GameObject))) as GameObject;
		Rigidbody rb = bullet.GetComponent<Rigidbody>();
		bullet.GetComponent<AudioSource>().Play(); ;

		bullet.transform.position = -bulletSpawn.transform.position;
		rb.velocity =  bulletSpawn.transform.forward * 500.0f;
		//rb.AddForce(bulletSpawn.transform.forward * 500.0f);

		Destroy(bullet, 2.0f);
	}
}
