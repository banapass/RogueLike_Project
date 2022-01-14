using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

            StageManager.instance.SetGrid();

        }
    }

}
