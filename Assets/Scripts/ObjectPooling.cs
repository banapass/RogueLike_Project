using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling<T> : MonoBehaviour where T : MonoBehaviour
{
    public static ObjectPooling<T> instance;
    private Queue<GameObject> poolingObj = new Queue<GameObject>();
    [SerializeField] private int poolingCount = 10;
    [SerializeField] private T parts;
    // Start is called before the first frame update
    protected void Start()
    {
        instance = this;
        Pooling();
    }


    private GameObject CreateParts()
    {
        GameObject temp = Instantiate(parts.gameObject, transform);
        temp.gameObject.SetActive(false);
        return temp;
    }
    private void Pooling()
    {
        for (int i = 0; i < poolingCount; i++)
        {
            poolingObj.Enqueue(CreateParts());
        }
    }
    public static GameObject GetParts()
    {
        if (instance.poolingObj.Count > 0)
        {
            GameObject obj = instance.poolingObj.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;

        }
        else
        {
            GameObject newObj = instance.CreateParts();
            newObj.transform.SetParent(null);
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }
    public static void ReturnParts(GameObject parts)
    {
        parts.gameObject.SetActive(false);
        parts.transform.SetParent(instance.transform);
        instance.poolingObj.Enqueue(parts);
    }

}
