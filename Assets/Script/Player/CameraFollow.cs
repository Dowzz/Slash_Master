using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float distance, Height;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        TargetFollow();
	}
    private void TargetFollow()
    {
        transform.position = new Vector3(target.position.x, target.position.y + Height, target.position.z - distance);
        transform.LookAt(target);
    }
}
