using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Character : MonoBehaviour
{
    public float maxHp;
    public float currentHp;
    public float atk;
    public float def;
    
    const int CONST_DEF = 100;
    public float damege = 1.0f;
    public float increaceDmg;

    public Action deadEvent;
    public bool isDie;

    virtual public float CurrentHp
    {
        set
        {
            currentHp = value;
            if (currentHp <= 0)
            {
                deadEvent();
                currentHp = 0;
            }
            if (currentHp > maxHp)
            {
                currentHp = maxHp;
            }
        }
        get
        {
            return currentHp;
        }
    }
    public float Def
    {
        set
        {
            def = value;
            if (def > 400) // 피해감소율 80% 제한
            {
                def = 400;
            }
        }
        get { return def; }
    }

    #region 공식 정리   
    // 체력 전체 퍼센트 공식 : (currentHp / maxHp) * 100;
    #endregion


    protected enum State
    {
        None,
        Brun,
        Poison,
        Frezee,
    }

    // 기본 베이스 DeadEvent
    virtual protected void SetDeadEvent()
    {
        deadEvent += () => gameObject.SetActive(false);
    }
    // 피해감소율
    public float DamageReduction(float atk)
    {
        return atk * (def / (def + CONST_DEF));
    }
    virtual public void OnHit(Character character, Transform hit) { }


}
