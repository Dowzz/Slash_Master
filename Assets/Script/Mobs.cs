using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mobs : MonoBehaviour {

    [SerializeField] private Transform player;

    [SerializeField] private float vitesse, range, rangeAttack ;

    [SerializeField] private AnimationClip run, idle, attack, die;

    private Animation animationcontroller;
    private bool isAttack;  
    private bool Inrange { get { return Vector3.Distance(transform.position, player.position) <= range; } }
    private bool InrangeAttack { get { return Vector3.Distance(transform.position, player.position) <= rangeAttack; } }
    private float vie;
    public bool IsTarget { get; set; }

    [SerializeField] private float MaxHealth = 100f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float cooldown = 2f;

    [SerializeField] public GameObject HealthBarMob;
    [SerializeField] private GameObject valueBarMob;
    private float cooldownGlobal;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeAttack);
    }

    void Start () {
        HealthBarMob.SetActive(false);
        animationcontroller = GetComponent<Animation>();
        vie = MaxHealth;
	}
	
	
	void Update () {
        chase();
        Attack();
        manageHealthBar();

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
        IsTarget = true;
        if (Input.GetMouseButton(0))
        {
            player.GetComponent<PlayerFight>().Target = gameObject;
            HealthBarMob.SetActive(true);
        }
    }
    private void OnMouseExit()
    {
        IsTarget = false;
        
    }
    private void manageHealthBar()
    {
        if (player.GetComponent<PlayerFight>().Target == gameObject)
        {
            valueBarMob.GetComponent<Image>().fillAmount = Mathf.Lerp(valueBarMob.GetComponent<Image>().fillAmount, vie / MaxHealth, 0.2f);
        }
    }
    public void GetHit(float damage)
    {
        vie -= damage;
        if (Inrange && vie<=0)
        {   
            HealthBarMob.SetActive(false);
            Destroy(gameObject, 0.5f);
        }
    }
}
