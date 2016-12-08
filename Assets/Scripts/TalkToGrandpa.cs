using UnityEngine;
using System.Collections;

/*

TalkToGranpa is the script that is attached to Grandblob

Written by: Elena Sparacio and Patrick Lathan
(C) 2016

*/
public class TalkToGrandpa : MonoBehaviour {

	public float spawnTime = 0.5f;
	const float MIN_DIST = 5.0f;
	const float WIN_NUM_SEEDS = 8;
	private bool cutscene1; 

	// Use this for initialization
	void Start () {

		//the cutscene has not been played upon initialization
		cutscene1 = false; 
		InvokeRepeating ("CheckObjectsAroundHim", spawnTime, spawnTime);
	
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

		} else if (other.gameObject.tag == "seed") {
			//GrandBlob dialogue when he is feeling a bit warmer
			GameObject UICanvas = GameObject.Find ("UICanvas");
			UI uiScript = UICanvas.GetComponent<UI>();
			uiScript.Dialog ("T-t-that's a b-b-bit... Warmer.");

		} else {
			//GrandBlob dialogue
			GameObject UICanvas = GameObject.Find ("UICanvas");
			UI uiScript = UICanvas.GetComponent<UI>();
			uiScript.Dialog ("*shiver shiver*");

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
				GameObject UICanvas = GameObject.Find ("UICanvas");
				UI uiScript = UICanvas.GetComponent<UI>();
				uiScript.Dialog ("It is too cold! Oh noooo!");
			}
		}

		//check if the player has won the game
		GameObject [] seeds = GameObject.FindGameObjectsWithTag ("seed");
		int winCounter = 0;
		for(var i = 0 ; i < seeds.Length ; i ++)
		{
			Vector3 seedPlace = seeds [i].transform.position;
			if (Vector3.Distance (transform.position, seedPlace) <= MIN_DIST) {
				winCounter++;
				if (winCounter >= WIN_NUM_SEEDS) {
					YouWin ();
				}
			}
		}

	}

	//This method is called when the player saves Grandblob!
	void YouWin(){

		//destroy all the seeds
		GameObject[] seeds = GameObject.FindGameObjectsWithTag("seed");
		foreach (GameObject seedObj in seeds) {
			Destroy(seedObj);
		}

		//play the last cutscene 
		MovieSetup ();
		MovieCamera cameraScript = GameObject.Find ("SecondaryCamera").GetComponent<MovieCamera> ();
		cameraScript.EndingSequence ();

	}
}