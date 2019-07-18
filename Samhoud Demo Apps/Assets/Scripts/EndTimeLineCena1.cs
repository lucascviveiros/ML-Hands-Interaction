using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class EndTimeLineCena1 : MonoBehaviour {

	private PlayableDirector playableDirector;
	private float startTime = 0.0f;
	private float holdTime = 5.0f;

	// Use this for initialization
	void Start () {
		playableDirector = GetComponent<PlayableDirector> (); 
	}
	
	// Update is called once per frame
	void Update () {
		if (playableDirector.state != PlayState.Playing) {
			SceneManager.LoadScene ("cena2");
		}

		if (Input.GetKey (KeyCode.Space)) {
			startTime += Time.deltaTime;
			if (startTime >= holdTime) {
				SceneManager.LoadScene ("cena2");
			}
		} else {
			startTime = 0.0f;
		}

	}
}
