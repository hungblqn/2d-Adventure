using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Creep
{
    public GameObject[] getHitEffects;
    void Start()
    {
        Init(500,0, 0, 0);
    }
    void Update()
    {
        base.Die();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerNormalAttackArea"))
        {
            hp -= collision.GetComponentInParent<Character>().damage;
            Debug.Log("Current creep hit point: " + hp);
            GameObject hitEffect = Instantiate(getHitEffects[0],transform.position + new Vector3(0,0,-1),transform.rotation);
            hitEffect.transform.SetParent(gameObject.transform);
            collision.gameObject.SetActive(false);
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
