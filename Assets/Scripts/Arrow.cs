using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    private void OnEnable()
    {
        StartCoroutine(ReturnArrow());
    }
    private IEnumerator ReturnArrow()
    {
        float currentTime = 0;
        float setTime = 5;
        while (true)
        {

            currentTime += Time.deltaTime;
            if (currentTime >= setTime)
            {
                ObjectPools.ReturnParts(this.gameObject, "arrow");
            }
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !other.GetComponent<PlayerController>().isDodge)
        {
            Player target = other.GetComponent<Player>();
            ObjectPools.ReturnParts(this.gameObject, "arrow");
        }
    }

}
