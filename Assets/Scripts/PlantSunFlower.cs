using UnityEngine;
using System.Collections;

/*

PlantSunFlower is the script attached to the player that allows it to plant 
seeds.

Written by: Nathan Young and Elena Sparacio
(C) 2016

*/
public class PlantSunFlower : MonoBehaviour {

	//variables including for all prefabs
	public GameObject seedPrefab;
	public GameObject waterPlacePrefab;
	public GameObject vaporPlacePrefab;

	//variables for the number of seeds and if the seed collected is the first seed
	private int numSeeds;
	private bool isFirst;

	//variables for scripts
	BlobPlayer blobPlayer;
	SeedCounter seedCounter;

	//constants
	const float MIN_DIST = 15.0f;
	const float GRAND_DIST = 20.0f;
	const int STARTING_NUM = 0;


	// Use this for initialization
	void Start () {

		GameObject actualBlob = GameObject.Find ("ActualBlob");
		blobPlayer = actualBlob.GetComponent<BlobPlayer> ();
		seedCounter = GameObject.Find ("SeedText").GetComponent<SeedCounter> ();

		numSeeds = STARTING_NUM;
		seedCounter.updateCounter (STARTING_NUM);
		isFirst = true;

	}

	// Update is called once per frame
	void Update () {

		//if the player is allowed to plant a seed, put it down in front of the player
		if(Input.GetButtonDown("PlantSunFlower") && numSeeds>0)
		{
			GameObject blob = GameObject.Find ("ActualBlob");

			//don't allow them to plant a water/vapor place on grandblob... 
			GameObject grandBlob = GameObject.Find ("grandpa");
			if (Vector3.Distance (transform.position, grandBlob.transform.position) <= GRAND_DIST) {

				GameObject seed = Instantiate (seedPrefab) as GameObject;
				seed.tag = "grandpaSeed";
				Vector3 blobPosition = blob.transform.position + (blob.transform.forward * 2);
				seed.transform.position = blobPosition;

			} else {

				//if a water place exists in the area, create a vapor place instead!
				bool isVapor = false;

				GameObject[] waterPlaces = GameObject.FindGameObjectsWithTag ("waterPlace");
				for (var i = 0; i < waterPlaces.Length; i++) {
					Vector3 waterPlacePos = waterPlaces [i].transform.position;
					if (Vector3.Distance (transform.position, waterPlacePos) <= MIN_DIST) {
						Destroy (waterPlaces [i]);
						GameObject newPlace = Instantiate (vaporPlacePrefab) as GameObject;
						Vector3 blobPosition = blob.transform.position + (blob.transform.forward * 5);
						newPlace.transform.position = blobPosition;
						isVapor = true;
					} 
				}
				if (!isVapor) {
					GameObject newPlace = Instantiate (waterPlacePrefab) as GameObject;
					Vector3 blobPosition = blob.transform.position + (blob.transform.forward * 5);
					newPlace.transform.position = blobPosition;
				}
			}

			numSeeds--;
			seedCounter.updateCounter (-1);
		}

	}

	//GotASeed is called when the player picks up a seed. It increments the amount of seeds
	//they currently have. If it is the first collected seed, it plays instructions
	public void GotASeed(GameObject aSeed){

	
		if (aSeed.tag == "waterSeed") {
			Destroy (aSeed.transform.parent.gameObject);
			//if the water place is destroyed, on trigger exit is NOT called, but we need to
			//change the power back
			blobPlayer.SetState ("ice");
			//give the player their seed back
			numSeeds++;
			seedCounter.updateCounter (1);
	
		} else if (aSeed.tag == "vaporSeed") {
			Destroy (aSeed.transform.parent.gameObject);
			//if the vapor place is destroyed, on trigger exit is NOT called, but we need to
			//change the power back
			blobPlayer.SetState ("ice");
			numSeeds += 2;
			seedCounter.updateCounter (2);
			print (numSeeds);
		} else {
			//collected a regular ole seed
			numSeeds++;
			seedCounter.updateCounter (1);
		}

		if (isFirst) {

			MovieCamera movieScript = GameObject.Find ("SecondaryCamera").GetComponent<MovieCamera> ();
			movieScript.SeedInstructions ();
			isFirst = false;
		}
	}
}
