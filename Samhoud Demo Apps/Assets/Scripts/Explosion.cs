using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public float Seconds;
    public GameObject ExplosionPrefab;

	// Use this for initialization
	void Start () {
        StartCoroutine(Expl());
        StartCoroutine(Destroi());
    }
	
	// Update is called once per frame
	void Update () {
	}

    IEnumerator Expl()
    {
        yield return new WaitForSeconds(Seconds);
        Instantiate(ExplosionPrefab, transform.position, transform.rotation);
    }

    IEnumerator Destroi()
    {
        yield return new WaitForSeconds(Seconds + 0.3f);
        gameObject.SetActive(false);
    }
}
