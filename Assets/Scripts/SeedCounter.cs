using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*

Seed counter is used to count the number of seeds in the game, as well
as update the counter based upon collected plants

By: Elena Sparacio

*/
public class SeedCounter : MonoBehaviour {

	//get textfield for updating
	private Text seedText;
	public int totalSeeds;
	private int currentAmt;
	bool isWon;

	// Use this for initialization
	void Start () {

		//get the text field
		seedText = this.gameObject.GetComponent<Text>();
		//get all the seeds in the world 
		GameObject[] seeds = GameObject.FindGameObjectsWithTag ("seed");
		totalSeeds = seeds.Length;
		isWon = false;
		currentAmt = 0;
		updateCounter (0);
	
	}
	
	// Update is called once per frame
	void Update () {

		if (!isWon) {
			checkWin ();
		}

	}

	public void updateCounter(int seedToAdd){

		currentAmt += seedToAdd;
		string counterString = currentAmt + "/" + totalSeeds;
		seedText.text = counterString;
	}

	//check win will see if the player has collected enough seeds to save 
	//their grandblob. If they have, it updates the quest marker. 
	public void checkWin(){
		
		if (currentAmt >= totalSeeds) {
			isWon = true;
			StoryCanvas storyScript = GameObject.Find ("StoryCanvas").GetComponent<StoryCanvas> ();
			storyScript.PrintCenterMessage ("You collected all the seeds! Go save Grandblob by planting seeds around him to warm him up!", 5);
			storyScript.setQuestText ("Plant near Grandblob.");

		}
	}
}
