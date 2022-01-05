using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject targetObj;
    [SerializeField] private GameObject[] enemies;

    private void Start()
    {
        targetObj = GameObject.FindGameObjectWithTag("Gate");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
    void Update()
    {
        CursorOnOff();
        CheckEnemyLength();
    }
    // 적이 스테이지에 남아있는지 체크
    private void CheckEnemyLength()
    {
        if (enemies.Length <= 0)
        {
            targetObj.SetActive(false);
        }
        else
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }
    }

    private void CursorOnOff()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            CursorOn();
        }
        else
        {
            CursorOff();

        }
    }
    private void CursorOn()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void CursorOff()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
