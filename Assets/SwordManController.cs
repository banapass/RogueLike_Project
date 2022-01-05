using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManController : AiController
{
    bool isAttack;



    // Update is called once per frame
    void Update()
    {
        Debug.Log(isAttack);
        CheckAnim();
        Ai();
    }
    protected override void Attack()
    {
        if (!isAttack)
        {
            int randomNum = Random.Range(1, 4);
            Debug.Log(randomNum);
            switch (randomNum)
            {
                case 1:
                    anim.SetTrigger("Attack" + randomNum);
                    break;
                case 2:
                    anim.SetTrigger("Attack" + randomNum);
                    break;
                case 3:
                    anim.SetTrigger("Attack" + randomNum);
                    break;
            }
            isAttack = true;
        }
    }

    protected override void ChasePlayer()
    {
        anim.SetBool("isRun", true);
        nav.SetDestination(targetTf.position);
    }
    private void Ai()
    {
        if (Vector3.Distance(transform.position, targetTf.position) < 2.2f)
        {
            nav.SetDestination(transform.position);
            anim.SetBool("isRun", false);
            Attack();
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


}
