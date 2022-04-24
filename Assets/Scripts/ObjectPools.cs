using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 만들 오브젝트와 생성 갯수 설정

[System.Serializable]
public struct PoolIngredient
{
    public GameObject prefab;
    public int poolingCount;
}

public class Pool : MonoBehaviour
{
    public Queue<GameObject> pooling = new Queue<GameObject>();
    private GameObject CreateParts(GameObject obj, Transform parent = null)
    {
        GameObject temp = Instantiate(obj, parent);
        temp.SetActive(false);
        return temp;
    }
    public void CreatePool(GameObject obj, Transform parent = null, int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            pooling.Enqueue(CreateParts(obj, parent));
        }
    }
}

public class ObjectPools : SingleTon<ObjectPools>
{

    public List<PoolIngredient> poolingPrefabs;
    private Dictionary<string, Pool> poolingDic = new Dictionary<string, Pool>();

    void Start()
    {
        CreatePools();
    }

    private void CreatePools()
    {
        for (int i = 0; i < poolingPrefabs.Count; i++)
        {
            Pool tempPool = new Pool();
            tempPool.CreatePool(poolingPrefabs[i].prefab, transform, poolingPrefabs[i].poolingCount);
            poolingDic.Add(poolingPrefabs[i].prefab.name, tempPool);
        }
    }
    /// <summary>
    /// arrow , atkEffect, atkCritical
    /// 
    /// </summary>
    public static GameObject GetParts(string partsName)
    {
        if (instance.poolingDic[partsName].pooling.Count > 0)
        {
            GameObject obj = instance.poolingDic[partsName].pooling.Dequeue();
            Debug.Log(instance.poolingDic[partsName].pooling.Count);
            obj.transform.SetParent(null);
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject newObj = Instantiate(instance.SearchParts(partsName));
            newObj.transform.SetParent(null);
            newObj.SetActive(true);
            return newObj;
        }
    }
    public static void ReturnParts(GameObject parts, string partsName)
    {
        parts.SetActive(false);
        parts.transform.SetParent(instance.transform, false);
        instance.poolingDic[partsName].pooling.Enqueue(parts);
    }
    private GameObject SearchParts(string partsName)
    {
        for (int i = 0; i < poolingPrefabs.Count; i++)
        {
            if (poolingPrefabs[i].prefab.name == partsName)
            {
                return poolingPrefabs[i].prefab;
            }
        }
        return null;
    }
}
