using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] protected GameObject coin;
    public
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnDisable()
    {
        Instantiate(coin, transform.position, Quaternion.identity);

    }

}
