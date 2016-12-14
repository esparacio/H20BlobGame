using UnityEngine;
using System.Collections;

/*

ThirdPersonCamera is the script that deals with camera movement around the character. 

Written by: Patrick Lathan
(C) 2016

*/
public class ThirdPersonCamera : MonoBehaviour {

    // General settings - sensitivity
    private float sensitivityHor = 100.0f;
    private float sensitivityVert = 100.0f;
    // General settings - y rotation constraints
    private float maxAndMinVert = 2f;
    // General settings - initial distance from player
    private float cameraHeight = 3f;
    private float cameraDepth = 4f;

    // Speed that camera will return to ideal position
    // Keep this low ideally to keep it smooth
    private float cameraZoomSpeed = 4f;

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

        // Set ideal distance to initial camera distance
        idealDistance = Vector3.Distance(player.transform.position, transform.position);
        currentDistance = idealDistance;
    }

    void Update() {
        // VERTICAL CAMERA MOVEMENT
        // Vertical movement moves the camera around the playermodel in an arc within specified bounds

        // Get player input, modify by vertical sensitivity, and invert
        float xRot = (Input.GetAxis("Mouse Y") + Input.GetAxis("Joystick Y")) * sensitivityVert * Time.deltaTime * -1;

        // Limit rotation around x axis
        if (!(xRot > 0 && transform.localPosition.y <= maxAndMinVert && transform.localPosition.z > 0) && !(xRot < 0 && transform.localPosition.y <= maxAndMinVert && transform.localPosition.z < 0)) {
            // Rotate camera around the player's x axis at the player's position
            transform.RotateAround(player.transform.position, player.transform.right, xRot);
        }

        // HORIZONTAL CAMERA MOVEMENT
        // Horizontal movement rotates the actual playermodel so that powers will always be cast from the "front"

        // Get player input and modify by horizontal sensitivity
        float yRot = player.transform.localEulerAngles.y + (Input.GetAxis("Mouse X") + Input.GetAxis("Joystick X")) * sensitivityHor * Time.deltaTime;
        // Get current player rotation
        float currentXRot = player.transform.localEulerAngles.x;
        // Modify player rotation
        player.transform.localEulerAngles = new Vector3(currentXRot, yRot, 0);

        //adapted from http://wiki.unity3d.com/index.php?title=MouseOrbitImproved
        RaycastHit hit;
        if (Physics.Linecast(player.transform.position, (transform.position), out hit)) {
            string hitTag = hit.collider.gameObject.tag;
            if (hitTag != "Player" && hitTag != "seed" && hitTag != "secondCamera") {
                currentDistance = hit.distance;
            }

        } else {
            currentDistance += Time.deltaTime * cameraZoomSpeed;
        }
        currentDistance = Mathf.Clamp(currentDistance, 1, idealDistance);

        // Vector pointing from player through camera
        Vector3 directionOfTravel = transform.position - player.transform.position;
        // Sets magnitude of vector to current distance specified
        Vector3 finalDirection = directionOfTravel.normalized * currentDistance;
        // Adds the vector of correct length and magnitude to the current player position
        Vector3 targetPosition = player.transform.position + finalDirection;

        transform.position = targetPosition;
    }

}