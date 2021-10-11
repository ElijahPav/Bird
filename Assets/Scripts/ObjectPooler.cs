using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public GameObject objectToPool;
    public int amountToPool;
    public bool shouldExpand = true;

    public ObjectPoolItem(GameObject obj, int amt, bool exp = true)
    {
        objectToPool = obj;
        amountToPool = Mathf.Max(amt, 2);
        shouldExpand = exp;
    }
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    public List<ObjectPoolItem> itemsToPool;


    public List<List<GameObject>> pooledObjectsList;
    public List<GameObject> pooledObjects;
    private List<int> positions;
    public int spikeIndex;

    void Awake()
    {
        Instance = this;

        pooledObjectsList = new List<List<GameObject>>();
        pooledObjects = new List<GameObject>();
        positions = new List<int>();


        for (int i = 0; i < itemsToPool.Count; i++)
        {
            ObjectPoolItemToPooledObject(i);
        }
    }
    
    public void AddSpike(GameObject GO, int amt = 3, bool exp = true)
    {
        ObjectPoolItem item = new ObjectPoolItem(GO, amt, exp);
        int currLen = itemsToPool.Count;
        itemsToPool.Add(item);
        ObjectPoolItemToPooledObject(currLen);
        spikeIndex=currLen;
    }

    public List<GameObject> GetAllPooledSpikes()
    {
        return pooledObjectsList[spikeIndex];
    }

    public GameObject GetPooledSpike()
    {
        int curSize = pooledObjectsList[spikeIndex].Count;
        for (int i = positions[spikeIndex] + 1; i < positions[spikeIndex] + pooledObjectsList[spikeIndex].Count; i++)
        {
            if (!pooledObjectsList[spikeIndex][i % curSize].activeInHierarchy)
            {
                positions[spikeIndex] = i % curSize;
                return pooledObjectsList[spikeIndex][i % curSize];
            }
        }

        if (itemsToPool[spikeIndex].shouldExpand)
        {
            GameObject obj = (GameObject) Instantiate(itemsToPool[spikeIndex].objectToPool);
            obj.SetActive(false);
            obj.transform.parent = this.transform;
            pooledObjectsList[spikeIndex].Add(obj);
            return obj;
        }

        return null;
    }

    public void SetSpikesToDefault(Vector3 defaultRotation,Vector3 defaultPosition)
    {
        
        foreach (var spike in GetAllPooledSpikes())
        {
            spike.transform.eulerAngles = defaultRotation;
            spike.transform.position = defaultPosition;
            spike.SetActive(false);
        }
    }


    public GameObject GetPooledObject(int index)
    {
        int curSize = pooledObjectsList[index].Count;
        for (int i = positions[index] + 1; i < positions[index] + pooledObjectsList[index].Count; i++)
        {
            if (!pooledObjectsList[index][i % curSize].activeInHierarchy)
            {
                positions[index] = i % curSize;
                return pooledObjectsList[index][i % curSize];
            }
        }

        if (itemsToPool[index].shouldExpand)
        {
            GameObject obj = (GameObject) Instantiate(itemsToPool[index].objectToPool);
            obj.SetActive(false);
            obj.transform.parent = this.transform;
            pooledObjectsList[index].Add(obj);
            return obj;
        }

        return null;
    }

    public List<GameObject> GetAllPooledObjects(int index)
    {
        return pooledObjectsList[index];
    }


    public int AddObject(GameObject GO, int amt = 3, bool exp = true)
    {
        ObjectPoolItem item = new ObjectPoolItem(GO, amt, exp);
        int currLen = itemsToPool.Count;
        itemsToPool.Add(item);
        ObjectPoolItemToPooledObject(currLen);
        return currLen;
    }


    void ObjectPoolItemToPooledObject(int index)
    {
        ObjectPoolItem item = itemsToPool[index];

        pooledObjects = new List<GameObject>();
        for (int i = 0; i < item.amountToPool; i++)
        {
            GameObject obj = (GameObject) Instantiate(item.objectToPool);
            obj.SetActive(false);
            obj.transform.parent = this.transform;
            pooledObjects.Add(obj);
        }

        pooledObjectsList.Add(pooledObjects);
        positions.Add(0);
    }
}