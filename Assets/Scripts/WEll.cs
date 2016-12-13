using UnityEngine;
using System.Collections;

public class Well : MonoBehaviour {

	private float progress;
	private float startPos;
	private float endPos;
	private GameObject specialSeed;
	private bool isWater;

	// Use this for initialization
	void Start () {

		specialSeed = GameObject.Find ("wellSeed");
		startPos = specialSeed.transform.position.y;
		//hardcoded end position (YIPES) 
		endPos = 5f;
		progress = 0;
		isWater = false;

	}

	// Update is called once per frame
	void Update () {

		if ((isWater)&&(specialSeed)) {
			progress += .005f;
			float currentPos = Mathf.Lerp (startPos, endPos, progress);
			specialSeed.transform.position = new Vector3 (specialSeed.transform.position.x,currentPos,specialSeed.transform.position.z);

		}

	}

	//bring the seed up when water hits the well
	void OnParticleCollision(GameObject particle){

		if (particle.tag == "waterParticle") {
			isWater = true;
		} else {
			isWater = false;
		}
	}
}


