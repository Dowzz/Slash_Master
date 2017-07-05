using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFight : MonoBehaviour {
    private GameObject target;
    public GameObject Target { get { return target; } set { target = value; } }
    [SerializeField] private float maxvie  { get; set; }
    [SerializeField] private float vie  { get; set; }
    [SerializeField] private float damage = 10f;
    [SerializeField] private float rangeAttack = 2f;
    private Animator anim;
    private bool isAttack;
    public bool AutoAttack { get; set; }
    public bool IsAttack { get { return isAttack; }set { isAttack = value; } }
    public bool InRangeAttack { get { return Vector3.Distance(transform.position, target.transform.position) < rangeAttack; } }
    public Slider healthbar;
    

    void Start ()
    {
        anim = GetComponent<Animator>();
        maxvie = 200;
        vie = maxvie;
        healthbar.value = CalculateHealth();
	}
	

	void Update () {
        Attack();
        if (isAttack)
        {
            anim.SetBool("Attack", true);
        }
		
	}
    private void Attack()
    {
        if (target != null && InRangeAttack && !isAttack && AutoAttack)
        {
            isAttack = true;
            StartCoroutine(RotationPlayer());
            anim.SetBool("Attack", true);
            anim.SetBool("Idle", false);
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
    float CalculateHealth()
    {
        return vie / maxvie;
    }
    public void EndAnimation()
    {
        isAttack = false;
        anim.SetBool("Attack", false);
        


    }
    public void Gethit(float damage)
    {
        vie -= damage;
        healthbar.value = CalculateHealth();
        if (vie <= 0)
        {
            vie = 0;
        }
        
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
