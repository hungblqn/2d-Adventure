using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public int numOfMobs;
    public float spawnRate;
    public GameObject mob;
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Skeleton").Length < numOfMobs)
        {
            SpawnMob();
        }
    }
    void SpawnMob()
    {
        Instantiate(mob,transform.position,transform.rotation);
    }
}
