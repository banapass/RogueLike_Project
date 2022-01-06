using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : Character
{
    [SerializeField] protected GameObject coin;
    [SerializeField] protected Transform targetTf; // 체력바가 바라볼 대상
    [SerializeField] protected Image hpBar;
    [SerializeField] protected Canvas canvas;
    [SerializeField] protected PlayerController controller;
    [SerializeField] protected LayerMask layerMask;
    [SerializeField] protected GameObject damageText;
    [SerializeField] protected Transform damageDispalyTf;
    [SerializeField] protected float checkRange = 5f;

    protected bool isAttack;
    public bool isHit;
    bool isCheck = false;


    #region 남은 적 구현 목록
    // 몬스터 죽을시 무기 및 아이템 드랍
    // 몬스터 Ai
    #endregion
    // Start is called before the first frame update
    protected void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        targetTf = GameObject.FindGameObjectWithTag("MainCamera").transform;
        hpBar = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        canvas = transform.GetChild(0).GetComponent<Canvas>();
        damageDispalyTf = transform;
        SetDeadEvent();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCheck();
        if (isHit)
            IsHitCheck();

    }
    private void OnDisable()
    {
        Instantiate(coin, transform.position, Quaternion.identity);

    }
    protected override void SetDeadEvent()
    {
        base.SetDeadEvent();
    }
    protected void EnemyHpbar()
    {
        hpBar.fillAmount = (currentHp / maxHp);
        canvas.transform.LookAt(targetTf);
    }

    // 캐릭터의 애니메이션이 끝날때 isHit을 다시 false로 바꿈
    protected void IsHitCheck()
    {
        for (int i = 1; i < 4; i++)
        {
            isCheck |= controller.EndAnim("GreatSword_Attack" + i, 0.85f);
            //  Debug.Log("isCheck" + i + "번째 = " + controller.EndAnim("GreatSword_Attack" + i));
        }

        // Debug.Log("DodgeCheck = " + controller.EndAnim("Dodge", 0.7f));

        if (isCheck || controller.EndAnim("Dodge", 0.7f))
        {
            Debug.Log("들어옴 EndAnim");
            isHit = false;
        }
        isCheck = false;
    }
    protected void PlayerCheck()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, checkRange, layerMask);
        if (cols.Length > 0)
        {
            hpBar.gameObject.SetActive(true);
            EnemyHpbar();
        }
        else
        {
            hpBar.gameObject.SetActive(false);
        }
    }

    public override void OnHit(Character character, Transform hit)
    {
        float totalAtk = character.atk * (character.damege + character.increaceDmg);
        if (!isHit)
        {
            if (Critical(character))
            {
                ShowDamage(((int)(totalAtk - (int)DamageReduction(totalAtk)) * 2));
                CurrentHp -= (totalAtk - (int)DamageReduction(totalAtk)) * 2;
                ObjectPools.GetParts("atkCritical").transform.position = hit.transform.position;
                Debug.Log("Critical");

            }
            else
            {
                ShowDamage((int)(totalAtk - (int)DamageReduction(totalAtk)));
                ObjectPools.GetParts("atkEffect").transform.position = hit.transform.position;
                CurrentHp -= totalAtk - (int)DamageReduction(totalAtk);
            }

            Debug.Log(name + " " + "현재 Hp : " + CurrentHp + "데미지 : " + (totalAtk - (int)DamageReduction(totalAtk)));
            isHit = true;
        }
    }
    protected bool Critical(Character player)
    {
        int randomNum = Random.Range(1, 101);
        if (randomNum <= player.criticalChance)
            return true;
        else
            return false;

    }
    protected void ShowDamage(int damage)
    {
        GameObject temp = Instantiate(damageText, transform.GetChild(0));
        temp.transform.localPosition = transform.localPosition - Vector3.up * 0.5f + (Vector3.forward) * 1.5f;
        temp.GetComponent<DamageTextConroller>().damage = damage;
    }

    // 아이템 드랍
    protected void OnDestroy()
    {

    }
    private void OnTriggerEnter(Collider other)
    {

    }
}
