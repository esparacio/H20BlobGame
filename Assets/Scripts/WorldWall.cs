using UnityEngine;
using System.Collections;

public class WorldWall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){

		if (other.tag == "Player") {
			StoryCanvas storyScript = GameObject.Find ("StoryCanvas").GetComponent<StoryCanvas> ();
			storyScript.PrintCenterMessage ("Oh noo! You've gone too far. Get back to Blobtown.",5);

		}
	}
}
