using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int level { get; set; }
    public float hp { get; set; }
    public float maxHp { get; set; }
    public float exp { get; set; }
    public float maxExp { get; set; }
    public float damage { get; set; }
    public float movementSpeed { get; private set; }
    public bool inverseBody { get; set; }
    public bool isBlocking { get; set; }
    public void Init(float hp, float damage,float maxExp, float movementSpeed)
    {
        this.level = 1;
        this.hp = hp;
        this.maxHp = hp;
        this.exp = 0;
        this.maxExp = maxExp;
        this.damage = damage;
        this.movementSpeed = movementSpeed;
        this.inverseBody = false;
        this.isBlocking = false;
    }

    public virtual void FaceDirection()
    {
        Debug.Log("Face direction");
    }
    public virtual void Move()
    {
        Debug.Log("Move");
    }
    public virtual void Attack()
    {
        Debug.Log("Attack");
    }

}
