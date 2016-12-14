using UnityEngine;
using System.Collections;

/*

DoorOpen is a script attached the to the gate in the game 

Written by: Patrick Lathan and Nathan Young
(C) 2016

*/
public class DoorOpen : MonoBehaviour {

	private float progress;
	private float closedAngle;
    private float openAngle;
    private bool complete = false;

    private float doorSpeed = .5f;
    private float amountToOpen = 110;

    // Use this for initialization
    void Start () {
		closedAngle = transform.localEulerAngles.y - 360;
        openAngle = closedAngle + amountToOpen;
		progress = 0;
    }
	
	// Update is called once per frame
	void Update () {

        // If the seed has been collected, keep the door open
        if (complete) {
            doorOpenAndClose();
        } else {
            // If MillSeed can't be found, puzzle complete
            complete = !(GameObject.Find("MillSeed"));
            progress -= (doorSpeed * Time.deltaTime);
        }
		progress = Mathf.Clamp (progress, 0, 1);
		gameObject.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(closedAngle, openAngle, progress), 0);
    }

	//This opens the door partially
	public void doorOpenAndClose(){
		progress += (doorSpeed * Time.deltaTime * 4);
	}

}
