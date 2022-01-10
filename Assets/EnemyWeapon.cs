using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyWeapon : MonoBehaviour
{

    [SerializeField] private Enemy enemy;
    [SerializeField] private Transform hitPoint;
    private void Start()
    {
        hitPoint = transform.GetChild(0).transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            other.GetComponent<Player>().OnHit(enemy, hitPoint);
            GetComponent<BoxCollider>().enabled = false;
        }
    }

}
