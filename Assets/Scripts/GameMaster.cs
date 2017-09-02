using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

	public static GameMaster gm;

	[SerializeField]
	private int maxLives = 3;

	private static int _remainingLives;
	public static int RemainingLives {
		get { return _remainingLives; }
	}

	private AudioManager audioManager;

	void Start () {
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster>();
		}
		_remainingLives = maxLives;

		audioManager = AudioManager.instance;
		if (audioManager == null) {
			Debug.LogError ("No audio manager found");
		}
	}

	public Transform playerPrefab;
	public Transform spawnPoint;
	public float spawnDelay = 2;
	public Transform spawnPrefab;
	public AudioClip respawnAudio;
	public Transform enemyDeathParticles;
	public string spawnSoundName;

	public string gameOverSoundName = "GameOver";

	[SerializeField]
	private GameObject gameOverUI;

	public void EndGame () {

		audioManager.PlaySound (gameOverSoundName);

		Debug.Log ("Game Over");
		gameOverUI.SetActive (true);
	}

	public IEnumerator _RespawnPlayer () {
		audioManager.PlaySound (spawnSoundName);
		yield return new WaitForSeconds (spawnDelay);

		Instantiate (playerPrefab, spawnPoint.position, spawnPoint.rotation);
		Transform clone = Instantiate (spawnPrefab, spawnPoint.position, spawnPoint.rotation) as Transform;
		Destroy (clone.gameObject, 3f);
	}

	public static void KillPlayer (Player player) {
		Destroy (player.gameObject);
		_remainingLives -= 1;
		if (_remainingLives <= 0) {
			gm.EndGame ();
		} else {
			gm.StartCoroutine (gm._RespawnPlayer());
		}
	}

	public static void KillEnemy (Enemy enemy) {
		gm._KillEnemy (enemy);
	}
	public void _KillEnemy(Enemy _enemy) {
		audioManager.PlaySound (_enemy.deathSoundName);
		GameObject _clone =  Instantiate (_enemy.deathParticles, _enemy.transform.position, Quaternion.identity).gameObject;
		Destroy (_clone, 5f);
		Destroy (_enemy.gameObject);
		ScoreScript.scoreValue += 10;
	}
}