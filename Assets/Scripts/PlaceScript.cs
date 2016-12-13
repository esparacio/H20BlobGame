using UnityEngine;
using System.Collections;

/*

PlaceScript is attached to water and vapor places created by planting seeds

By: Nathan Young and Elena Sparacio

image credits: https://upload.wikimedia.org/wikipedia/commons/c/c4/Fire_Texture_01.png

*/


public class PlaceScript : MonoBehaviour {

    BlobPlayer blobPlayer;

    // Use this for initialization
    void Start() {
        GameObject actualBlob = GameObject.Find("ActualBlob");
        blobPlayer = actualBlob.GetComponent<BlobPlayer>();
    }

    //when the player enters the area, change the powers of the player
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            blobPlayer.SetState(tag);
        }
    }

    //note, when destroyed, must say outside circle
    void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            blobPlayer.SetState("ice");
        }
    }
}
