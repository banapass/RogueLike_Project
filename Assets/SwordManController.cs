using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SwordManController : AiController
{
    [SerializeField] private float atkRange;

    [SerializeField] private BoxCollider weaponCol;


    protected override void Awake()
    {
        base.Awake();

        enemy = GetComponent<Enemy>();
        atkRange = 2.2f;
    }
    // Update is called once per frame
    void Update()
    {
        if (!enemy.isDie)
        {
            Ai();
        }

    }
    protected override void Attack()
    {
        if (!isAttack && !isHit)
        {
            int randomNum = UnityEngine.Random.Range(1, 4);
            isAttack = true;

            nav.SetDestination(transform.position);
            anim.SetBool("isRun", false);
            anim.SetTrigger("Attack" + randomNum);

        }

    }

    private void ResetTriggers(int count)
    {
        for (int i = 1; i < count; i++)
        {
            anim.ResetTrigger("Attack" + i);
        }
    }
    protected override void ChasePlayer()
    {
        anim.SetBool("isRun", true);
        nav.SetDestination(targetTf.position);
    }
    private void Ai()
    {
        if (Vector3.Distance(transform.position, targetTf.position) <= 2.2f)
        {
            Attack();
            transform.LookAt(targetTf.position);
        }
        else if (Vector3.Distance(transform.position, targetTf.position) > 2.2f && !isAttack)
            ChasePlayer();

    }

    private void CheckAnim()
    {
        if (isAttack)
        {
            for (int i = 1; i <= 3; i++)
            {
                if (anim.EndAnim("Attack" + i, 1f))
                {
                    isAttack = false;
                }
            }
        }
    }
    private void SetAttack(int boolCheck)
    {
        bool temp = Convert.ToBoolean(boolCheck);
        if (temp)
        {
            weaponCol.enabled = true;
        }
        else
        {
            weaponCol.enabled = false;
        }
    }
    private void SetIsHit(int boolCheck)
    {
        bool temp = Convert.ToBoolean(boolCheck);
        if (temp)
        {
            isHit = true;
            ResetTriggers(4);
        }
        else
        {
            isHit = false;
        }
    }

}
