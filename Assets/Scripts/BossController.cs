using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : AiController
{
    [SerializeField] private GameObject[] enemys;
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
        anim.SetTrigger("Attack" + randomNum);
    }
    private void SummonEnemy()
    {
        Instantiate(enemys[Random.Range(0, enemys.Length)], transform.position + Vector3.left, transform.rotation);
        Instantiate(enemys[Random.Range(0, enemys.Length)], transform.position + Vector3.right, transform.rotation);

    }
}
