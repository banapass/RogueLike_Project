using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (StageManager.instance.scenes.Count <= 0)
        {
            StageManager.LoadScene("EndingScene");
        }
        else
        {
            StageManager.RandomChoiceStage();
        }
    }
}
