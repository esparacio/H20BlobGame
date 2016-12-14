using UnityEngine;
using System.Collections;

/*

TownBlob is the script that is attached to each blob. 

Written by: Elena Sparacio
(C) 2016

*/
public class TownBlob : MonoBehaviour {

	private string [] genericDialogue;

	// Use this for initialization
	void Start () {

		//initialize generic dialogue - each blob could say one of these random phrases
		genericDialogue = new string[9];
		genericDialogue [0] = "Hello!";
		genericDialogue [1] = "It's such a wonderful day.";
		genericDialogue [2] = "Blub blub blub";
		genericDialogue [3] = "Good to see you!";
		genericDialogue [4] = "I'm thinking of adopting a snowman...";
		genericDialogue [5] = "BLUB BLUB";
		genericDialogue [6] = "Have you considered investment banking?";
		genericDialogue [7] = "I think Ted Cruz is the zodiac killer...";
		genericDialogue [8] = "Excuse me, do you have a moment to talk about our lord and savior, Blobman?";
	
	}

	//When the player comes into a certain range, the town blob should then say
	//one of the generic dialogue options to the player. 
	void OnTriggerEnter(Collider other) {

		System.Random rand = new System.Random ();
		int random = rand.Next (0, genericDialogue.Length);
		StoryCanvas storyScript = GameObject.Find ("StoryCanvas").GetComponent<StoryCanvas> ();
		storyScript.Dialog (genericDialogue[random]);
	}
}
