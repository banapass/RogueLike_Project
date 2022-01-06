using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnObject : MonoBehaviour
{
    private void OnDisable()
    {
        ObjectPools.ReturnParts(this.gameObject, name.RemoveClone());
    }
}
