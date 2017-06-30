using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight : MonoBehaviour {
    private GameObject target;
    public GameObject Target { get { return target;  } set { target = value; } }
    [SerializeField] private float vie = 200;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float rangeAttack = 2f;
    private Animator anim;
    private bool isAttack;
    public bool AutoAttack { get; set; }
    public bool IsAttack { get { return isAttack; }set { isAttack = value; } }
    public bool InRangeAttack { get { return Vector3.Distance(transform.position, target.transform.position) < rangeAttack; } }
    

    void Start ()
    {
        anim = GetComponent<Animator>();
	}
	

	void Update () {
        Attack();
        if (isAttack)
        {
            anim.SetBool("Idle", false);
        }
		
	}
    private void Attack()
    {
        if (target != null && InRangeAttack && !isAttack && AutoAttack)
        {
            isAttack = true;
            StartCoroutine(RotationPlayer());
            anim.SetBool("Attack", true);
            anim.SetBool("run", false);
        }
    }
    
    public void Hit()
    {
        if (target != null)
        {
            target.GetComponent<Mobs>().GetHit(damage);
        }
        
    }
    public void EndAnimation()
    {
        isAttack = false;
        anim.SetBool("Attack", false);
        anim.SetBool("Idle", true);
    }
    public void Gethit(float damage)
    {
        vie -= damage;
        if (vie <= 0)
        {
            vie = 0;
        }
        Debug.Log(vie);
    }
    private IEnumerator RotationPlayer()
    {
        while (isAttack)
        {
            if (target != null)
            {
                Quaternion newRotation = Quaternion.LookRotation(target.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 0.2f);
                yield return null;
            }
            yield return null;
               
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeAttack);
    }
}
