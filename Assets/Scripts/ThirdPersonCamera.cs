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
    // General settings - initial distance from player
    private float cameraHeight = 3f;
    private float cameraDepth = 4f;

    // Speed that camera will return to ideal position
    private float cameraZoomSpeed = 4f;
    private float progress = 1f;

    // Initial camera distance from player
    private float idealDistance;
    // Camera distance adjusted by collisions
    private float currentDistance;

    private GameObject player;

    void Start() {
        // Hide cursor
        Cursor.lockState = CursorLockMode.Locked;

        // Set target to the player
        player = GameObject.FindGameObjectsWithTag("Player")[0];

        // Set initial camera position
        transform.localPosition = new Vector3(0, cameraDepth, cameraHeight);

        // Point at target
        transform.LookAt(player.transform);

        idealDistance = Vector3.Distance(player.transform.position, transform.position);

        currentDistance = idealDistance;
    }

    void Update() {
        // VERTICAL CAMERA MOVEMENT
        // Vertical movement moves the camera around the playermodel in an arc within specified bounds

        // Get player input, modify by vertical sensitivity, and invert
        float xRot = Input.GetAxis("Mouse Y") * sensitivityVert * Time.deltaTime * -1;

        //TODO use maxvert and minvert instead
        //if ((transform.localPosition.z > 0 || xRot > 0) && (transform.localPosition.z < 3 || xRot < 0)) {
        // Rotate camera around the player's x axis at the player's position
        //Debug.Log(transform.localPosition.x + " " + transform.localPosition.y + " " + transform.localPosition.z);
            transform.RotateAround(player.transform.position, player.transform.right, xRot);
        //}

        // HORIZONTAL CAMERA MOVEMENT
        // Horizontal movement rotates the actual playermodel so that powers will always be cast from the "front"

        // Get player input and modify by horizontal sensitivity
        float yRot = player.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityHor * Time.deltaTime;
        // Get current player rotation
        float currentXRot = player.transform.localEulerAngles.x;
        // Modify player rotation
        player.transform.localEulerAngles = new Vector3(currentXRot, yRot, 0);

        //float distance = distanceFromSurface;

        RaycastHit hit;
        if (Physics.Linecast(player.transform.position, (transform.position), out hit)) {
            string hitTag = hit.collider.gameObject.tag;
            if (hitTag != "Player" && hitTag != "vapor" && hitTag != "water") {
                //Debug.Log(hit.collider.gameObject.tag);
                currentDistance = hit.distance;
            }

            //distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

        } else {
            currentDistance += Time.deltaTime * cameraZoomSpeed;
        }
        currentDistance = Mathf.Clamp(currentDistance, 1, idealDistance);

        // Vector pointing from player through camera
        Vector3 directionOfTravel = transform.position - player.transform.position;
        // Sets magnitude of vector to original camera distance
        Vector3 finalDirection = directionOfTravel.normalized * currentDistance;
        // Adds the vector of correct length and magnitude to the current player position
        Vector3 targetPosition = player.transform.position + finalDirection;
        //Debug.Log(targetPosition);
        //Debug.Log(transform.position);

        transform.position = targetPosition;
    }

}