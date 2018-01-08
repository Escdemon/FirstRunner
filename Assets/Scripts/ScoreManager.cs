using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public Text scoreText;
	public Text highScoreText;

	public float score;
	public float highScore;

	public float pointsPerSeconds;

	public bool scoreIncreasing; 

	public bool shouldDouble;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetFloat("highScore") != null) {
			highScore = PlayerPrefs.GetFloat("highScore");
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (scoreIncreasing) {
			score += pointsPerSeconds * Time.deltaTime;
		}

		if (score > highScore) {
			highScore = score;
			PlayerPrefs.SetFloat ("highScore", highScore);
		}

		scoreText.text = "Score: " + Mathf.Round(score);
		highScoreText.text = "High Score: " + Mathf.Round(highScore);
	}

	public void AddScore(int points) {

		if (shouldDouble) {
			points = points * 2;
		}
		score += points;
	}
}
