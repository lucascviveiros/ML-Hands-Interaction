using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Cena2Controller : MonoBehaviour {

	public GameObject cameraPlayer;
	private PlayableDirector playableDirector;
	private float startTime = 0.0f;
	private float holdTime = 5.0f;

	void Start () {
		playableDirector = GetComponent<PlayableDirector> (); 
		Time.timeScale = 3.0f;
		StartCoroutine (Wait ());
	}

	void Update () {
		if (Input.GetKey (KeyCode.Space)) {
			startTime += Time.deltaTime;
			if (startTime >= holdTime) {
				SceneManager.LoadScene ("CenaJogo");
			}
		} else {
			startTime = 0.0f;
		}

	}
	
	IEnumerator Wait(){
		playableDirector.Play ();
		yield return new WaitForSeconds (42);
		cameraPlayer.SetActive(true);
		enabled = false;
		yield return new WaitForSeconds (59);
		//SceneManager.LoadScene ("cena2.1");
		SceneManager.LoadScene ("CenaJogo");
	}
}
