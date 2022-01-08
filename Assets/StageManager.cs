using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;


public class StageManager : SingleTon<StageManager>
{

    [SerializeField] Player player;
    [SerializeField] Transform gridBase;
    [SerializeField] GameObject target;
    [SerializeField] Button[] gridList;
    [SerializeField] List<string> scenes = new List<string>();
    [SerializeField] List<string> stages = new List<string>();
    [SerializeField] float atkIncrease;
    [SerializeField] float defIncrease;
    [SerializeField] int createCount;
    public static int stageCount;
    public static string nextScene;
    bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //gridList = gridBase.GetComponentsInChildren<Button>();
        GetAllScene();
        //RandomChoiceStage();
        SetGrid();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GetAllScene()
    {
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene.path.IndexOf("Stage") != -1)
            {
                scenes.Add(scene.path);
            }
        }

    }
    public static void RandomChoiceStage()
    {
        int randomNum = Random.Range(0, instance.scenes.Count);
        LoadingScene(instance.scenes[randomNum]);
        instance.scenes.Remove(instance.scenes[randomNum]);
        stageCount++;
    }
    public static void LoadingScene(string scenePath)
    {
        nextScene = scenePath;
        SceneManager.LoadScene("LoadingScene");
    }
    public static IEnumerator SetLoadingScene(Image progressBar, TMPro.TextMeshProUGUI progressText)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
        operation.allowSceneActivation = false;
        float delay = 0;
        while (!operation.isDone)
        {
            progressBar.fillAmount = operation.progress;
            progressText.text = (progressBar.fillAmount * 100) + "%";

            if (operation.progress == 0.9f)
            {
                delay += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1, delay);
                progressText.text = (progressBar.fillAmount * 100) + "%";
                if (progressBar.fillAmount >= 1f)
                {
                    operation.allowSceneActivation = true;
                    yield break;
                }
                yield return null;
            }

        }
        yield return null;
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
    public void CriticalChanceUp()
    {
        player.criticalChance += 10;
    }
}
