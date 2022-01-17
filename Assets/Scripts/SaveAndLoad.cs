using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveData : ISaveTarget
{

    public float currentHp;
    public float maxHp;
    public float atk;
    public float damage;
    public float increaceAtk;
    public float def;
    public float atkSpeed;
    public int criticalChance;


    #region 프로퍼티

    public float CurrentHp
    {
        get { return currentHp; }
        set { currentHp = value; }
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
        get { return damage; }
        set { damage = value; }
    }
    public float IncreaceAtk
    {
        get { return increaceAtk; }
        set { increaceAtk = value; }
    }

    public float Def
    {
        get { return def; }
        set { def = value; }
    }
    public float AtkSpeed
    {
        get { return atkSpeed; }
        set { atkSpeed = value; }
    }

    #endregion


}
public class SaveAndLoad : SingleTon<SaveAndLoad>
{


    [SerializeField] private string targetTag;
    public string saveData_Directroy;
    public string saveFileName = "PlayerData.json";
    private void Start()
    {
        saveData_Directroy = Application.dataPath + "/Save/";
        if (!Directory.Exists(saveData_Directroy))
        {
            Directory.CreateDirectory(saveData_Directroy);
        }

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += Load;

    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= Load;
    }
    public void Save()
    {
        SaveData saveData = new SaveData();
        Player target = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        File.WriteAllText(saveData_Directroy + saveFileName, JsonUtility.ToJson(target.SavaDataProperty));

    }


    private void SetPlayer(SaveData target, SaveData apply)
    {
        target = apply;
        File.WriteAllText(saveData_Directroy + saveFileName, JsonUtility.ToJson(target, true));
    }


    public void Load(Scene scene, LoadSceneMode mode)
    {

        Player target = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();


        if (File.Exists(saveData_Directroy + saveFileName))
        {
            string load = File.ReadAllText(saveData_Directroy + saveFileName);
            target.SavaDataProperty = JsonUtility.FromJson<SaveData>(load);
        }
        else
        {
            File.WriteAllText(saveData_Directroy + saveFileName, JsonUtility.ToJson(target.SavaDataProperty, true));
        }

    }
}
