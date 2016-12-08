using UnityEngine;
using System.Collections;

/*

Unfinished controller class 

By: Elena Sparacio

*/

public class GameControl : MonoBehaviour {

	private Vector3 startPos;
	private UI uScript;
	private CharacterController control;

	// Use this for initialization
	void Start () {

		uScript = GameObject.Find ("UICanvas").GetComponent<UI> ();
		startPos = GameObject.Find ("ActualBlob").transform.position;
		control = GameObject.Find("ActualBlob").GetComponent<CharacterController> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		//closes the game
		if (Input.GetButtonDown ("Cancel")) {

			print ("Close game.");
			Application.Quit ();

		} 

		//respawns character
		if (Input.GetButtonDown ("Oops")) {
			Oops ();
		}
	
	}

	//Oops is a method that respawns a character once they die
	public void Oops(){
		//play a message
		uScript.PrintCenterMessage ("Oops!", 3);
		//Find blob and respawn him
		GameObject actualBlob = GameObject.Find ("ActualBlob");
		actualBlob.transform.position = startPos;
		GameObject blob = GameObject.Find ("NewBlob");
		MeshRenderer renderer = blob.GetComponent<MeshRenderer>();
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

		//play the message and disable controls
		uScript.PrintCenterMessage("...you... Died...",3);
		control.enabled = false;


	}


	//GrandBlobDie is activated if you try to freeze your Grandblob to death 
	public IEnumerator GrandBlobDie(){

		yield return new WaitForSeconds(3);
		GameObject grandblob = GameObject.Find ("grandpa");
		Destroy (grandblob.gameObject);

	}

	//you Win activates once you have finished the game - this will probably change
	public void YouWin(){

		uScript.PrintCenterMessage("You found all the seeds! You have saved GrandBlob!",5);
		Cursor.lockState = CursorLockMode.None;
		Time.timeScale = 0;

	}

}
