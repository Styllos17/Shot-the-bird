using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LivesCounter : MonoBehaviour {

	private Text livesText;

	void Awake () {
		livesText = GetComponent<Text> ();
	}
	

	void Update () {
		livesText.text = "Lives; " + GameMaster.RemainingLives.ToString();
	}
}
