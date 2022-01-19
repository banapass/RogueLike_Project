using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Loading : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private TextMeshProUGUI progressText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StageManager.SetLoadingScene(progressBar, progressText));
    }

}
