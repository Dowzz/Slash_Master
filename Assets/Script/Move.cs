using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    private PlayerFight fight;
    private Vector3 position;
    [SerializeField] private float vitesse;
    private Animator animator;
    [SerializeField] private LayerMask layer;
    private bool MoveAuto;
    private Mobs mob;
	
	void Start () {
        animator = GetComponent<Animator>();
        position = transform.position;
        fight = GetComponent<PlayerFight>();
	}
	
	void Update () {
        MoveToPotision();
    }

    private void MoveToPotision()
    {
        if (Vector3.Distance(transform.position, Position()) > 0f)
        {
            if (!fight.IsAttack)
            {
                animator.SetBool("run", true);
                animator.SetBool("Idle", false);
                animator.SetBool("Attack", false);
                Quaternion newRotation = Quaternion.LookRotation(Position() - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 0.1f);
                transform.position = Vector3.MoveTowards(transform.position, Position(), vitesse * Time.deltaTime); 
            }            
            Debug.Log(fight.Target);
        }
        else
        {
            animator.SetBool("run", false);
            animator.SetBool("Idle", false);
        }
       
    }
    private Vector3 Position()
    {
        GameObject target = fight.Target;

        if (Input.GetMouseButton(0))
        {
            animator.SetBool("Attack", false);
            animator.SetBool("Idle", false);

            fight.IsAttack = false;
            MoveAuto = false;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                return position = hit.point;
            }
        }
        if (target != null)
        {
            if (target.GetComponent<Mobs>().IsTarget && Input.GetMouseButton(0))
            {
                animator.SetBool("Attack", true);
                animator.SetBool("Idle", false);
                fight.IsAttack = false;
                MoveAuto = true;
                return position = target.transform.position;
            }
            if (fight.InRangeAttack && MoveAuto)
            {
                return position = transform.position;
            }
            if (fight.IsAttack)
            {
                return position = transform.position;
            }
        }
        return position;
      
    }
}
