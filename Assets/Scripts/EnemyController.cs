using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : AiController
{

    protected override void Awake()
    {
        base.Awake();

    }
    protected override void Start()
    {
        base.Start();
        anim.SetTrigger("Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector3.Distance(transform.position, targetTf.position));
        if (Vector3.Distance(transform.position, targetTf.position) > 20)
            ChasePlayer();
        else
            Attack();

        if (anim.SetAnim("Attack", 0.7f, 0.9f))
        {

            Debug.Log("AttackEnd");
        }
        if (anim.EndAnim("Attack", 1))
        {
            Debug.Log("Player");
        }

    }

    protected override void ChasePlayer()
    {
        isMove = true;
        anim.SetBool("isMove", isMove);
        nav.SetDestination(targetTf.position);
    }

    protected override void Attack()
    {
        if (!enemy.isDie)
        {
            isMove = false;
            transform.LookAt(targetTf.position);
            nav.SetDestination(transform.position);
            anim.SetTrigger("Attack1");
            anim.SetBool("isMove", isMove);
        }
    }
    protected void Die()
    {

    }
}
