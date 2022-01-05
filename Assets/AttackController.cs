using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private GameObject atkEffect;
    [SerializeField] private Transform effectPoint;
    bool isAttack = true;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * range, Color.red);
        if (Input.GetMouseButtonDown(0))
        {
            TryAttack();
        }
    }
    private void TryAttack()
    {

        Effect(atkEffect, effectPoint);
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            if (hit.transform.tag == "Enemy")
            {
                Debug.Log("Hit");
            }
        }
    }
    private IEnumerator AttackCo()
    {

        if (isAttack)
        {
            yield return new WaitForSeconds(1f);
            isAttack = false;
        }


    }
    private void Effect(GameObject obj, Transform pos)
    {
        GameObject temp = Instantiate(obj, effectPoint.position, Quaternion.identity);
        temp.transform.SetParent(pos, false);
        temp.transform.position = pos.transform.position;
        temp.transform.rotation = pos.transform.rotation;
    }
}
