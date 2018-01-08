using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

	//public GameObject platform;
	public Transform generationPoint;
	public float distanceBetween;

	//private float platformWidth;

	public float distanceBetweenMin;
	public float distanceBetweenMax;

	//public GameObject[] platforms;
	private int platformSelector;
	private float[] platformWidths;

	public ObjectPooler[] objectPoolers;

	private float minHeight;
	public Transform maxHeightPoint;
	private float maxHeight;
	public float maxHeightChange;
	private float heightChange;

	private CoinGenerator coinGenerator;
	public float randomCoinTreshold;

	public float randomSpikeTreshold;
	public ObjectPooler spikePool;

	public float powerupHeight;
	public ObjectPooler powerupPool;
	public float powerupTreshold;

	// Use this for initialization
	void Start () {
		//platformWidth = platform.GetComponent<BoxCollider2D> ().size.x;

		platformWidths = new float[objectPoolers.Length];
		for (int i = 0; i < objectPoolers.Length; i++) {
			platformWidths [i] = objectPoolers[i].pooledObject.GetComponent<BoxCollider2D> ().size.x;
		}

		minHeight = transform.position.y;
		maxHeight = maxHeightPoint.position.y;

		coinGenerator = FindObjectOfType<CoinGenerator> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (transform.position.x < generationPoint.position.x) {

			distanceBetween = Random.Range (distanceBetweenMin, distanceBetweenMax);

			platformSelector = Random.Range (0, objectPoolers.Length);

			heightChange = Mathf.Max(minHeight, Mathf.Min(maxHeight, transform.position.y + Random.Range(-maxHeightChange, maxHeightChange)));

			if (Random.Range (0f, 100f) < powerupTreshold) {
				GameObject newPowerup = powerupPool.GetPooledObject ();

				newPowerup.transform.position = new Vector3 (transform.position.x + distanceBetween / 2f, Mathf.Max(minHeight, Mathf.Min(maxHeight, transform.position.y + Random.Range(0f, powerupHeight))), transform.position.z);

				newPowerup.SetActive (true);
			}

			transform.position = new Vector3 (transform.position.x + platformWidths[platformSelector] / 2  + distanceBetween, heightChange, transform.position.z);

			//Instantiate (platforms[platformSelector], transform.position, transform.rotation);

			GameObject newPlatform = objectPoolers[platformSelector].GetPooledObject();
			newPlatform.transform.position = transform.position;
			newPlatform.transform.rotation = transform.rotation;
			newPlatform.SetActive (true);

			if (Random.Range (0f, 100f) < randomCoinTreshold) {
				coinGenerator.SpawnCoins (new Vector3 (transform.position.x, transform.position.y + 1f, transform.position.z));
			}

			BoxCollider2D newPlatformCollider = newPlatform.GetComponent<BoxCollider2D> ();

			if (newPlatformCollider.size.x > 4 && Random.Range (0f, 100f) < randomSpikeTreshold) {
				GameObject newSpike = spikePool.GetPooledObject ();

				float posX = Random.Range (-platformWidths [platformSelector] / 2 + 1f, platformWidths [platformSelector] / 2 -1f);
				Vector3 pos = new Vector3 (posX, newPlatform.GetComponent<BoxCollider2D> ().size.y / 2, 0f);
				newSpike.transform.position = transform.position + pos;
				newSpike.transform.rotation = transform.rotation;
				newSpike.SetActive (true);
			}

			transform.position = new Vector3 (transform.position.x + platformWidths[platformSelector] / 2, transform.position.y, transform.position.z);

		}
	}
}
