using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossController : AiController
{
    [SerializeField] private GameObject[] enemys;
    [SerializeField] private float summonDelay;
    [SerializeField] private float coolTime;
    [SerializeField] private BoxCollider weaponCol;
    [SerializeField] private float atkRange;
    bool isCoolTime = true;
    bool isUsingSkill;

    protected override void Start()
    {

        base.Start();
        StartCoroutine(SetStart());
        StartCoroutine(BattleStartCheck());
        StartCoroutine(SummonCoolTime());
    }
    void Update()
    {
        if (!enemy.isDie && isSpawn)
            Ai();
        Debug.Log("isUsing : " + isUsingSkill + " isCoolTime : " + isCoolTime);
        Debug.Log("isSpawn : " + isSpawn);

    }
    protected override void ChasePlayer()
    {
        anim.SetBool("isRun", true);
        nav.SetDestination(targetTf.position);
    }
    protected override void Attack()
    {
        if (!isAttack)
        {
            int randomNum = UnityEngine.Random.Range(1, 3);
            isAttack = true;
            nav.SetDestination(transform.position);
            anim.SetTrigger("Attack" + randomNum);
        }
    }
    private void Ai()
    {
        if (isCoolTime && !isUsingSkill)
        {

            if (Vector3.Distance(targetTf.position, transform.position) < atkRange)
            {
                Attack();
                transform.LookAt(targetTf.position);

            }
            else if (Vector3.Distance(targetTf.position, transform.position) > atkRange && !isAttack)
            {
                ChasePlayer();
            }
        }
        else if (!isCoolTime && !isUsingSkill && !isAttack)
        {
            anim.SetTrigger("Skill1");
            isUsingSkill = true;
            isCoolTime = true;
        }
    }
    // 쿨타임 체크용 코루틴
    private IEnumerator SummonCoolTime()
    {
        
        while (isCoolTime)
        {
            if (isSpawn)
            {
                summonDelay += Time.deltaTime;
                if (summonDelay >= coolTime)
                {
                    isCoolTime = false;
                    summonDelay = 0;
                    break;
                }
            }
            yield return null;
        }
    }
    // 양옆 몬스터 소환
    private void SummonEnemy()
    {

        Instantiate(enemys[UnityEngine.Random.Range(0, enemys.Length)], (transform.localPosition + Vector3.left * 2f), transform.rotation);
        Instantiate(enemys[UnityEngine.Random.Range(0, enemys.Length)], (transform.localPosition + Vector3.right * 2f), transform.rotation);
        nav.SetDestination(transform.position);
        StartCoroutine(SummonCoolTime());

    }
    private void SetAttack(int boolCheck)
    {
        bool temp = Convert.ToBoolean(boolCheck);
        weaponCol.enabled = temp;
        isAttack = temp;
    }
    private void UsingSkillCheck(int boolCheck)
    {
        bool temp = Convert.ToBoolean(boolCheck);
        isUsingSkill = temp;
        Debug.Log("isUsing");
    }
    private IEnumerator SetStart()
    {
        anim.SetTrigger("Spawn");
        yield return new WaitForSeconds(0.5f);
        anim.speed = 0;

    }
    private IEnumerator BattleStartCheck()
    {
        while (true)
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, 5f, enemy.layerMask);
            if (cols.Length > 0)
            {
                anim.speed = 1;
                break;
            }
            yield return null;
        }

    }
    private void DeadAnimationEvent()
    {
        this.gameObject.tag = "Untagged";
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
}
