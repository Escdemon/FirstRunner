using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Transform platformGenerator;
	private Vector3 platformStartPoint;

	public PlayerController player;
	private Vector3 playerStartPoint;

	private PlatformDestroyer[] platforms;

	private ScoreManager scoreManager;

	public DeathMenu deathScreen;
	public bool powerupReset;

	public GameObject pauseButton;

	public GameObject background; 

	// Use this for initialization
	void Start () {
		platformStartPoint = platformGenerator.position;
		playerStartPoint = player.transform.position;

		scoreManager = FindObjectOfType<ScoreManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RestartGame () {
		//StartCoroutine ("RestartGameCo");

		scoreManager.scoreIncreasing = false;
		player.gameObject.SetActive(false);

		deathScreen.gameObject.SetActive (true);
		pauseButton.SetActive (false);
	}

	public void Reset() {

		deathScreen.gameObject.SetActive (false);
		pauseButton.SetActive (true);

		platforms = FindObjectsOfType<PlatformDestroyer> ();
		for (int i = 0; i < platforms.Length; i++) {
			platforms [i].gameObject.SetActive (false);
		}

		player.transform.position = playerStartPoint;
		platformGenerator.position = platformStartPoint;
		player.gameObject.SetActive (true);

		scoreManager.score = 0.0f;
		scoreManager.scoreIncreasing = true;

		powerupReset = true;

		background.GetComponent<BackgroundController>().reset (); 
	}

	/*public IEnumerator RestartGameCo () {

		scoreManager.scoreIncreasing = false;
		player.gameObject.SetActive(false);

		yield return new WaitForSeconds (0.5f);

		platforms = FindObjectsOfType<PlatformDestroyer> ();
		for (int i = 0; i < platforms.Length; i++) {
			platforms [i].gameObject.SetActive (false);
		}

		player.transform.position = playerStartPoint;
		platformGenerator.position = platformStartPoint;
		player.gameObject.SetActive (true);

		scoreManager.score = 0.0f;
		scoreManager.scoreIncreasing = true;
	}*/
}
