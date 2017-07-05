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
            if (target != null)
            {
                if (target.GetComponent<Mobs>().IsTarget)
                {
                    
                    if (fight.InRangeAttack && MoveAuto)
                    {
                        animator.SetBool("Attack", true);
                        fight.IsAttack = false;
                        return position = transform.position;
                    }
                    animator.SetBool("run", true);
                    animator.SetBool("Idle", false);
                    fight.IsAttack = false;
                    fight.AutoAttack = true;
                    MoveAuto = true;
                    return position = target.transform.position;
                }
                else
                {
                    
                    fight.Target = null;
                    fight.IsAttack = true;
                    fight.AutoAttack = false;
                    animator.SetBool("Attack", false);
                    animator.SetBool("run", true);
                    RaycastHit hit2;
                    Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray2, out hit2, Mathf.Infinity, layer))
                    {
                        return position = hit2.point;
                    }
                }
            
                if (fight.IsAttack)
                {
                    
                    animator.SetBool("Attack", false);
                    animator.SetBool("run", true);
                    return position = transform.position;
                }
            }

            fight.IsAttack = false;
            fight.AutoAttack = false;
            MoveAuto = false;
            animator.SetBool("run",true);
            animator.SetBool("Attack", false);
            

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                return position = hit.point;
            }

        }

        return position;
      
    }
}
