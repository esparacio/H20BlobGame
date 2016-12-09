using UnityEngine;
using System.Collections;

/*

WaterPlace is a script that is attached to the WaterPlace prefab

By: Nathan Young and Elena Sparacio

*/

public class WaterPlace : MonoBehaviour {

	BlobPlayer blobPlayer;

	// Use this for initialization
	void Start () {
		
		GameObject actualBlob = GameObject.Find ("ActualBlob");
		blobPlayer = actualBlob.GetComponent<BlobPlayer> ();

	}

	// Update is called once per frame
	void Update () {

	}

	//when the player enters the area, change the powers of the player
	void OnTriggerEnter(Collider other){
	 	if (other.tag == "Player") {
			blobPlayer.SetState ("water");
	 		}
	 	}
	
	 //note, when destroyed, must say outside circle
	void OnTriggerExit(Collider other){
	 		if (other.tag == "Player") {
			    blobPlayer.SetState ("ice");

	 		}
	 	}

}