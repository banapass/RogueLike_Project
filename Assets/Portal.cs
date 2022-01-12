using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    bool isClick;
    private void OnTriggerEnter(Collider other)
    {
        if (StageManager.instance.scenes.Count <= 0)
        {
            StageManager.LoadScene("Title");
        }
        else
        {
            StageManager.instance.gridBase.gameObject.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {

        // SaveAndLoad.instance.Save();
        // StageManager.RandomChoiceStage();
    }

}
