using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPoints : MonoBehaviour {
	
	public int scoreToGive;
	private ScoreManager scoreManager;

	private AudioSource coinSound;

	// Use this for initialization
	void Start () {
		scoreManager = FindObjectOfType<ScoreManager> ();

		coinSound = GameObject.Find ("CoinSound").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name == "Player") {
			scoreManager.AddScore(scoreToGive);
			gameObject.SetActive (false);

			if (coinSound.isPlaying) {
				coinSound.Stop ();
			} 
			coinSound.Play ();
			
		}
	}
}
