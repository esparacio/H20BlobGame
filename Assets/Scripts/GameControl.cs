using UnityEngine;
using System.Collections;

/*

Controller class 

By: Elena Sparacio

*/

public class GameControl : MonoBehaviour {

	private Vector3 startPos;
	private StoryCanvas storyScript;
	private CharacterController control;

	// Use this for initialization
	void Start () {

		storyScript = GameObject.Find ("StoryCanvas").GetComponent<StoryCanvas> ();
		startPos = GameObject.Find ("ActualBlob").transform.position;
		control = GameObject.Find("ActualBlob").GetComponent<CharacterController> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		//closes the game
		if (Input.GetButtonDown ("Cancel")) {

			//pause the game and give the user a cursor
			Time.timeScale = 0;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.Confined; 

			//create the pause menu 

		} 
	
	}

	//Oops is a method that respawns a character once they die
    //Included for debugging purposes
	public void Oops(){
		//play a message
		storyScript.PrintCenterMessage ("Oops! You died. Be careful! Blobs only have 24325 lives.", 5);
		//Find blob and respawn him
		GameObject actualBlob = GameObject.Find ("ActualBlob");
		actualBlob.transform.position = startPos;
		GameObject blob = GameObject.Find ("NewBlob");
		MeshRenderer renderer = blob.GetComponent<MeshRenderer>();
		//give him the right starting powers (ice) 
		BlobPlayer blobPlayer = GameObject.Find ("ActualBlob").GetComponent<BlobPlayer> ();
		blobPlayer.SetState("ice");
		//Make blob reappear and enable controls
		renderer.enabled=true;
		control.enabled = true;


	}

	//You Died activates if the player dies. It displays a message and does not let the player move
	public void YouDied(){

		//find the blob and make him disappear
		GameObject blob = GameObject.Find ("NewBlob");
		MeshRenderer renderer = blob.GetComponent<MeshRenderer>();
		renderer.enabled=false;

		//disable controls
		control.enabled = false;
		//respawn the player
		Oops ();


	}

}
