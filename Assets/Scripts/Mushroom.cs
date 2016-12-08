using UnityEngine;
using System.Collections;

public class Mushroom : MonoBehaviour {

	public GameObject seedPrefab;
	private float elapsedTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime *= .95f;

	}

	//increase the speed of the blade when water hits it
	void OnParticleCollision(GameObject particle){

		if (particle.tag == "water") {
			elapsedTime += 1;
			if (elapsedTime >= 12) {
				
				Vector3 oldPos = this.gameObject.transform.position;
				GameObject aSeed = Instantiate (seedPrefab) as GameObject;
				aSeed.transform.position = oldPos;
				Destroy (this.gameObject);

			}
		}

	}
}
