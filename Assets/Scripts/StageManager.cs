using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



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
        DefaultSetting();
        //RandomChoiceStage();
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
        Time.timeScale = 1;
        UiManager.instance.isMenuOpen = false;

    }

    public void GetAllScenes()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        scenes.Clear();

        for (int i = 0; i < sceneCount; i++)
        {
            if (SceneUtility.GetScenePathByBuildIndex(i).IndexOf("Stage") != -1)
            {
                scenes.Add(SceneUtility.GetScenePathByBuildIndex(i));
            }
        }
    }

    public static void RandomChoiceStage()
    {
        int randomNum;
        while (true)
        {
            randomNum = Random.Range(0, instance.scenes.Count);
            // ?????? ??????????????? ???????????? ?????????
            if (instance.scenes[randomNum].IndexOf("Boss") == -1)
            {
                LoadingScene(instance.scenes[randomNum]);
                instance.scenes.Remove(instance.scenes[randomNum]);
                break;
            }
            // ?????? ????????????
            else if (instance.scenes.Count == 1)
            {
                LoadingScene(instance.scenes[0]);
                instance.scenes.Remove(instance.scenes[0]);
                break;
            }
        }

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
    #region ???????????? ????????? ??????
    // ????????? ??????
    public void AtkUp()
    {
        player.increaceDmg += atkIncrease;
        Debug.Log(atkIncrease * 100 + "% ????????? ??????");
    }
    // ????????? ??????
    public void DefUp()
    {
        player.def += 10;
    }
    // ???????????? 20% ??????
    public void MaxHpUp()
    {
        player.maxHp *= 0.2f;
    }
    // ???????????? 50% ?????? ??????
    public void Healing()
    {
        player.CurrentHp += player.maxHp * 0.5f;
    }
    // ???????????? 20% ??????
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
        SaveAndLoad.instance.Save();
        gridBase.gameObject.SetActive(false);
        RandomChoiceStage();
    }
    #endregion
}
