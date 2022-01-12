using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingleTon<GameManager>
{
    [SerializeField] private GameObject targetObj;
    [SerializeField] private GameObject[] enemies;


    void Update()
    {
        CursorOnOff();
        CheckEnemyLength();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += SetLoadScene;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SetLoadScene;
    }
    private void SetLoadScene(Scene scene, LoadSceneMode mode)
    {
        targetObj = GameObject.FindGameObjectWithTag("Gate");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        CursorOnOff();
    }

    // 적이 스테이지에 남아있는지 체크
    private void CheckEnemyLength()
    {
        try
        {
            if (enemies.Length <= 0)
            {
                targetObj.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                enemies = GameObject.FindGameObjectsWithTag("Enemy");
            }
        }
        catch
        {
            Debug.Log("NULL");
        }
    }

    private void CursorOnOff()
    {
        if (SceneManager.GetActiveScene().CheckScene("Stage"))
        {


            if (Input.GetKey(KeyCode.LeftAlt) || StageManager.instance.isOpen)
            {
                CursorOn();
            }
            else
            {
                CursorOff();
            }


        }


    }
    public void CursorOn()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void CursorOff()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
