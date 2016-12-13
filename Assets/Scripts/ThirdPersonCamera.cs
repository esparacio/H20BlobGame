using UnityEngine;
using System.Collections;

/*

ThirdPersonCamera is the script that deals with camera movement. 

Written by: Patrick Lathan and Nathan Young
(C) 2016

*/
public class ThirdPersonCamera : MonoBehaviour {

	// General settings - sensitivity
	private float sensitivityHor = 100.0f;
	private float sensitivityVert = 100.0f;
	// General settings - y rotation constraints
	private float maxVert = 45f;
	private float minVert = -45f;
	//General settings - distance from player
	private float cameraHeight = 3f;
	private float cameraDepth = 4f;

    private bool lerpTowardsPlayer = false;
    private float distanceFromSurface;
    private float progress = 1f;

	private GameObject player;

	void Start() {
		// Hide cursor
		Cursor.lockState = CursorLockMode.Locked;

		// Set target to the player
		player = GameObject.FindGameObjectsWithTag("Player")[0];

		// Set initial camera position
		transform.localPosition = new Vector3(0,cameraDepth,cameraHeight);

		// Point at target
		transform.LookAt(player.transform);

        distanceFromSurface = Vector3.Distance(player.transform.position, transform.position);
	}

	void Update() {
		// VERTICAL CAMERA MOVEMENT
		// Vertical movement moves the camera around the playermodel in an arc within specified bounds

		// Get player input, modify by vertical sensitivity, and invert
		float xRot = Input.GetAxis("Mouse Y") * sensitivityVert * Time.deltaTime * -1;

		//TODO use maxvert and minvert instead
		if ((transform.localPosition.z > 0 || xRot > 0) && (transform.localPosition.z < 3 || xRot < 0)) {
			// Rotate camera around the player's x axis at the player's position
			transform.RotateAround(player.transform.position, player.transform.right, xRot);
		}

		// HORIZONTAL CAMERA MOVEMENT
		// Horizontal movement rotates the actual playermodel so that powers will always be cast from the "front"

		// Get player input and modify by horizontal sensitivity
		float yRot = player.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityHor * Time.deltaTime;
		// Get current player rotation
		float currentXRot = player.transform.localEulerAngles.x;
		// Modify player rotation
		player.transform.localEulerAngles = new Vector3(currentXRot, yRot, 0);

        // Vector pointing from player through camera
        Vector3 directionOfTravel = transform.position - player.transform.position;
        // Sets magnitude of vector to original camera distance
        Vector3 finalDirection = directionOfTravel.normalized * distanceFromSurface;
        // Adds the vector of correct length and magnitude to the current player position
        Vector3 targetPosition = player.transform.position + finalDirection;
        //Debug.Log(targetPosition);
        //Debug.Log(transform.position);

        //TODO: set collider size to larger than a point so it will continue detection longer
        if (lerpTowardsPlayer) {
            progress -= .05f;
        } else {
            progress += .05f;
        }
        progress = Mathf.Clamp(progress, .5f, 1);
		//commented out for testing
        //transform.position = Vector3.Lerp(player.transform.position, targetPosition, progress);
    }

    // Camera LERPing concept from http://answers.unity3d.com/questions/14693/stop-camera-from-going-trough-walls.html
    void OnTriggerStay(Collider other) {
        lerpTowardsPlayer = true;
    }

    void OnTriggerExit() {
        lerpTowardsPlayer = false;
    }

}