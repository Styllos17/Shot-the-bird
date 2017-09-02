using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[System.Serializable]
	public class PlayerStats {
		public int maxHealth = 100;
		private int _curHealth;
		public int curHealth {
			get { return _curHealth; }
			set { _curHealth = Mathf.Clamp (value, 0, maxHealth);}
		}
		public void Init () {
			_curHealth = maxHealth;
		}
	}

	public PlayerStats stats = new PlayerStats();

	public int fallBoundary = -20;

	public string deathSoundName = "DeathSound";
	public string damageSoundName = "DamageP";

	private AudioManager audioManager;

	[SerializeField]
	private StatusIndicator statusIndicator;
	void Start() {
		stats.Init ();
		if (statusIndicator == null) {
			Debug.Log ("No status indicator player");
		} else {
			statusIndicator.SetHealth (stats.curHealth, stats.maxHealth);
		}

		audioManager = AudioManager.instance;
		if (audioManager == null) {
			Debug.LogError ("No audio manager in scene");
		}
	}

	void Update () {
		if (transform.position.y <= fallBoundary)
			DamagePlayer (999999);	
	}

	public void DamagePlayer (int damage) {
		stats.curHealth -= damage;
		if (stats.curHealth <= 0) {
			//play sound
			audioManager.PlaySound (deathSoundName);

			GameMaster.KillPlayer (this);
		} else {
			//play damage sound
			audioManager.PlaySound(damageSoundName);
		}
		statusIndicator.SetHealth (stats.curHealth, stats.maxHealth);
	}

}
