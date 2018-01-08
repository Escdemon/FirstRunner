using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {
	
	public AudioClip[] clips;

	void Start(){
		//myClips = Resources.LoadAll("../Audio",AudioClip);
		playRandomMusic ();
	}

	void Update()
	{
		/*if(!audio.isPlaying)
			playRandomMusic();*/

	}

	void playRandomMusic()
	{
		/*
		audio.clip = myClips[Random.Range(0, myClips.length)];
		audio.Play();*/

		//Debug.Log ("clips size : " + clips.Length);
		//int selector = Random.Range (0, clips.Length);
		//Debug.Log ("selector : " + selector);
		AudioSource source = gameObject.GetComponent<AudioSource> ();

		source.clip = clips [Random.Range (0, clips.Length)];

		source.Play ();
	}
		
}
