using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*

Seed is the script that is attached to a seed in the game.

Written by: Elena Sparacio
(C) 2016

*/
public class Seed : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//if the player hits the seed, allow it to be collected
	void OnTriggerEnter(Collider other) {

		if (other.tag == "Player") {
			SeedCollected ();
			Destroy(this.gameObject);
		}
	}

	//seedCollected is called when the player gets a new seed. 
	void SeedCollected(){

		//tell the player they got a seed
		StoryCanvas storyScript = GameObject.Find("StoryCanvas").GetComponent<StoryCanvas>();
		storyScript.PrintCenterMessage ("Congrats! You collected a seed!", 5);
	
		//add a seed to the seed counter in another script
		PlantSunFlower seedScript = GameObject.Find ("ActualBlob").GetComponent<PlantSunFlower> ();
		seedScript.GotASeed (this.gameObject);

	}
		

}
