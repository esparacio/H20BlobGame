using UnityEngine;
using System.Collections;

/*

TalkToGrandpa is the script that is attached to Grandblob

Written by: Elena Sparacio
(C) 2016

*/
public class TalkToGrandpa : MonoBehaviour {

	public float spawnTime = 0.5f;
	const float MIN_DIST = 5.0f;
	private int winNumSeeds;
	private bool cutscene1; 
	StoryCanvas storyScript;

	// Use this for initialization
	void Start () {

		//the cutscene has not been played upon initialization
		cutscene1 = false; 
		InvokeRepeating ("CheckObjectsAroundHim", spawnTime, spawnTime);
		storyScript = GameObject.Find("StoryCanvas").GetComponent<StoryCanvas>();

		//get the seeds needed to win
		SeedCounter seedCounter = GameObject.Find ("SeedText").GetComponent<SeedCounter> ();
		winNumSeeds = seedCounter.totalSeeds; 
	
	}

	// Update is called once per frame
	void Update () {

	}

	//setCutscene is called to the set whether or not the first cutscene has been played
	public void setCutscene(bool isTrue){
		cutscene1 = isTrue;
	}

	//Play certain animations
	void OnTriggerEnter(Collider other){

		if (cutscene1) {

			MovieSetup ();
			MovieCamera cameraScript = GameObject.Find ("SecondaryCamera").GetComponent<MovieCamera> ();
			cameraScript.grandpaCutscene ();

		} else if (other.gameObject.tag == "grandpaSeed") {
			//GrandBlob dialogue when he is feeling a bit warmer
			storyScript.Dialog ("T-t-that's a b-b-bit... Warmer.");

		} else {
			//GrandBlob dialogue
			storyScript.Dialog ("*shiver shiver*");

		}
	}
		

	void MovieSetup(){

		//get secondary camera
		Camera movieCam = GameObject.Find ("SecondaryCamera").GetComponent<Camera> ();

		//set location
		movieCam.transform.position = new Vector3 (86, 5, -34);

		//set camera view
		MovieCamera cameraScript = GameObject.Find ("SecondaryCamera").GetComponent<MovieCamera> ();
		cameraScript.SetMovieCam ();

	}

	//This checks if the player is close to grandblob, and if the player uses ice powers, 
	//it kills grandblob :( Also checks for if 10 seeds are around him, leading to him being 
	//unfrozen 
	void CheckObjectsAroundHim(){

		//if ice is too close to him
		GameObject [] iceMagics = GameObject.FindGameObjectsWithTag ("ice");
		for(var i = 0 ; i < iceMagics.Length ; i ++)
		{
			Vector3 icePlace = iceMagics [i].transform.position;
			if (Vector3.Distance (transform.position, icePlace) <= MIN_DIST) {
				storyScript.Dialog ("It is too cold! Oh noooo!");
			}
		}

		//check if the player has won the game
		GameObject [] seeds = GameObject.FindGameObjectsWithTag ("grandpaSeed");
		int winCounter = 0;
		for(var i = 0 ; i < seeds.Length ; i ++)
		{
			Vector3 seedPlace = seeds [i].transform.position;
			if (Vector3.Distance (transform.position, seedPlace) <= MIN_DIST) {
				winCounter++;
				if (winCounter >= winNumSeeds) {
					YouWin ();
				}
			}
		}

	}

	//This method is called when the player saves Grandblob!
	void YouWin(){

		//destroy all the seeds
		GameObject[] seeds = GameObject.FindGameObjectsWithTag("grandpaSeed");
		foreach (GameObject seedObj in seeds) {
			Destroy(seedObj);
		}

		//play the last cutscene 
		MovieSetup ();
		MovieCamera cameraScript = GameObject.Find ("SecondaryCamera").GetComponent<MovieCamera> ();
		cameraScript.EndingSequence ();

	}
}