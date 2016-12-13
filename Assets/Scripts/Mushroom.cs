using UnityEngine;
using System.Collections;

/*

Mushroom is attached to mushrooms in the garden and makes then transform into the specified object when watered.

Written by: Elena Sparacio
(C) 2016

*/

public class Mushroom : MonoBehaviour {

	public GameObject seedPrefab;
	private float amountWatered;

	// Use this for initialization
	void Start () {
        amountWatered = 0;
	}

	// Add to a counter when a water particle hits the mushroom
	void OnParticleCollision(GameObject particle){

		if (particle.tag == "waterParticle") {
			amountWatered += 1;
			if (amountWatered >= 20) {
				GameObject aSeed = Instantiate (seedPrefab) as GameObject;
				aSeed.transform.position = gameObject.transform.position;
				Destroy (gameObject);
			}
		}
	}
}
