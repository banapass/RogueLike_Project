using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    int coinCount;
    // Start is called before the first frame update
    private void OnEnable()
    {
        SetCoin();

    }
    private void SetCoin()
    {
        coinCount = Random.Range(1, 5);
        Rigidbody rigid = GetComponent<Rigidbody>();
        transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0f);
        rigid.velocity = Vector3.up * 5;
        rigid.AddForce(transform.forward * 100);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().GetCoin(coinCount);
            gameObject.SetActive(false);
        }
    }



}
