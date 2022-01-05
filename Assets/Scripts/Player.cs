using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    [SerializeField] protected List<Item> inventory;
    [SerializeField] protected int coin;
    [SerializeField] protected Transform weaponPoint;
    [SerializeField] protected GameObject currentWeapon;
    [SerializeField] protected PlayerController playerController;
    [SerializeField] private Weapon equipWeapon;
    [SerializeField] private GameObject weaponTest;
    [SerializeField] private Animator playerAnim;
    //public int criticalChance;
    public float atkSpeed = 1;

    public Weapon EquipWeapon
    {
        get { return equipWeapon; }
        set
        {
            equipWeapon = value;
            if (equipWeapon != null)
            {
                atk = equipWeapon.atk;

            }
            else
            {
                atk = 50;
            }
        }
    }

    protected void Start()
    {
        maxHp = currentHp;
        deadEvent += SetDeadEvent;
        EquipWeapon = weaponPoint.GetChild(0).GetComponent<Weapon>();
        playerAnim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();


        //OnEquipWeapon();
    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    OnEquipWeapon();
        //}
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    OnReleaseWeapon();
        //}
    }
    // 미리 정해놓은 위치에 현제 장착하고 있는 무기 세팅
    private void OnEquipWeapon()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
            Debug.Log("Destroy");
        }
        GameObject temp = Instantiate(currentWeapon);
        temp.transform.SetParent(weaponPoint, false);
        temp.transform.position = weaponPoint.position;
        temp.transform.rotation = weaponPoint.rotation;
        EquipWeapon = temp.GetComponent<Weapon>();

    }
    private void OnReleaseWeapon()
    {
        EquipWeapon = null;
        Destroy(weaponPoint.GetChild(0));
    }
    public void GetCoin(int count)
    {
        coin += count;
    }
    #region 
    // 비활성화 or Destroy 
    // Die애니메이션  실행
    #endregion
    // 죽일 시 이벤트 설정

    protected override void SetDeadEvent()
    {
        base.SetDeadEvent();

    }
    public override void OnHit(Character character)
    {
        base.OnHit(character);

    }
}
