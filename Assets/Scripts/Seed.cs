using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*

Seed is the script that is attached to a seed in the game.

Written by: Elena Sparacio and Patrick Lathan
(C) 2016

*/
public class Seed : MonoBehaviour {
    private bool collectable;

	// Use this for initialization
	void Start () {
        collectable = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (collectable && Input.GetButtonDown("Collect")) {
            SeedCollected();
            Destroy(gameObject);
        }
    }

	//if the player hits the seed, allow it to be collected
	void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            StoryCanvas storyScript = GameObject.Find("StoryCanvas").GetComponent<StoryCanvas>();
            storyScript.PrintCenterMessage("Press E to collect a seed", 5);
            collectable = true;
        }

	}

    void OnTriggerExit() {
        collectable = false;
    }

	//seedCollected is called when the player gets a new seed. 
	void SeedCollected(){

		//tell the player they got a seed
		StoryCanvas storyScript = GameObject.Find("StoryCanvas").GetComponent<StoryCanvas>();
		storyScript.PrintCenterMessage ("Congrats! You collected a seed!", 5);
	
		//add a seed to the seed counter in another script
		PlantSunFlower seedScript = GameObject.Find ("ActualBlob").GetComponent<PlantSunFlower> ();
		seedScript.GotASeed (gameObject);

	}
		

}
