using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, ISaveTarget
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
    public float atkSpeed = 2;

    public SaveData saveData;
    public SaveData SavaDataProperty
    {
        get
        {
            SaveDataSetting();
            return saveData;
        }
        set
        {
            saveData = value;
            PlayerSetting();
        }
    }

    void PlayerSetting()
    {
        atkSpeed = saveData.atkSpeed;
        atk = saveData.atk;
        maxHp = saveData.maxHp;
        currentHp = saveData.currentHp;
        def = saveData.def;
        damege = saveData.damage;
        increaceDmg = saveData.increaceAtk;
    }
    public void SaveDataSetting()
    {
        saveData.atkSpeed = atkSpeed;
        saveData.atk = atk;
        saveData.maxHp = maxHp;
        saveData.currentHp = currentHp;
        saveData.def = def;
        saveData.damage = damege;
        saveData.increaceAtk = increaceDmg;
    }


    public float MaxHp
    {
        get { return maxHp; }
        set { maxHp = value; }
    }
    public float Atk
    {
        get { return atk; }
        set { atk = value; }
    }
    public float Damage
    {
        get { return damege; }
        set { damege = value; }
    }
    public float IncreaceAtk
    {
        get { return increaceDmg; }
        set { increaceDmg = value; }
    }

    public float AtkSpeed
    {
        get { return atkSpeed; }
        set { atkSpeed = value; }
    }

    public override float CurrentHp
    {
        get { return currentHp; }
        set
        {
            currentHp = value;
            if (currentHp <= 0)
            {
                isDie = true;
                deadEvent();
            }
        }
    }

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

    public void Awake()
    {

        saveData = new SaveData();

        if (System.IO.File.Exists(Application.dataPath + "/Save/" + "PlayerData.json"))
        {
            string loadData = System.IO.File.ReadAllText(Application.dataPath + "/Save/" + "PlayerData.json");
            SavaDataProperty = JsonUtility.FromJson<SaveData>(loadData);
        }
        else
        {
            Debug.Log("디폴트");
            saveData.maxHp = 500;
            saveData.currentHp = 500;
            saveData.damage = 1;
            saveData.def = 100;
            saveData.atk = 100;
            saveData.increaceAtk = 0;
            saveData.atkSpeed = 1;
            System.IO.File.WriteAllText(Application.dataPath + "/Save/" + "PlayerData.json", JsonUtility.ToJson(saveData));
        }

    }
    protected void Start()
    {

        deadEvent += SetDeadEvent;
        //EquipWeapon = weaponPoint.GetChild(0).GetComponent<Weapon>();
        playerAnim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();


        //OnEquipWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        playerAnim.SetFloat("AttackSpeed", atkSpeed);
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
        deadEvent = null;
        playerAnim.SetTrigger("Die");

    }
    public override void OnHit(Character character, Transform hit)
    {
        if (!playerController.isDodge)
        {
            float totalAtk = character.atk * (character.damege + character.increaceDmg);

            CurrentHp -= totalAtk - (int)DamageReduction(totalAtk);
            Debug.Log(CurrentHp + " " + totalAtk);
            ObjectPools.GetParts("EnemyAtkEffect").transform.position = hit.transform.position;
        }

    }
}
