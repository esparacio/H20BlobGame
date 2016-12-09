using UnityEngine;
using System.Collections;

/*

Blobrarian is the script that is attached to the Blobrarian

Written by: Elena Sparacio 
(C) 2016

*/
public class Blobrarian : MonoBehaviour {

	private bool cutscene;
	//location for movie camera - hardcoded
	private Vector3 sceneLocation = new Vector3 (-59, 5, -50);

	// Use this for initialization
	void Start () {

		//the cutscene has not been played upon initialization
		cutscene = false; 

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//setCutscene is called to the set whether or not the first cutscene has been played
	public void setCutscene(bool isTrue){
		cutscene = isTrue;
	}

	//Play certain animations
	void OnTriggerEnter(Collider other){

		if (cutscene) {

			//get secondary camera
			Camera movieCam = GameObject.Find ("SecondaryCamera").GetComponent<Camera> ();

			//set location for cutscene
			movieCam.transform.position = sceneLocation;

			//set camera view
			MovieCamera cameraScript = GameObject.Find ("SecondaryCamera").GetComponent<MovieCamera> ();
			cameraScript.SetMovieCam ();
			cameraScript.LibraryCutscene ();

		} else {

			//Print generic dialogue
			StoryCanvas storyScript = GameObject.Find("StoryCanvas").GetComponent<StoryCanvas>();
			storyScript.Dialog ("Good luck saving your Grandblob.");

		}


	}
}
