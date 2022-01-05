using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 만들 오브젝트와 생성 갯수 설정용
[System.Serializable]
public struct PoolIngredient
{
    public GameObject prafab;
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

public class TestDic : MonoBehaviour
{

    public static TestDic instance;
    public List<PoolIngredient> poolingPrefabs;
    Dictionary<string, Pool> poolingDic = new Dictionary<string, Pool>();
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

        CreatePool();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void CreatePool()
    {
        for (int i = 0; i < poolingPrefabs.Count; i++)
        {
            Pool tempPool = new Pool();
            tempPool.CreatePool(poolingPrefabs[i].prafab, transform, poolingPrefabs[i].poolingCount);
            poolingDic.Add(poolingPrefabs[i].prafab.name, tempPool);
        }
    }
    /// <summary>
    /// arrow , atkEffect1,
    /// 
    /// </summary>
    public static GameObject GetParts(string partsName)
    {
        if (instance.poolingDic[partsName].pooling.Count > 0)
        {
            GameObject obj = instance.poolingDic[partsName].pooling.Dequeue();
            obj.transform.SetParent(null);
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject newObj = instance.poolingDic[partsName].pooling.Dequeue();
            newObj.transform.SetParent(null);
            newObj.SetActive(true);
            return newObj;
        }
    }
    public static void ReturnParts(GameObject parts, string partsName)
    {
        parts.SetActive(false);
        parts.transform.SetParent(instance.transform);
        instance.poolingDic[partsName].pooling.Enqueue(parts);
    }
}
