using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobs : MonoBehaviour {
    [SerializeField] private Transform player;
    [SerializeField] private float vitesse, range, rangeAttack ;
    [SerializeField] private AnimationClip run, idle, attack;
    private Animation animationcontroller;
    private bool isAttack;
    private bool Inrange { get { return Vector3.Distance(transform.position, player.position) <= range; } }
    private bool InrangeAttack { get { return Vector3.Distance(transform.position, player.position) <= rangeAttack; } }
    [SerializeField] private float vie = 100;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float cooldown = 2f;
    private float cooldownGlobal;
    void Start () {
        animationcontroller = GetComponent<Animation>();
	}
	
	
	void Update () {
        chase();
        Attack();

    }
    private void chase()
    {
        if (Inrange && !isAttack)
        { 
            transform.LookAt(player);
            transform.Translate(Vector3.forward * vitesse * Time.deltaTime);
            animationcontroller.CrossFade(run.name);    
        }
    }
    private void Attack()
    {
        if (InrangeAttack)
        {
            if (cooldownGlobal <= Time.time)
            {
                cooldownGlobal = Time.time + cooldown;
                player.GetComponent<PlayerFight>().Gethit(damage);
            }
            transform.LookAt(player);
            animationcontroller.CrossFade(attack.name);
            isAttack = true;
        }
        else
        {
            isAttack = false;
        }
        if (!Inrange && ! isAttack)
        {
            animationcontroller.CrossFade(idle.name);
        }
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButton(1))
        {
            player.GetComponent<PlayerFight>().Target = gameObject;
        }
    }
    public void GetHit(float damage)
    {
        vie -= damage;
        if (vie<=0)
        {
            vie = 0;
        }
        Debug.Log(vie);

    }
}
