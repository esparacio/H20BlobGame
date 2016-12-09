using UnityEngine;
using System.Collections;

/*

PlantSunFlower is the script attached to the player that allows it to plant 
seeds.

Written by: Nathan Young and Elena Sparacio
(C) 2016

*/
public class PlantSunFlower : MonoBehaviour {

	//variables including default number of seeds
	public GameObject seedPrefab;
	public GameObject waterPlacePrefab;
	public GameObject vaporPlacePrefab;
	private int numSeeds;
	private bool isFirst;
	BlobPlayer blobPlayer;	

	//constants
	const float MIN_DIST = 15.0f;
	const float GRAND_DIST = 20.0f;


	// Use this for initialization
	void Start () {

		//DEBUG
		numSeeds = 0;
		isFirst = true;

		GameObject actualBlob = GameObject.Find ("ActualBlob");
		blobPlayer = actualBlob.GetComponent<BlobPlayer> ();

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
			print (numSeeds);


		}

	}

	//GotASeed is called when the player picks up a seed. It increments the amount of seeds
	//they currently have. If it is the first collected seed, it plays instructions
	public void GotASeed(GameObject aSeed){


		if (aSeed.tag == "waterSeed") {
			//destroy the things around the seed 
			GameObject[] waterPlaces = GameObject.FindGameObjectsWithTag ("waterPlace");
			for (var i = 0; i < waterPlaces.Length; i++) {
				Vector3 waterPlacePos = waterPlaces [i].transform.position;
				if (Vector3.Distance (transform.position, waterPlacePos) <= MIN_DIST) {
					Destroy (waterPlaces [i]);
					//if the water place is destroyed, on trigger exit is NOT called, but we need to
					//change the power back
					blobPlayer.SetState ("ice");
					//give the player their seed back
					numSeeds++;
					print (numSeeds);
				}
			}
		} else if (aSeed.tag == "vaporSeed") {
			//destroy the things around the seed 
			GameObject[] vaporPlaces = GameObject.FindGameObjectsWithTag ("vaporPlace");
			for (var i = 0; i < vaporPlaces.Length; i++) {
				Vector3 waterPlacePos = vaporPlaces [i].transform.position;
				if (Vector3.Distance (transform.position, waterPlacePos) <= MIN_DIST) {
					Destroy (vaporPlaces [i]);
					//if the vapor place is destroyed, on trigger exit is NOT called, but we need to
					//change the power back
					blobPlayer.SetState ("ice");
					numSeeds+=2;
					print (numSeeds);

				}
			}
		}

		if (isFirst) {

			MovieCamera movieScript = GameObject.Find ("SecondaryCamera").GetComponent<MovieCamera> ();
			movieScript.SeedInstructions ();
			isFirst = false;
		}
	}
}
