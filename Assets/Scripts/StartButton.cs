﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/*

StartButton is the script that goes with the main menu. It allows
you to the start or quit the game. 

Written by: Elena Sparacio
(C) 2016

*/
public class StartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

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
