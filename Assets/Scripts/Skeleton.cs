using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skeleton : Creep
{
    public GameObject[] attackAreas;
    public GameObject[] getHitEffects;
    public LayerMask playerLayerMask;
    public Image SkeletonHealthBar;
    private GameObject player;
    private Animator anim;
    private float attackCooldown = 3;
    private float attackTimeStamp;
    void Start()
    {
        Init(100, 100, 5, 3);
        anim = GetComponent<Animator>();
        player = GameObject.Find("Viego");
    }

    // Update is called once per frame
    void Update()
    {
        Die();
        if (!anim.GetBool("dead"))
        {
            Attack();
            Walk();
        }
        UpdateHealthBar();
        FaceDirection();
    }
    public void FaceDirection()
    {
        if (player.transform.position.x > transform.position.x)
        {
            inverseBody = true;
        }
        else inverseBody = false;
        if (inverseBody)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    void UpdateHealthBar()
    {
        SkeletonHealthBar.fillAmount = hp / maxHp;
    }
    public void Walk()
    {
        if(playerIsNear() && !playerInAttackRange())
        {
            if(player.transform.position.x < transform.position.x)
            {
                transform.Translate(Vector2.left * movementSpeed * Time.deltaTime, Space.World);
            }
            else
            {
                transform.Translate(Vector2.right * movementSpeed * Time.deltaTime, Space.World);
            }
        }
    }
    public void Attack()
    {
        if (playerInAttackRange() && canAttack())
        {
            StartCoroutine(AttackAction());
        }
    }
    IEnumerator AttackAction()
    {
        anim.SetBool("attack", true);
        attackTimeStamp = Time.time;
        attackAreas[0].SetActive(true);
        yield return new WaitForSeconds(0.682f);
        anim.SetBool("attack", false);
        attackAreas[0].SetActive(false);
    }
    bool canAttack()
    {
        return (Time.time - attackTimeStamp >= attackCooldown) ? true : false;
    }
    bool playerIsNear()
    {
        return Physics2D.OverlapCircle(transform.position, 10, playerLayerMask);
    }
    bool playerInAttackRange()
    {
        return Physics2D.OverlapCircle(transform.position, 3, playerLayerMask);
    }
    public override void Die()
    {
        if (hp <= 0)
        {
            StartCoroutine(DieAction());
        }
    }
    IEnumerator DieAction()
    {
        anim.SetBool("dead", true);
        yield return new WaitForSeconds(0.849f);
        anim.SetBool("dead", false);
        player.GetComponent<Viego>().exp += exp;
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerNormalAttackArea"))
        {
            hp -= collision.GetComponentInParent<Character>().damage;
            Debug.Log("Current creep hit point: " + hp);
            GameObject hitEffect = Instantiate(getHitEffects[0], transform.position + new Vector3(0, 0, -1), transform.rotation);
            hitEffect.transform.SetParent(gameObject.transform);
        }
        if (collision.gameObject.CompareTag("PlayerSpecialAttackArea"))
        {
            hp -= collision.GetComponentInParent<Character>().damage * 2;
            Debug.Log("Current creep hit point: " + hp);
            GameObject hitEffect = Instantiate(getHitEffects[0], transform.position + new Vector3(0, 0, -1), transform.rotation);
            hitEffect.transform.SetParent(gameObject.transform);
        }
    }
}
