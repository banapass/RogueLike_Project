using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : AiController
{

    bool isAttack;
    // Update is called once per frame
    void Update()
    {

    }
    protected override void ChasePlayer()
    {
        base.ChasePlayer();
    }
    protected override void Attack()
    {
        int randomNum = Random.Range(1, 4);
        isAttack = true;
        if (isAttack)
        {
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
            isAttack = false;
        }
    }
}
