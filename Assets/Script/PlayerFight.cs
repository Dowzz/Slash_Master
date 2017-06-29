using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight : MonoBehaviour {
    private GameObject target;
    public GameObject Target { get { return Target;  } set { target = value; } }
    [SerializeField] private float vie = 200;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float cooldown = 2f;
    private float cooldownGlobal;
    private Animator anim;
    void Start () {
        anim = GetComponent<Animator>();
	}
	

	void Update () {
        Attack();
        Debug.Log(target);
		
	}
    private void Attack()
    {
        if (Input.GetKey(KeyCode.Space)&& target != null && cooldownGlobal <= Time.time)
        {
            cooldownGlobal = Time.time + cooldown;
            anim.SetBool("Attack", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Run", false);

        }
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
}
