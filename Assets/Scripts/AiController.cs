using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AiController : MonoBehaviour
{
    [SerializeField] protected Transform targetTf;
    [SerializeField] protected Animator anim;
    [SerializeField] protected NavMeshAgent nav;
    [SerializeField] protected bool isMove;

    #region 적 Ai로직 정리
    // 근접 적 : 일반 공격 , 콤보 공격 , 거리두기, 쫒아가기
    // Ai : 스테이지 입장시 플레이어를 쫒아옴 일정거리 보다 가까울 시 랜덤행동
    #endregion

    protected void Start()
    {
        targetTf = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }
    abstract protected void ChasePlayer();
    abstract protected void Attack();

}
