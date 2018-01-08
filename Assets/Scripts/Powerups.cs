using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour {

	public bool doublePoints;
	public bool safeMode;

	public float powerupLength;

	private PowerupManager powerupManager;

	public Sprite[] sprites;

	// Use this for initialization
	void Start () {
		powerupManager = FindObjectOfType<PowerupManager> ();
	}

	void Awake() {
		int powerupSelector = Random.Range (0, 2);
		if (powerupSelector == 0) {
			doublePoints = true;
		} else if (powerupSelector == 1) {
			safeMode = true;
		}

		GetComponent<SpriteRenderer> ().sprite = sprites [powerupSelector];
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.name == "Player") {
			powerupManager.ActivatePowerUp (doublePoints, safeMode, powerupLength);
		}
		gameObject.SetActive (false);
	}
}
