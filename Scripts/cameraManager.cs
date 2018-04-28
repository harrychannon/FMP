using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour {

    public Transform target; //What the camera follows
    public float smoothing; //camera dampening; how quickly the camera follows

    private Vector3 offset; //Difference between character and camera
    private float lowY; //Camera's Y limit

	void Start ()
    {
        offset = transform.position - target.position; //Maintain the difference of the distance camera is from the player

        lowY = transform.position.y;
	}
	
	void FixedUpdate ()
    {
        Vector3 targetCamPos = target.position + offset; //Where the camera should be placed

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing*Time.deltaTime); //smoothly transitions the camera between the current position and the ideal camera position

        /*if(transform.position.y < lowY) USED FOR IF CHARACTER FALLS OFF MAP
        {
            transform.position = new Vector3(transform.position.x, lowY, transform.position.z); 
        }
        */
	}
}
