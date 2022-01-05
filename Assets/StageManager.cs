using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StageManager : MonoBehaviour
{

    [SerializeField] Player player;
    [SerializeField] Transform gridBase;
    [SerializeField] GameObject target;
    [SerializeField] Button[] gridList;
    [SerializeField] float atkIncrease;
    [SerializeField] float defIncrease;
    [SerializeField] int createCount;
    bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        // gridList = gridBase.GetComponentsInChildren<Button>();
        SetGrid();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetGrid()
    {
        for (int i = 0; i < createCount; i++)
        {
            int randomNum = Random.Range(0, gridList.Length);
            if (gridList[randomNum].gameObject.activeSelf == false)
                gridList[randomNum].gameObject.SetActive(true);
            else
                i--;


        }
    }
    public void AtkUp()
    {
        player.increaceDmg += atkIncrease;
        Debug.Log(atkIncrease * 100 + "% 공격력 상승");
    }
    // 방어력 상승
    public void DefUp()
    {
        player.def += 10;
    }
    // 최대체력 20% 상승
    public void MaxHpUp()
    {
        player.maxHp *= 0.2f;
    }
    // 최대체력 50% 만큼 회복
    public void Healing()
    {

        player.CurrentHp += player.maxHp * 0.5f;
    }
    // 공격속도 20% 상승
    public void AtkSpeedUp()
    {
        player.atkSpeed += 0.2f;
    }
}