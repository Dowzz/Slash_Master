using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    private Vector3 position;
    [SerializeField] private float vitesse;
    private Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            GetPosition();
        }
        MoveToPotision();
		
	}
    private void GetPosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            position = hit.point;
    
        }
    }
    private void MoveToPotision()
    {
        if (Vector3.Distance(transform.position, position)> 0f)
        {
            animator.SetBool("run", true);
            Quaternion newRotation = Quaternion.LookRotation(position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 0.1f);
            transform.position = Vector3.MoveTowards(transform.position, position, vitesse * Time.deltaTime);

        }
        else
        {
            animator.SetBool("run", false);
        }
       
    }
}
