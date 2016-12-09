using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*

StoryCanvas is the script that is attached to the story canvas. It contains 
methods to alter this canvas for the storyline. It includes dialog
and messages that appear in the center

Written by: Elena Sparacio
(C) 2016

*/
public class StoryCanvas : MonoBehaviour {

	//All used textfields
	Text centerText;
	Text convo1;
	Text convo2;
	Text questText;
	//bool that is true if the user chose to skip the cutscene
	private bool isSkip;
	private bool isEnd;

	// Use this for initialization
	void Start () {

		//assign all text fields
		centerText = this.gameObject.GetComponent<Text>();
		convo1 = GameObject.Find ("Convo1").GetComponent<Text> ();
		convo2 = GameObject.Find ("Convo2").GetComponent<Text> ();
		questText = GameObject.Find ("Quest").GetComponent<Text> ();
		//assign boolean values
		isSkip = false;
		isEnd = false;
		//clear dialog boxes for now 
		GameObject.Find("Conversation1").transform.localScale = new Vector3(0,0,0);
		GameObject.Find("Conversation2").transform.localScale = new Vector3(0,0,0);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//setSkip is a method that sets the isSkip bool. If set to true,
	//this will stop the cutscene
	public void setSkip(bool isTrue){
		isSkip = isTrue;
	}

	//At the end of the game, this boolean is changed to true! :) 
	public void IsEnd(bool isTrue){
		isEnd = isTrue;
	}

	//Print center message will display a string on the screen for a certain
	//amount of time
	public void PrintCenterMessage(string aMessage, int aTime){
		centerText.text = aMessage;
		StartCoroutine (Wait (aTime,centerText));
	}

	//This method takes 3 arrays and goes through them to display a full conversation 
	//between blobs. It changes color and display time based upon what is set in the array.

	public IEnumerator regularCutscene(string [] messages, int [] times, Color [] colors){

		for (var i = 0; i < messages.Length; i++) {
			if (isSkip) {
				i = messages.Length;
				break;
			}

			centerText.color = colors [i];
			centerText.text = messages [i];

			int waitTime = times [i];
			yield return new WaitForSeconds (waitTime);
		}
			
		//switch back to normal view 
		centerText.text = "";
		MovieCamera script = GameObject.Find ("SecondaryCamera").GetComponent<MovieCamera> ();
		script.SetCharCam ();

		//if the game is over, load the main menu
		if (isEnd) {
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.Confined;
			Application.LoadLevel("MainMenu");
		}

	}

	//This method takes 3 arrays and goes through them to display a full conversation 
	//between blobs. It changes color and display time based upon what is set in the array.

	public IEnumerator dialogCutscene(string [] messages, int [] times, Color [] colors){


		for (var i = 0; i < messages.Length; i++) {
			if (isSkip) {
				i = messages.Length;
				GameObject.Find("Conversation1").transform.localScale = new Vector3(0,0,0);
				GameObject.Find("Conversation2").transform.localScale = new Vector3(0,0,0);
				break;
			}
			Text aText = centerText;

			//in all dialog cutscenes, the last two messages are centered 
			if ((i == messages.Length - 1)||(i == messages.Length - 2)) {
				//hide dialog boxes
				GameObject.Find("Conversation1").transform.localScale = new Vector3(0,0,0);
				GameObject.Find("Conversation2").transform.localScale = new Vector3(0,0,0);
				//print text
				aText = centerText;
			}
			else if (i % 2 == 0) {
				//second person speaking
				GameObject.Find("Conversation2").transform.localScale = new Vector3(0,0,0);
				GameObject.Find("Conversation1").transform.localScale = new Vector3(1,1,1);
				aText = convo1;
				
			} else {
				//first person speaking
				GameObject.Find("Conversation1").transform.localScale = new Vector3(0,0,0);
				GameObject.Find("Conversation2").transform.localScale = new Vector3(1,1,1);
				aText = convo2;
				
			}

			aText.color = colors [i];
			aText.text = messages [i];

			int waitTime = times [i];
			yield return new WaitForSeconds (waitTime);
		}


		//switch back to normal view - hide all boxes and texts
		centerText.text = "";
		GameObject.Find("Conversation1").transform.localScale = new Vector3(0,0,0);
		GameObject.Find("Conversation2").transform.localScale = new Vector3(0,0,0);
		MovieCamera script = GameObject.Find ("SecondaryCamera").GetComponent<MovieCamera> ();
		script.SetCharCam ();

		//if the game is over, load the main menu
		if (isEnd) {
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.Confined;
			Application.LoadLevel("MainMenu");
		}

	}

	public void setQuestText(string questInfo){
		questText.text = questInfo;
	}

	//Dialog is a method that displays a string at the lower part of the screen
	public void Dialog(string message){

		centerText.text = "\n\n\n\n\""+message+"\"";
		StartCoroutine (Wait (5, centerText));

	}

	//Waits a certain number of seconds and then changes the center message to blank 
	public IEnumerator Wait(int aTime, Text aText) {
		yield return new WaitForSeconds(aTime);
		aText.text = "";

	}
}
