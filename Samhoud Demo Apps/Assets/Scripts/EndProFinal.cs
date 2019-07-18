using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class EndProFinal : MonoBehaviour {

	private PlayableDirector playableDirector;

	// Use this for initialization
	void Start () {
		playableDirector = GetComponent<PlayableDirector> (); 
		Time.timeScale = 1.0f;
	}

	// Update is called once per frame
	void Update () {
		if (playableDirector.state != PlayState.Playing) {
			SceneManager.LoadScene ("Cena3-Final");
		}
	}
}
