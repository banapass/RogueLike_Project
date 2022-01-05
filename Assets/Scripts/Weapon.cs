using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected string weaponName;
    [SerializeField] public float atk;
    [SerializeField] protected WeaponTier weaponTier; // 무기 티어
    [SerializeField] protected WeaponAttributes attributes; // 무기 속성
    [SerializeField] protected PlayerController controller;
    // [SerializeField] protected GameObject hitEffect; // 이펙트
    [SerializeField] protected string targetTag;

    // 캐싱
    protected Player player;
    protected Enemy target;
    protected BoxCollider col;
    private Transform hitPoint;
    protected enum WeaponTier
    {
        Tier1 = 1,
        Tier2,
        Tier3,

    }
    protected enum WeaponAttributes
    {
        Fire = 1,
        Ice,
    }

    #region 남은 무기 구현 목록
    // 무기 티어별 랜덤 스탯 부여
    // 옵션 랜덤 부여
    // 무기 공격 딜레이 주기
    // 추가요소) 속성별 이벤트 
    // Fire 초당 체력1% 지속피해
    // Ice : 일정시간동안 빙결 or 일정시간동안 공격 및 이동속도 감소  
    #endregion
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        col = GetComponent<BoxCollider>();
        hitPoint = transform.GetChild(0).transform;

    }

    private void Update()
    {
        if (controller.EndAnim("GreatSword_Attack"))
        {
            col.enabled = false;
        }
    }

    protected private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>() != null)
        {
            if (other.tag == targetTag)
            {
                //Character[] temp = other.GetComponent<Character>();
                target = other.GetComponent<Enemy>();
                if (!target.isHit)
                {
                    ObjectPools.GetParts("atkEffect").transform.position = hitPoint.transform.position;
                }
                target.OnHit(player);


                //col.enabled = false; // 한번에 여러번 공격 방지

            }
        }
    }

}
