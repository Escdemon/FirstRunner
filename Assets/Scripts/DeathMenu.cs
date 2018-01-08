using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MonoBehaviour {

	public string mainMenu;

	public void RestartGame() {

		FindObjectOfType<GameManager> ().Reset ();
	}

	public void QuitToMainMenu() {

		Application.LoadLevel (mainMenu);
	}
}
