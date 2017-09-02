using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverUI : MonoBehaviour {

	[SerializeField]
	string mouseHoverSound = "ButtonHover";

	[SerializeField]
	string buttonPressSound = "ButtonPress"; 

	AudioManager audioManager;

	void Start () {
		audioManager = AudioManager.instance;
		if (audioManager == null) {
			Debug.LogError ("No audio manager found");
		}
	}

	public void Quit () {

		audioManager.PlaySound (buttonPressSound);

		Debug.Log ("ApplicationQuit");
		Application.Quit ();
	}
	public void Retry () {

		audioManager.PlaySound (buttonPressSound);

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void OnMouseOver () {
		audioManager.PlaySound (mouseHoverSound);
	}
}
