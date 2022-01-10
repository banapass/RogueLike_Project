using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMonsterAnimEvent : MonoBehaviour
{
    Transform playerTf;
    [SerializeField] private float arrowSpeed;

    [SerializeField] private Transform fireTf;
    // Start is called before the first frame update
    void Start()
    {
        arrowSpeed = 20f;
        playerTf = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnShoot()
    {
        GameObject temp = ObjectPools.GetParts("arrow");
        Rigidbody rigid = temp.GetComponent<Rigidbody>();
        rigid.velocity = ((playerTf.position + Vector3.up) - fireTf.position).normalized * arrowSpeed;
        temp.transform.position = fireTf.position;
        temp.transform.forward = rigid.velocity;
    }


}
