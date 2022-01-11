using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

[System.Serializable]
public class SaveData
{
    public Player player;
    public float hp;
}
public class SaveAndLoad : SingleTon<SaveAndLoad>
{

    [SerializeField] private Player player;
    [SerializeField] private string targetTag;
    private string saveData_Directroy;
    private string saveFileName = "PlayerData.json";
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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        saveData.player = player;
        saveData.hp = saveData.player.currentHp;
        string test = JsonConvert.SerializeObject(player);
        Debug.Log(test);
        File.WriteAllText(saveData_Directroy + saveFileName, JsonUtility.ToJson(saveData));

    }
    private void Load(Scene scene, LoadSceneMode mode)
    {
        if (File.Exists(saveData_Directroy + saveFileName))
        {
            LoadPlayerData();

        }
    }
    private void LoadPlayerData()
    {
        Player target = GameObject.FindGameObjectWithTag(targetTag).GetComponent<Player>();
        string load = File.ReadAllText(saveData_Directroy + saveFileName);
        Player playerData = JsonUtility.FromJson<SaveData>(load).player;
        target.maxHp = playerData.maxHp;
        target.currentHp = playerData.currentHp;
        target.atk = playerData.atk;
        target.def = playerData.def;
        target.atkSpeed = playerData.atkSpeed;
        target.criticalChance = playerData.criticalChance;
    }
}
