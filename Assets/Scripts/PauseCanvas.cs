using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*

Pause Canvas is a class that deals with the pause screen.

Author: Elena Sparacio

*/
public class PauseCanvas : MonoBehaviour {

	//variable to hold the character controller
	private CharacterController control;

	// Use this for initialization
	void Start () {

		//get the character controller for when we need to disable it
		control = GameObject.Find("ActualBlob").GetComponent<CharacterController> ();

		//upon start, hide the Pause Canvas
		setPause(false);
	
	}
	
	// Update is called once per frame
	void Update () {

		//closes the game
		if (Input.GetButtonDown ("Cancel")) {

			//pause the game and give the user a cursor
			Time.timeScale = 0;
			control.enabled = false;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.Confined; 

			//create the pause menu 
			setPause(true);
		} 

	
	}


	//setPause will enable or disable the Pause Screen 
	public void setPause(bool isEnabled){

		//get movie canvas
		Canvas pauseCanvas = GameObject.Find ("PauseCanvas").GetComponent<Canvas>();
		pauseCanvas.enabled = isEnabled;

	}

	//Continue the game 
	public void continueGame(){
		
		//unpause the game and give the user a cursor
		Time.timeScale = 1;
		control.enabled = true;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.None; 

		//hide the pause menu 
		setPause(false);


	}

	//Quit the game from the editor or from the application
	public void quitGame(){

		print ("Bye bye");
		Application.Quit ();

	}
}
