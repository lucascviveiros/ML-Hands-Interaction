using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Final : MonoBehaviour {

	private PlayableDirector playableDirector;
	public Button botao;

	// Use this for initialization
	void Start () {
		playableDirector = GetComponent<PlayableDirector> (); 
		Time.timeScale = 1.0f;
	}

	// Update is called once per frame
	void Update () {
		if (playableDirector.state != PlayState.Playing) {
			botao.gameObject.SetActive (true);
		}
	}

}
