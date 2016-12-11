using UnityEngine;
using System.Collections;

/*

IceCircle is the script that deals with the ice circle that is
created when the player uses their ice power.

Written by: Patrick Lathan
(C) 2016

*/
public class IceCircle : MonoBehaviour {
	
    private float horScale; //original diameter of circle
    private float vertScale; //original height of circle
    private float minScale; //scale threshold at which circle is destroyed
    private float scale; //diameter of circle as it shrinks over time
    private float startTime;
    private float speed; //speed at which circle will shrink
    private float delay; //delay in seconds before circle begins to shrink

    // Use this for initialization
    void Start () {
        horScale = transform.localScale.x;
        vertScale = transform.localScale.y;
        minScale = .25f * horScale;
        speed = 0.5f;
		//formerly 2
        delay = 5;
        startTime = Time.time + delay;
    }
	
	// Update is called once per frame - constants shrinks the circle 
	void Update () {
        if (startTime < Time.time) {
            scale = Mathf.Lerp(horScale, 0, (Time.time - startTime) * speed);
            transform.localScale = new Vector3(scale,vertScale,scale);
            if (scale < minScale) {
                Destroy(this.gameObject);
                Destroy(this);
            }
        }
    }
}
