using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/*

StartButton is the script that goes with the main menu. It allows
you to the start or quit the game. 

Would love to eventually incorporate load/save game in the future. 

Written by: Elena Sparacio
(C) 2016

*/
public class StartButton : MonoBehaviour {

	//Load the scene upon start
	public void startGame(){

		SceneManager.LoadScene("SnowMountains");

	}

	//Quit the game from the editor or from the application
	public void quitGame(){

		print ("Bye bye");
		Application.Quit ();

	}
}
