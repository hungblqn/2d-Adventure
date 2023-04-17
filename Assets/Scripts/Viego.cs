using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Viego : Character
{
    private Rigidbody2D rb;
    private Animator anim;
    public GameObject[] attackAreas;
    public GameObject[] getHitEffects;
    public GameObject groundCheck;
    public LayerMask groundMask;

    private float attackCooldown = 0;
    private float attackTimeStamp = 0;
    void Start()
    {
        Init(100, 50, 1000, 5);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        FaceDirection();
        Attack();
        Crouch();
        Dodge();
        Guard();
        Injured();
        Taunt();
        Jump();
        Dash();
        LevelUp();
    }
    public void LevelUp()
    {
        if (exp >= maxExp)
        {
            exp -= maxExp;
            maxExp *= 1.1f;
            maxHp *= 1.1f;
            hp = maxHp;
            damage *= 1.1f;
            level++;
        }
    }
    public void Dash()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(DashAction());
            if (inverseBody)
            {
                rb.AddForce(Vector2.left * 10,ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
            }
        }
    }
    public override void Move()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            anim.SetBool("walk", true);
        }
        else anim.SetBool("walk", false);
        if (Input.GetKey(KeyCode.A) && !anim.GetBool("guard") && !anim.GetBool("crouch"))
        {
            transform.Translate(Vector2.left * movementSpeed * Time.deltaTime, Space.World);
            inverseBody = true;
        }
        if (Input.GetKey(KeyCode.D) && !anim.GetBool("guard") && !anim.GetBool("crouch"))
        {
            transform.Translate(Vector2.right * movementSpeed * Time.deltaTime, Space.World);
            inverseBody = false;
        }
    }
    public override void Attack()
    {
        if (canAttack() && !anim.GetBool("jump") && !anim.GetBool("guard") && !anim.GetBool("injured") && !anim.GetBool("dodge"))
        {
            if (Input.GetKey(KeyCode.J) && anim.GetBool("dash"))
            {
                StartCoroutine(NeutralAttack());
                StartCoroutine(NeutralAttackAttackArea());
            }
            else if (Input.GetKey(KeyCode.J) && Input.GetKey(KeyCode.S))
            {
                StartCoroutine(DownTilt());
                StartCoroutine(DownTiltAttackArea());
            }
            else if (Input.GetKey(KeyCode.J) && Input.GetKey(KeyCode.W))
            {
                StartCoroutine(UpTilt());
                StartCoroutine(UpTiltAttackArea());
            }
            else if (Input.GetKeyDown(KeyCode.J))
            {
                StartCoroutine(ForwardTilt());
                StartCoroutine(ForwardTiltAttackArea());
            }


            if (Input.GetKey(KeyCode.U) && Input.GetKey(KeyCode.S))
            {
                StartCoroutine(DownStrong());
                StartCoroutine(DownStrongAttackArea());
            }
            else if (Input.GetKey(KeyCode.U) && Input.GetKey(KeyCode.W))
            {
                StartCoroutine(UpStrong());
                StartCoroutine(UpStrongAttackArea());
            }
            else if (Input.GetKeyDown(KeyCode.U))
            {
                StartCoroutine(ForwardStrong());
                StartCoroutine(ForwardStrongAttackArea());
            }

            if (Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.S))
            {
                StartCoroutine(DownAerial());
                StartCoroutine(DownAerialAttackArea());
            }
            else if (Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.W))
            {
                StartCoroutine(UpAerial());
                StartCoroutine(UpAerialAttackArea());
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                StartCoroutine(ForwardAerial());
                StartCoroutine(ForwardAerialAttackArea());
            }

            if (Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.S))
            {
                StartCoroutine(DownSpecial());
                StartCoroutine(DownSpecialAttackArea());
            }
            else if (Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.W))
            {
                StartCoroutine(UpSpecial());
                StartCoroutine(UpSpecialAttackArea());
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                StartCoroutine(ForwardSpecial());
                StartCoroutine(ForwardSpecialAttackArea());
            }
        }
        else
        {
            
        }
    }
    bool canAttack()
    {
        return (Time.time - attackTimeStamp >= attackCooldown) ? true : false;
    }
    IEnumerator DashAction()
    {
        anim.SetBool("dash", true);
        yield return new WaitForSeconds(0.432f);
        anim.SetBool("dash", false);
    }
    public void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && CanJump())
        {
            rb.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
        }
        if (!CanJump())
        {
            anim.SetBool("jump", true);
        }
        else
        {
            anim.SetBool("jump", false);
        }
    }
    bool CanJump()
    {
        return Physics2D.OverlapCircle(groundCheck.transform.position, 0.1f, groundMask);
    }
    public void Taunt()
    {
        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Alpha2))
        {
            StartCoroutine(TauntAction());
        }
    }
    IEnumerator TauntAction()
    {
        anim.SetBool("taunt", true);
        yield return new WaitForSeconds(1.016f);
        anim.SetBool("taunt", false);
    }
    public void Injured()
    {
        if (hp <= 0) anim.SetBool("injured", true);
    }
    public void Guard()
    {
        if (Input.GetKey(KeyCode.L))
        {
            anim.SetBool("guard", true);
            isBlocking = true;
        }
        else
        {
            anim.SetBool("guard", false);
            isBlocking = false;
        }
    }
    public void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            anim.SetBool("crouch", true);
        }
        else anim.SetBool("crouch", false);
    }
    public void Dodge()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            StartCoroutine(DodgeAction());
        }
    }
    IEnumerator DodgeAction()
    {
        anim.SetBool("dodge", true);
        yield return new WaitForSeconds(1.366f);
        anim.SetBool("dodge", false);
    }
    public override void FaceDirection()
    {
        if (inverseBody)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    
    IEnumerator ForwardAerial()
    {
        attackTimeStamp = Time.time;
        attackCooldown = 1.016f;
        anim.SetBool("forwardaerial", true);
        yield return new WaitForSeconds(1.016f);
        anim.SetBool("forwardaerial", false);
    }
    IEnumerator ForwardAerialAttackArea()
    {
        yield return new WaitForSeconds(0.508f);
        attackAreas[0].SetActive(true);
        yield return new WaitForSeconds(0.02f);
        attackAreas[0].SetActive(false);
    }
    IEnumerator ForwardSpecial()
    {
        attackTimeStamp = Time.time;
        attackCooldown = 2.016f;
        anim.SetBool("forwardspecial", true);
        yield return new WaitForSeconds(2.016f);
        anim.SetBool("forwardspecial", false);
    }
    IEnumerator ForwardSpecialAttackArea()
    {
        yield return new WaitForSeconds(1.512f);
        attackAreas[0].SetActive(true);
        yield return new WaitForSeconds(0.02f);
        attackAreas[0].SetActive(false);
    }
    IEnumerator ForwardStrong()
    {
        attackTimeStamp = Time.time;
        attackCooldown = 0.849f;
        anim.SetBool("forwardstrong", true);
        yield return new WaitForSeconds(0.849f);
        anim.SetBool("forwardstrong", false);
    }
    IEnumerator ForwardStrongAttackArea()
    {
        yield return new WaitForSeconds(0.666f);
        attackAreas[0].SetActive(true);
        yield return new WaitForSeconds(0.02f);
        attackAreas[0].SetActive(false);
    }
    IEnumerator ForwardTilt()
    {
        anim.SetBool("forwardtilt", true);
        attackTimeStamp = Time.time;
        attackCooldown = 0.849f;
        yield return new WaitForSeconds(0.849f);
        anim.SetBool("forwardtilt", false);
    }
    IEnumerator ForwardTiltAttackArea()
    {
        yield return new WaitForSeconds(0.3396f);
        attackAreas[0].SetActive(true);
        yield return new WaitForSeconds(0.02f);
        attackAreas[0].SetActive(false);
    }
    IEnumerator DownAerial()
    {
        attackTimeStamp = Time.time;
        attackCooldown = 0.849f;
        anim.SetBool("downaerial", true);
        yield return new WaitForSeconds(0.849f);
        anim.SetBool("downaerial", false);
    }
    IEnumerator DownAerialAttackArea()
    {
        yield return new WaitForSeconds(0.3396f);
        attackAreas[0].SetActive(true);
        yield return new WaitForSeconds(0.02f);
        attackAreas[0].SetActive(false);
    }
    IEnumerator DownSpecial()
    {
        attackTimeStamp = Time.time;
        attackCooldown = 0.516f;
        anim.SetBool("downspecial", true);
        yield return new WaitForSeconds(0.516f);
        anim.SetBool("downspecial", false);
    }
    IEnumerator DownSpecialAttackArea()
    {
        yield return new WaitForSeconds(0.258f);
        attackAreas[0].SetActive(true);
        yield return new WaitForSeconds(0.02f);
        attackAreas[0].SetActive(false);
    }
    IEnumerator DownStrong()
    {
        attackTimeStamp = Time.time;
        attackCooldown = 1.016f;
        anim.SetBool("downstrong", true);
        yield return new WaitForSeconds(1.016f);
        anim.SetBool("downstrong", false);
    }
    IEnumerator DownStrongAttackArea()
    {
        yield return new WaitForSeconds(0.677f);
        attackAreas[0].SetActive(true);
        yield return new WaitForSeconds(0.02f);
        attackAreas[0].SetActive(false);
    }
    IEnumerator DownTilt()
    {
        attackTimeStamp = Time.time;
        attackCooldown = 1.032f;
        anim.SetBool("downtilt", true);
        yield return new WaitForSeconds(1.032f);
        anim.SetBool("downtilt", false);
    }
    IEnumerator DownTiltAttackArea()
    {
        yield return new WaitForSeconds(0.344f);
        attackAreas[0].SetActive(true);
        yield return new WaitForSeconds(0.02f);
        attackAreas[0].SetActive(false);
    }
    IEnumerator BackAerial()
    {
        anim.SetBool("backaerial", true);
        yield return new WaitForSeconds(0.849f);
        anim.SetBool("backaerial", false);
    }
    IEnumerator NeutralAerial()
    {
        attackTimeStamp = Time.time;
        attackCooldown = 0.182f;
        anim.SetBool("neutralaerial", true);
        yield return new WaitForSeconds(0.182f);
        anim.SetBool("neutralaerial", false);
    }
    IEnumerator NeutralAerialAttackArea()
    {
        yield return new WaitForSeconds(0);
        attackAreas[0].SetActive(true);
        yield return new WaitForSeconds(0.02f);
        attackAreas[0].SetActive(false);
    }
    IEnumerator NeutralAttack()
    {
        anim.SetBool("neutralattack", true);
        attackTimeStamp = Time.time;
        attackCooldown = 0.682f;
        yield return new WaitForSeconds(0.682f);
        anim.SetBool("neutralattack", false);
    }
    IEnumerator NeutralAttackAttackArea()
    {
        yield return new WaitForSeconds(0.341f);
        attackAreas[0].SetActive(true);
        yield return new WaitForSeconds(0.02f);
        attackAreas[0].SetActive(false);
    }
    IEnumerator UpAerial()
    {
        attackTimeStamp = Time.time;
        attackCooldown = 1.349f;
        anim.SetBool("upaerial", true);
        yield return new WaitForSeconds(1.349f);
        anim.SetBool("upaerial", false);
    }
    IEnumerator UpAerialAttackArea()
    {
        yield return new WaitForSeconds(0.843125f);
        attackAreas[0].SetActive(true);
        yield return new WaitForSeconds(0.02f);
        attackAreas[0].SetActive(false);
    }
    IEnumerator UpSpecial()
    {
        attackTimeStamp = Time.time;
        attackCooldown = 1.349f;
        anim.SetBool("upspecial", true);
        yield return new WaitForSeconds(1.349f);
        anim.SetBool("upspecial", false);
    }
    IEnumerator UpSpecialAttackArea()
    {
        yield return new WaitForSeconds(1.01175f);
        attackAreas[0].SetActive(true);
        yield return new WaitForSeconds(0.02f);
        attackAreas[0].SetActive(false);
    }
    IEnumerator UpStrong()
    {
        attackTimeStamp = Time.time;
        attackCooldown = 1.349f;
        anim.SetBool("upstrong", true);
        yield return new WaitForSeconds(1.349f);
        anim.SetBool("upstrong", false);
    }
    IEnumerator UpStrongAttackArea()
    {
        yield return new WaitForSeconds(0.843125f);
        attackAreas[0].SetActive(true);
        yield return new WaitForSeconds(0.02f);
        attackAreas[0].SetActive(false);
    }
    IEnumerator UpTilt()
    {
        
        attackTimeStamp = Time.time;
        attackCooldown = 1.016f;
        anim.SetBool("uptilt", true);
        yield return new WaitForSeconds(1.016f);
        anim.SetBool("uptilt", false);
        
    }
    IEnumerator UpTiltAttackArea()
    {
        yield return new WaitForSeconds(0.508f);
        attackAreas[0].SetActive(true);
        yield return new WaitForSeconds(0.02f);
        attackAreas[0].SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SkeletonNormalAttackArea"))
        {
            if (!isBlocking)
            {
                hp -= collision.GetComponentInParent<Skeleton>().damage;
                Debug.Log("Current player hit point: " + hp);
                Instantiate(getHitEffects[0], transform.position + new Vector3(0, 0, -1), transform.rotation);
            }
            else
            {
                hp -= collision.GetComponentInParent<Skeleton>().damage/3;
                Debug.Log("Current player hit point: " + hp);
                Instantiate(getHitEffects[0], transform.position + new Vector3(0, 0, -1), transform.rotation);
            }
        }
    }
}
