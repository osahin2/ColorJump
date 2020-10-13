using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;
    public bool shouldExpand = true;
    
    private Transform ground;
    private Vector3 distance;
    private void Awake()
    {
        ground = GameObject.FindGameObjectWithTag("Ground").GetComponent<Transform>();
        distance = ground.transform.position;
    }
    private void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }

        StartCoroutine(Pooler());
    }
    
    IEnumerator Pooler()
    {
        while (true)
        {
            objectToPool = GetPooledObject();
            objectToPool.transform.position = new Vector3(0, 0, distance.z += 120f);
            objectToPool.SetActive(true);
            yield return new WaitForSeconds(12.5f);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        if (shouldExpand)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            return obj;
        }
        else
        {
            return null;
        }
    }

}
