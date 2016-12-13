using UnityEngine;
using System.Collections;

/*

MillBlade is the script attached to the mill blade in the game

Written by: Elena Sparacio, Patrick Lathan, and Nathan Young
(C) 2016

*/
public class MillBlade : MonoBehaviour {

	//the speed at which the blade is spinning
	private float speed;

	// Use this for initialization
	void Start () {
		speed = 0;
	}
	
	// Update is called once per frame
	void Update () {

		//rotate the blade based upon the speed
		Mathf.Clamp (speed,0,20f);
		speed *= .95f;
		this.gameObject.transform.Rotate (0,0,speed);
	}

	//increase the speed of the blade when water hits it
	void OnParticleCollision(GameObject particle){
		
		DoorOpen script = GameObject.Find ("door1").GetComponent<DoorOpen> ();

		if (particle.tag == "waterParticle") {
			speed += 1;
			//open the gate if the speed is great enough
			if (speed >= 12) {
				script.doorOpenAndClose ();
			}

		}
	}
}
