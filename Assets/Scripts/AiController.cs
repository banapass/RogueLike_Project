using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;


abstract public class AiController : MonoBehaviour
{
    [SerializeField] protected Transform targetTf;
    public Enemy enemy;
    public Animator anim;
    [SerializeField] protected NavMeshAgent nav;
    [SerializeField] protected bool isMove;
    public bool isAttack;
    public bool isHit;
    protected bool isSpawn;



    virtual protected void Awake()
    {

        enemy = GetComponent<Enemy>();
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();

    }
    virtual protected void Start()
    {
        targetTf = GameObject.FindGameObjectWithTag("Player").transform;
    }
    abstract protected void ChasePlayer();
    abstract protected void Attack();
    protected void IsAttackOnOff(int boolCheck)
    {
        bool temp = Convert.ToBoolean(boolCheck);
        if (temp)
        {
            isAttack = true;
        }
        else
        {
            isAttack = false;
        }
    }
    protected void SetSpawn(int boolCheck)
    {
        bool temp = Convert.ToBoolean(boolCheck);
        isSpawn = temp;
    }


}
