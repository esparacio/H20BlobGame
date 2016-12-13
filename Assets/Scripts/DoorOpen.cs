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

    private float doorSpeed = .005f;
    private float amountToOpen = 110;

    // Use this for initialization
    void Start () {
		closedAngle = transform.localEulerAngles.y - 360;
        openAngle = closedAngle + amountToOpen;
		progress = 0;
    }
	
	// Update is called once per frame
	void Update () {

        // If the door is open, check to see if the seed is taken. If so, open it and stop checking.
        if (progress > 0 && !complete) {
            complete = !(GameObject.Find("MillSeed"));
            progress -= doorSpeed;
        } else {
            doorOpenAndClose();
        }
		progress = Mathf.Clamp (progress, 0, 1);
		gameObject.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(closedAngle, openAngle, progress), 0);
    }

	//This opens the door all the way and restarts the lerp
	public void doorOpenAndClose(){
		progress += (doorSpeed * 4);
	}

}
