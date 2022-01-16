using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ButtonSet();
    }

    private void ButtonSet()
    {
        Button[] buttons = GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(UiManager.instance.StartButton);
    }
}
