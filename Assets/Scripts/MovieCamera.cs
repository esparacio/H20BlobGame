using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*

MovieCamera is the script that deals with all the cutscenes in the game.

Written by: Elena Sparacio
(C) 2016

*/
public class MovieCamera : MonoBehaviour {

	//set both cameras and other related variables
	[SerializeField] Camera movieCam;
	[SerializeField] Camera charCam;
	StoryCanvas storyScript;

	private float speed = 0.13f;
	private Vector3 destination;
	private bool isBegin;

	// Use this for initialization
	void Start () {

		//get story canvas
		storyScript = GameObject.Find("StoryCanvas").GetComponent<StoryCanvas>();

		SetMovieCam ();
		CutsceneSetup ();

		//destination is just before we meet our character
		destination = new Vector3 (34, 4, -30);
		isBegin = true;

		//begin cutscene
		startAnimation ();
	
	}
	
	// Update is called once per frame
	void Update () {

		//in the beginning of the game, move the camera for an animation
		if (isBegin) {
			movieCam.transform.position = Vector3.Lerp (transform.position, destination, speed * Time.deltaTime);
		}

		//skip animation if enter/return key is pressed
		if (Input.GetKeyDown("="))
		{
			storyScript.setSkip (true);
		}
	}

	//if the character hits the camera collider, switch to the character camera
	void OnTriggerEnter(Collider other) {
		if (isBegin) {
			//set character camera to allow the player to control the blob
			SetCharCam ();
			//move movie camera out of view
			movieCam.transform.position = new Vector3 (movieCam.transform.position.x, movieCam.transform.position.y - 40, movieCam.transform.position.z);
		} 
	}

	//setup for a cutscene
	void CutsceneSetup(){
		//since this is run on initilization, set the cutscene to be fresh
		TalkToGrandpa script = GameObject.Find ("grandpa").GetComponent<TalkToGrandpa> ();
		script.setCutscene (true);
	}

	//sets the camera back to being focused on the player
	public void SetCharCam(){

		isBegin = false;

		//unlock cursor
		Cursor.visible = true;
		//unlock controls 
		GameObject blob = GameObject.Find("ActualBlob");
		CharacterController control = blob.GetComponent<CharacterController> ();
		control.enabled = true;

		//change camera appropriately
		movieCam.enabled = false;
		charCam.enabled = true; 

		//take off movie bars
		setMovieScreen(false);

        //stop moviecamera music
        GetComponent<AudioSource>().Stop();

        //start game music
        charCam.GetComponent<AudioSource>().Play();

        //show controls text and temperature
        string controls = "Press \"c\" for controls :)";
		Text controlText = GameObject.Find("ControlCanvas").GetComponent<Text>();
		controlText.text = controls;

	}

	//setMovieScreen will enable or disable the movieCanvas 
	public void setMovieScreen(bool isEnabled){
		
		//get movie canvas
		Canvas movieCanvas = GameObject.Find ("MovieScreen").GetComponent<Canvas>();
		movieCanvas.enabled = isEnabled;

	}

	//sets the camera to the movie mode, where the player cannot interact
	public void SetMovieCam(){

		//lock cursor
		Cursor.visible = false;
		//lock controls 
		GameObject blob = GameObject.Find("ActualBlob");
		CharacterController control = blob.GetComponent<CharacterController> ();
		control.enabled = false;

		//change camera appropriately
		movieCam.enabled = true;
		charCam.enabled = false; 

		setMovieScreen (true);

		//hide controls text and temperature bar
		Text controlText = GameObject.Find("ControlCanvas").GetComponent<Text>();
		controlText.text = "";

	}

	//startAnimation is the first cutscene of the game. It introduces the player
	//to the world. 
	public void startAnimation(){

		//don't allow the player to move the cursor
		Cursor.visible = false;

		//Print introduction
		string [] messages = new string[8];
		messages [0] = "You begin in the Arctic Tundra...";
		messages [1] = "A blob with no name that has the ability to control ice.";
		messages [2] = "Until now, you’ve just used your abilities\n to go ice-skating";
		messages [3] = "and freeze ground beef for those tacos\n you’d like to make next month.";
		messages [4] = "But one day...";
		messages [5] = "Everything changes when you go to\n visit your Grandblob.";
		messages [6] = "";
		messages [7] = "Go find your Grandblob.";

		//create times and colors arrays
		int[] times = new int[8];
		for (int i = 0; i < times.Length; i++) {
			times [i] = 5;
		}

		Color[] colors = new Color[8];
		for (int i = 0; i < colors.Length; i++) {
			colors [i] = Color.white;
		}

		//print messages
		StartCoroutine (storyScript.regularCutscene (messages, times, colors));
		storyScript.setQuestText ("Go find Grandblob.");

	}

	//grandpaCutscene is the second cutscene of the game. It is a conversation between Grandblob and
	//the player blob. 
	public void grandpaCutscene(){

		Cursor.visible = false;

		storyScript.setSkip (false);

		//Print grandblob scene
		string [] messages = new string[9];
		messages [0] = "Grandblob! I found you!";
		messages [1] = "H-h-hello t-t-t-here...";
		messages [2] = "Grandblob? Are you okay?";
		messages [3] = "I'm just... So... C-c-c-old...";
		messages [4] = "Grandblob, you don't look so good...";
		messages [5] = "......................................";
		messages [6] = "Grandblob! Nooo! You're frozen!!!";
		messages [7] = "";
		messages [8] = "Go find someone to help!";


		//create times array
		int[] times = new int[9];
		for (int i = 0; i < times.Length; i++) {
			times [i] = 3;
		}

		//change color based on dialogue 
		Color[] colors = new Color[9];

		//set dialogue colors
		Color grandblobColor = new Color32(1,12,255,255);
		Color blobColor = new Color32(0,244,225,255);

		colors [0] = blobColor;
		colors [1] = grandblobColor;
		colors [2] = blobColor;
		colors [3] = grandblobColor;
		colors [4] = blobColor;
		colors [5] = grandblobColor;
		colors [6] = blobColor;
		colors [7] = grandblobColor;
		colors [8] = Color.white;

		StartCoroutine (storyScript.dialogCutscene (messages, times, colors));

		TalkToGrandpa script = GameObject.Find ("grandpa").GetComponent<TalkToGrandpa> ();
		script.setCutscene (false);

		//activate the next cutscene
		Blobrarian scriptLibrary = GameObject.Find ("Blobrarian").GetComponent<Blobrarian> ();
		scriptLibrary.setCutscene (true);
		storyScript.setQuestText ("Go find help!");


	}

	//this is the third cutscene of the game. It sets the scene and tells the player how to save
	//their Grandblob
	public void LibraryCutscene(){

		Cursor.visible = false;

		storyScript.setSkip (false);

		//Print library scene
		string [] messages = new string[9];
		messages [0] = "Hello there. You look worried.";
		messages [1] = "I am, Blobrarian! My Grandblob is FROZEN.";
		messages [2] = "That's terrible. It must be Blobothermia, from the temperature.";
		messages [3] = "Is there anything I can do to save him?";
		messages [4] = "...Well, perhaps. There is a legend that certain plants around the world" +
			" can be collected and planted to change the temperature.";
		messages [5] = "If I collect those plants, can I save my Grandblob?";
		messages [6] = "I would imagine so. Look, in that lake just near me... A plant.";
		messages [7] = "";
		messages [8] = "Collect all the plants around the world.";

		//create times array
		int[] times = new int[9];
		times [0] = 3;
		times [1] = 3;
		times [2] = 4;
		times [3] = 3;
		times [4] = 5;
		times [5] = 3;
		times [6] = 4;
		times [7] = 3;
		times [8] = 3; 

		//change color based on dialogue 
		Color[] colors = new Color[9];

		//set dialogue colors
		Color libraryColor = new Color32(118,78,31,255);
		Color blobColor = new Color32(0,244,225,255);

		colors [0] = libraryColor;
		colors [1] = blobColor;
		colors [2] = libraryColor;
		colors [3] = blobColor;
		colors [4] = libraryColor;
		colors [5] = blobColor;
		colors [6] = libraryColor;
		colors [7] = libraryColor;
		colors [8] = Color.white;

		StartCoroutine (storyScript.dialogCutscene (messages, times, colors));

		//disable the cutscene now that it has played
		Blobrarian scriptLibrary = GameObject.Find ("Blobrarian").GetComponent<Blobrarian> ();
		scriptLibrary.setCutscene (false);
		storyScript.setQuestText ("Collect all the plants.");


	}

	//this tells the player more about planting seeds
	public void SeedInstructions(){

		storyScript.setSkip (false);

		string[] messages = new string[5];
		messages [0] = "You can plant seeds by pressing 'shift' to gain more powers.";
		messages [1] = "1 planted seed will give you WATER powers in an area.";
		messages [2] = "2 planted seeds will give you VAPOR powers in an area";
		messages [3] = "Pick up a seed YOU PLANTED by pressing 'E'";
		messages [4] = "Once you collect ALL of the seeds, \n plant them around Grandblob to warm him.";

		//create times and colors arrays
		int[] times = new int[5];
		for (int i = 0; i < times.Length; i++) {
			times [i] = 3;
		}
		Color[] colors = new Color[5];
		for (int i = 0; i < colors.Length; i++) {
			colors [i] = Color.white;
		}

		StartCoroutine (storyScript.regularCutscene (messages, times, colors));

	}

	//this plays the final sequence of the game where Grandblob is saved
	public void EndingSequence(){

		Cursor.visible = false;

		storyScript.setSkip (false);

		//Print grandblob scene
		string [] messages = new string[8];
		messages [0] = "Grandblob!!!";
		messages [1] = "Blob! You saved me!";
		messages [2] = "I'm so happy you are alright!";
		messages [3] = "...";
		messages [4] = "Do you still have the ground beef for those tacos?";
		messages [5] = "As a matter a fact I think it is defrosted now...";
		messages [6] = "";
		messages [7] = "THE END";


		//create times array
		int[] times = new int[8];
		for (int i = 0; i < times.Length-1; i++) {
			times [i] = 3;
		}
		times [7] = 5;

		//change color based on dialogue 
		Color[] colors = new Color[8];

		//set dialogue colors
		Color grandblobColor = new Color32(1,12,255,255);
		Color blobColor = new Color32(0,244,225,255);

		colors [0] = blobColor;
		colors [1] = grandblobColor;
		colors [2] = blobColor;
		colors [3] = grandblobColor;
		colors [4] = grandblobColor;
		colors [5] = blobColor;
		colors [6] = blobColor;
		colors [7] = Color.white;

		StartCoroutine (storyScript.dialogCutscene (messages, times, colors));
		storyScript.IsEnd (true);

	}
}
