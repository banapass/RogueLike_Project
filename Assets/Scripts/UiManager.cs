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
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject option;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private Stack<GameObject> menuStack;
    public bool isMenuOpen;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        State();
        MouseSens();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += SearchPlayer;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SearchPlayer;
    }
    private void SearchPlayer(Scene scene, LoadSceneMode mode)
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
        if (player != null)
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
        StageManager.RandomChoiceStage();
    }

    public void Resume()
    {
        menu.SetActive(false);
    }
    public void Title()
    {
        SceneManager.LoadScene("Title");
    }
    public IEnumerator GameOverPanel()
    {
        while (true)
        {

        }
    }
    private void MouseSens()
    {
        if (targetCamera != null)
            targetCamera.sens = sensSlider.value * 10;
    }
    private void Menu()
    {
        isMenuOpen = !isMenuOpen;
    }

}
