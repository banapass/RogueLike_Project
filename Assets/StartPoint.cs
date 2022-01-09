using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private void Awake()
    {
        SetPlayerPos();
    }

    private void SetPlayerPos()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("Player");
        temp.transform.position = transform.localPosition;
        temp.transform.rotation = transform.localRotation;
    }
}
