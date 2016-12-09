using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SeedCounter : MonoBehaviour {

	//get textfield for updating
	Text seedText;
	int totalSeeds;
	int currentAmt;

	// Use this for initialization
	void Start () {

		//get the text field
		seedText = this.gameObject.GetComponent<Text>();
		//get all the seeds in the world 
		GameObject[] seeds = GameObject.FindGameObjectsWithTag ("seed");
		totalSeeds = seeds.Length;
		currentAmt = 0;
		updateCounter (0);
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void updateCounter(int seedToAdd){

		currentAmt += seedToAdd;
		string counterString = currentAmt + "/" + totalSeeds;
		seedText.text = counterString;
	}
}
