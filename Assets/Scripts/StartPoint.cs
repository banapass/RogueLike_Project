using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartPoint : MonoBehaviour
{
    Scene scene;
    LoadSceneMode mode;
    private void Awake()
    {
        SetPlayerPos();

    }

    private void SetPlayerPos()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            GameObject temp = GameObject.FindGameObjectWithTag("Player");
            temp.transform.position = transform.localPosition;
            temp.transform.rotation = transform.localRotation;


        }
        else
        {
            GameObject[] temps = Resources.LoadAll<GameObject>("Player/");
            for (int i = 0; i < temps.Length; i++)
            {
                Instantiate(temps[i], transform.localPosition, transform.localRotation);
            }
        }

        SaveAndLoad.instance.Load(scene, mode);
    }

}
