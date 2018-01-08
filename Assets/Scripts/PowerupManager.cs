using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour {

	private bool doublePoints;
	private bool safeMode;

	private bool powerupActive;

	private float powerupLengthCounter;

	private ScoreManager scoreManager;
	private PlatformController platformGenerator;

	private float normalPointsPerSecond;
	private float normalSpikeRate;

	private PlatformDestroyer[] spikes;

	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		scoreManager = FindObjectOfType<ScoreManager> ();
		platformGenerator = FindObjectOfType<PlatformController> ();
		gameManager = FindObjectOfType<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (powerupActive) {
			powerupLengthCounter -= Time.deltaTime;

			if (gameManager.powerupReset) {
				powerupLengthCounter = 0f;
				gameManager.powerupReset = false;
			}
			if (doublePoints) {
				scoreManager.pointsPerSeconds = normalPointsPerSecond * 2.75f;
				scoreManager.shouldDouble = true;
			}
			if (safeMode) {
				platformGenerator.randomSpikeTreshold = 0f;
			}

			if (powerupLengthCounter <= 0) {
				scoreManager.pointsPerSeconds = normalPointsPerSecond;
				scoreManager.shouldDouble = false;
				platformGenerator.randomSpikeTreshold = normalSpikeRate;
				powerupActive = false;
			}
		}
	}

	public void ActivatePowerUp(bool dp, bool sm, float time) {

		doublePoints = dp;
		safeMode = sm;
		powerupLengthCounter = time;

		normalPointsPerSecond = scoreManager.pointsPerSeconds;
		normalSpikeRate = platformGenerator.randomSpikeTreshold;

		if (safeMode) {
			spikes = FindObjectsOfType<PlatformDestroyer> ();
			for (int i = 0; i < spikes.Length; i++) {
				if (spikes [i].gameObject.name.Contains("spikes")) {
					spikes [i].gameObject.SetActive (false);
				}
			}
		}

		powerupActive = true;
	}
}
