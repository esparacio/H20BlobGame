using UnityEngine;
using System.Collections;

/*

Water is the script that is attached to a lake in the game. 

Written by: Elena Sparacio
(C) 2016

*/
public class Water : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//if the player hits the water, they die
	void OnTriggerEnter(Collider other) {

		if (other.tag == "Player") {

			StartCoroutine (YouDrowned ());

		}
	}
		
	//print messages telling the player that they died 
	public IEnumerator YouDrowned() {
		
		yield return new WaitForSeconds(1);
		GameControl controller = GameObject.Find ("Controller").GetComponent<GameControl> ();
		controller.YouDied ();

	}
}
