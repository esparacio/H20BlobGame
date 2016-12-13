using UnityEngine;
using System.Collections;

/*

WorldWall notifies players that they have hit the map boundary.

Written by: Elena Sparacio
(C) 2016

*/

public class WorldWall : MonoBehaviour {

	void OnTriggerEnter(Collider other){

		if (other.tag == "Player") {
			StoryCanvas storyScript = GameObject.Find ("StoryCanvas").GetComponent<StoryCanvas> ();
			storyScript.PrintCenterMessage ("Oh noo! You've gone too far. Get back to Blobtown.",5);
		}
	}
}
