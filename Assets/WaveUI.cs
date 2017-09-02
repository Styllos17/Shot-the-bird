using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour {

	[SerializeField]
	WavesSpawner spawner;

	[SerializeField]
	Animator waveAnimator;

	[SerializeField]
	Text waveCountdownText;

	[SerializeField]
	Text waveCountText;

	private WavesSpawner.SpawnState previousState;

	// Use this for initialization
	void Start () {
		if (spawner == null)
		{
			Debug.LogError("No spawner referenced!");
			this.enabled = false;
		}
		if (waveAnimator == null)
		{
			Debug.LogError("No waveAnimator referenced!");
			this.enabled = false;
		}
		if (waveCountdownText == null)
		{
			Debug.LogError("No waveCountdownText referenced!");
			this.enabled = false;
		}
		if (waveCountText == null)
		{
			Debug.LogError("No waveCountText referenced!");
			this.enabled = false;
		}
	}

	// Update is called once per frame
	void Update () {
		switch (spawner.State)
		{
		case WavesSpawner.SpawnState.COUNTING:
			UpdateCountingUI();
			break;
		case WavesSpawner.SpawnState.SPAWNING:
			UpdateSpawningUI();
			break;
		}

		previousState = spawner.State;
	}

	void UpdateCountingUI ()
	{
		if (previousState != WavesSpawner.SpawnState.COUNTING)
		{
			waveAnimator.SetBool("WaveIncoming", false);
			waveAnimator.SetBool("WaveCountdown", true);
			//Debug.Log("COUNTING");
		}
		waveCountdownText.text = ((int)spawner.WaveCountdown).ToString();
	}

	void UpdateSpawningUI()
	{
		if (previousState != WavesSpawner.SpawnState.SPAWNING)
		{
			waveAnimator.SetBool("WaveCountdown", false);
			waveAnimator.SetBool("WaveIncoming", true);

			waveCountText.text = spawner.NextWave.ToString();

			//Debug.Log("SPAWNING");
		}
	}
}