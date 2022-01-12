using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;


public class StageManager : SingleTon<StageManager>
{

    [SerializeField] Player player;
    [SerializeField] GameObject target;
    public Transform gridBase;
    public List<Button> gridList;
    public List<string> scenes = new List<string>();
    [SerializeField] List<string> stages = new List<string>();
    [SerializeField] float atkIncrease;
    [SerializeField] float defIncrease;
    [SerializeField] int createCount;
    public static int stageCount;
    public static string nextScene;
    public bool isOpen;
    bool isSetComplete;
    // Start is called before the first frame update
    void Start()
    {
        try
        {


        }
        catch
        {
            Debug.Log("NULL");
        }

        GetAllScene();
        DefaultSetting();
        //RandomChoiceStage();

    }
    private void Update()
    {
        Debug.Log(isOpen);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += StageCheck;

    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= StageCheck;

    }
    private void StageCheck(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name.IndexOf("Stage") == -1)
        {
            gridBase.gameObject.SetActive(false);

        }
        else
        {
            gridBase.gameObject.SetActive(true);
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            isSetComplete = false;
            isOpen = false;
        }
        DisableGrid();
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
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
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


            if (operation.progress == 0.9f)
            {
                delay += Time.unscaledDeltaTime;

                progressBar.fillAmount = Mathf.Lerp(0.9f, 1, delay);
                progressText.text = (int)(progressBar.fillAmount * 100f) + "%";

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
    private void DefaultSetting()
    {
        for (int i = 0; i < gridBase.childCount; i++)
        {
            gridList.Add(gridBase.GetChild(i).GetComponent<Button>());
        }
    }
    public void SetGrid()
    {

        if (SceneManager.GetActiveScene().name.IndexOf("Stage") != -1 && !isOpen)
        {
            gridBase.gameObject.SetActive(true);
            for (int i = 0; i < createCount; i++)
            {
                int randomNum = Random.Range(0, gridList.Count);
                if (gridList[randomNum].gameObject.activeSelf == false)
                    gridList[randomNum].gameObject.SetActive(true);
                else
                    i--;
            }

        }

        isOpen = true;
        isSetComplete = true;
        Debug.Log("GridEnd");
    }
    private IEnumerator TimeScaleControl()
    {
        while (true)
        {
            Time.timeScale -= 0.01f;
            if (Time.timeScale <= 0)
            {
                break;
            }
            yield return null;
        }
    }
    public void DisableGrid()
    {
        for (int i = 0; i < gridList.Count; i++)
        {
            if (gridList[i].gameObject.activeSelf == true)
            {
                gridList[i].gameObject.SetActive(false);
            }
        }
    }
    #region 스테이지 클리어 보상
    // 공격력 상승
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
    public void SelectPanelClick()
    {

        gridBase.gameObject.SetActive(false);
        SaveAndLoad.instance.Save();
        RandomChoiceStage();
    }
    #endregion
}
