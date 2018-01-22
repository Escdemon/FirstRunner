using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

	public PlayerController player;
	public Camera camera; 

	public float skyMoveFactor; 
	public Transform skyParent; 
	public GameObject[] skyObjects; 

	public float hillsBackMoveFactor; 
	public Transform hillsBack; 
	public GameObject[] hillsBackObjects; 

	public float hillsFrontMoveFactor; 
	public Transform hillsFront; 
	public GameObject[] hillsFrontObjects; 


	private Vector3 lastPlayerPosition;
	private float skyWidth; 
	private float hillWidth; 


	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController> ();
		skyWidth = skyObjects[0].GetComponent<SpriteRenderer>().bounds.size.x; 
		hillWidth = hillsBackObjects[0].GetComponent<SpriteRenderer>().bounds.size.x; 
		reset ();
	}
	
	// Update is called once per frame
	void Update () {
		// Move different layers at different speeds. Speed customizable with moveFactor variables 
		float distanceToMove = (player.transform.position.x - lastPlayerPosition.x);
		skyParent.position = new Vector3(skyParent.position.x + distanceToMove * skyMoveFactor, skyParent.position.y, skyParent.position.z);
		hillsBack.position = new Vector3(hillsBack.position.x + distanceToMove * hillsBackMoveFactor, hillsBack.position.y, hillsBack.position.z);
		hillsFront.position = new Vector3(hillsFront.position.x + distanceToMove * hillsFrontMoveFactor, hillsFront.position.y, hillsFront.position.z);

		// Loops on background objects for re-use
		foreach (GameObject sky in skyObjects) { 
			if (player.transform.position.x > sky.transform.position.x + skyWidth) {
				sky.transform.position = new Vector3(sky.transform.position.x + (2 * skyWidth), sky.transform.position.y, sky.transform.position.z); 
			}
		}

		foreach (GameObject hill in hillsBackObjects) { 
			if (player.transform.position.x > hill.transform.position.x + hillWidth) {
				hill.transform.position = new Vector3(hill.transform.position.x + (3 * hillWidth), hill.transform.position.y, hill.transform.position.z); 
			}
		}


		foreach (GameObject hill in hillsFrontObjects) { 
			if (player.transform.position.x > hill.transform.position.x + hillWidth) {
				hill.transform.position = new Vector3(hill.transform.position.x + (3 * hillWidth), hill.transform.position.y, hill.transform.position.z); 
			}
		}

		lastPlayerPosition = player.transform.position;
	}

	public void reset() { 
		lastPlayerPosition = player.transform.position;

		skyParent.position = new Vector3(0, skyParent.position.y, skyParent.position.z);
		hillsBack.position = new Vector3(0, hillsBack.position.y, hillsBack.position.z);
		hillsFront.position = new Vector3(0, hillsFront.position.y, hillsFront.position.z);

		for (int i=0; i<skyObjects.Length; i++) { 
			skyObjects[i].transform.position = new Vector3(i*skyWidth, skyObjects[i].transform.position.y, skyObjects[i].transform.position.z); 
		}

		for (int i=0; i<hillsBackObjects.Length; i++) {
			hillsBackObjects[i].transform.position = new Vector3(i*hillWidth, hillsBackObjects[i].transform.position.y, hillsBackObjects[i].transform.position.z); 
		}
		
		for (int i=0; i<hillsFrontObjects.Length; i++) {
			hillsFrontObjects[i].transform.position = new Vector3(i*hillWidth, hillsFrontObjects[i].transform.position.y, hillsFrontObjects[i].transform.position.z); 
		}
	}
}
