using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*

Controls is the script that is attached to the controls canvas. It contains 
methods to alter this canvas.

Written by: Elena Sparacio
(C) 2016

*/
public class Controls : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//if the key "c" is pressed, show the controls
		if (Input.GetKeyDown ("c")) {
			StartCoroutine (showControls (12));
		} 
	
	}

	//showControls will create a small popup on the bottom of the window that tells the player
	//what the controls are 
	public IEnumerator showControls(int aTime) {

		Text uiText = this.gameObject.GetComponent<Text> ();
		uiText.text = "~~~CONTROLS~~~ \n Use w, a, s, d to move and space to jump.\n" +
			"Click to use your powers.\n" +
			"Press shift to plant a power-changing plant. \n" +
			"1 plant gives you water powers.\n" +
			"2 plants in the same spot gives you vapor powers. \n" +
			"Press 'E' to pick up an already planted plant. \n" + 
			"Press 'O' for the Oops! button.";
		yield return new WaitForSeconds(aTime);
		uiText.text = "Press 'c' to show controls";

	}
}
