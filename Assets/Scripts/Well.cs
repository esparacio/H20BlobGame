using UnityEngine;
using System.Collections;

/*

WorldWall notifies players that they have hit the map boundary.

Written by: Elena Sparacio and Patrick Lathan
(C) 2016

*/

public class Well : MonoBehaviour {

    private float progress;
    private Vector3 startPos;
    private float distanceToMove = 120f;
    private Vector3 endPos;
    private GameObject specialSeed;

    // Perform calculations in local space to make it easier to see in editor
    void Start() {
        specialSeed = GameObject.Find("wellSeed");
        startPos = specialSeed.transform.localPosition;
        endPos = specialSeed.transform.localPosition + new Vector3(0, distanceToMove, 0);
        progress = 0;
    }

    void OnParticleCollision(GameObject particle) {
        // If a water particle collides and the specialSeed still exists, lerp it upwards
        if (particle.tag == "waterParticle" && (specialSeed)) {
            progress += .005f;
            specialSeed.transform.localPosition = Vector3.Lerp(startPos, endPos, progress);
        }
    }
}


