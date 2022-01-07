using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private CameraMovement targetCamera;
    [SerializeField] private Slider sensSlider;
    [SerializeField] private Image hpbar;
    [SerializeField] private GameObject menu;
    [SerializeField] private TextMeshProUGUI hpText;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        State();
        MouseSens();
    }
    private void State()
    {
        hpbar.fillAmount = (player.CurrentHp / player.maxHp);
        hpText.text = (player.CurrentHp + "/" + player.maxHp);
    }
    public void Resume()
    {
        menu.SetActive(false);
    }
    public void Title()
    {
        SceneManager.LoadScene("Title");
    }
    private void MouseSens()
    {

        targetCamera.sens = sensSlider.value * 10;
    }

}
