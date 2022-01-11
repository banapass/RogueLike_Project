using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (StageManager.instance.scenes.Count <= 0)
        {
            StageManager.LoadScene("Title");
        }
        else
        {
            SaveAndLoad.instance.Save();
            StageManager.RandomChoiceStage();
        }

    }
    private void OnTriggerStay(Collider other)
    {

    }

}
