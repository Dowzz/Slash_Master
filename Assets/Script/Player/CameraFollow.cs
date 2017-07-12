using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float distance, Height;
    float zoomSpeed = 25f;
    Camera playerCam;

    // Use this for initialization
    void Start () {
        playerCam = GetComponent<Camera>();
		
	}
	
	// Update is called once per frame
	void Update () {
        TargetFollow();
        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            playerCam.fieldOfView -= scroll * zoomSpeed;
            playerCam.fieldOfView =  Mathf.Clamp(playerCam.fieldOfView, 50, 120);
        }
	}
    private void TargetFollow()
    {
        transform.position = new Vector3(target.position.x, target.position.y + Height, target.position.z - distance);
        transform.LookAt(target);
    }
}
