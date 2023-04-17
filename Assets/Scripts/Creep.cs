using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creep : MonoBehaviour
{
    public float hp { get; set; }
    public float maxHp { get; set; }
    public float exp { get; set; }
    public float damage { get; private set; }
    public float movementSpeed { get; private set; }
    public bool inverseBody { get; set; }
    public bool isMarked { get; set; }
    public void Init(float hp,float exp,float damage,float movementSpeed)
    {
        this.hp = hp;
        this.maxHp = hp;
        this.exp = exp;
        this.damage = damage;
        this.movementSpeed = movementSpeed;
        inverseBody = false;
        isMarked = false;
    }
    public virtual void Die()
    {
        if (hp <= 0) Destroy(gameObject);
    }
}
