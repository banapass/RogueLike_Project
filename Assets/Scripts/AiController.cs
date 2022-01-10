using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;


public class AiController : MonoBehaviour
{
    [SerializeField] protected Transform targetTf;
    public Enemy enemy;
    public Animator anim;
    [SerializeField] protected NavMeshAgent nav;
    [SerializeField] protected bool isMove;
    public bool isAttack;
    public bool isHit;

    #region 적 Ai로직 정리
    // 근접 적 : 일반 공격 , 콤보 공격 , 거리두기, 쫒아가기
    // Ai : 스테이지 입장시 플레이어를 쫒아옴 일정거리 보다 가까울 시 랜덤행동
    #endregion

    virtual protected void Awake()
    {
        targetTf = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GetComponent<Enemy>();
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }
    virtual protected void ChasePlayer() { }
    virtual protected void Attack() { }
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

}
