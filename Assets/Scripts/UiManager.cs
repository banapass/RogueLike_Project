using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class UiManager : SingleTon<UiManager>
{
    [SerializeField] private Player player;
    [SerializeField] private CameraMovement targetCamera;
    [SerializeField] private Slider sensSlider;
    [SerializeField] private Image hpbar;
    [SerializeField] private Button[] btnTemp;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject option;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private Stack<GameObject> menuStack = new Stack<GameObject>();
    public bool isMenuOpen;



    // Update is called once per frame
    void Update()
    {
        State();
        MouseSens();
        Menu();

    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += SetLoad;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SetLoad;
    }
    private void SetLoad(Scene scene, LoadSceneMode mode)
    {

        StartCoroutine(SearchPlayerCo());
    }
    private IEnumerator SearchPlayerCo()
    {
        while (true)
        {
            try
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                targetCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
                yield break;
            }
            catch
            {
                //  Debug.Log("NULL");
            }

            yield return null;
        }
    }
    private void State()
    {
        if (player != null && SceneManager.GetActiveScene().name.IndexOf("Title") == -1)
        {
            hpbar.transform.parent.gameObject.SetActive(true);
            hpbar.fillAmount = (player.CurrentHp / player.maxHp);
            hpText.text = (player.CurrentHp + "/" + player.maxHp);
        }
        else
        {
            hpbar.transform.parent.gameObject.SetActive(false);
        }
    }

    public void StartButton()
    {
        if (File.Exists(Application.dataPath + "/Save/PlayerData.json"))
        {
            File.Delete(Application.dataPath + "/Save/PlayerData.json");
            Debug.Log("Delete");
        }
        StageManager.instance.GetAllScenes();
        StageManager.RandomChoiceStage();
    }

    public void Resume()
    {
        isMenuOpen = false;
        Time.timeScale = 1;
        menuStack.Pop().SetActive(false);
    }
    public void Title()
    {
        gameOver.SetActive(false);
        if (menuStack.Count > 0)
            menuStack.Pop().SetActive(false);
        SceneManager.LoadScene("Title");
    }
    public void StartGameOverCo()
    {

        StartCoroutine(GameOverPanel());
    }
    private IEnumerator GameOverPanel()
    {
        gameOver.SetActive(true);
        CanvasGroup gameOverGroup = gameOver.GetComponent<CanvasGroup>();
        gameOverGroup.alpha = 0;

        while (true)
        {
            gameOverGroup.alpha += Time.deltaTime * 2;
            if (gameOverGroup.alpha >= 1)
            {
                gameOverGroup.blocksRaycasts = true;
                break;
            }
            yield return null;

        }
    }
    private void MouseSens()
    {
        if (targetCamera != null)
            targetCamera.sens = sensSlider.value * 10;
    }
    private void Menu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !player.isDie)
        {
            if (menuStack.Count <= 0 && SceneManager.GetActiveScene().name.IndexOf("Stage") != -1)
            {
                isMenuOpen = true;
                Time.timeScale = 0;
                menuStack.Push(menu);
                menu.SetActive(true);
            }
            else
            {

                menuStack.Pop().SetActive(false);
                if (menuStack.Count > 0)
                    menuStack.Peek().SetActive(true);
                if (menuStack.Count <= 0)
                {
                    isMenuOpen = false;
                    Time.timeScale = 1;
                }
            }
        }
    }
    public void Option()
    {
        if (menuStack.Count > 0)
            menuStack.Peek().SetActive(false);
        menuStack.Push(option);
        option.SetActive(true);
    }
    public void StackClear()
    {
        for (int i = 0; i < menuStack.Count; i++)
        {
            menuStack.Pop().SetActive(false);
        }
    }
    public void AppQuit()
    {
        Application.Quit();
    }

}
