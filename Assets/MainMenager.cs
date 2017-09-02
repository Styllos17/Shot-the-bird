using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenager : MonoBehaviour {


	[SerializeField]
	string hoverOverSound = "ButtonHover";

	[SerializeField]
	string pressButtonSound = "ButtonPress";

	AudioManager audioManager;

	void Start () {
		audioManager = AudioManager.instance;
		if (audioManager == null) {
			Debug.LogError ("No audio manager");
		}
	}
	
		public void StartGame () {
		audioManager.PlaySound (pressButtonSound);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex +1);
	}

	public void QuitGame() {
		audioManager.PlaySound (pressButtonSound);
		Debug.Log ("We quit the game");
		Application.Quit ();
	}

	public void OnMouseOver () {
		audioManager.PlaySound (hoverOverSound);
	}
}
