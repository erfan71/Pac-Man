using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    #region Singleton Pattern
    static ObjectPoolManager _instance;
    public static ObjectPoolManager Instance
    {
        get {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<ObjectPoolManager>();
            return _instance;
        }
    }
    #endregion

    [System.Serializable]
    public struct PoolObject
    {
        
        public string objectKey;
        public Transform objectPrefab;
        public int objectInitialCount;
    }

    public PoolObject[] objectsRef;
    public Dictionary<string, List<Transform>> objectsInstances;
    void Awake()
    {
        objectsInstances = new Dictionary<string, List<Transform>>();
        FillThePool();


    }
    void FillThePool()
    {
        foreach (PoolObject obj in objectsRef)
        {
            List<Transform> transforms = new List<Transform>();
            for (int i = 0; i < obj.objectInitialCount; i++)
            {
                Transform instance = Instantiate(obj.objectPrefab, transform);
                instance.gameObject.SetActive(false);
                transforms.Add(instance);
            }
            if (objectsInstances.ContainsKey(obj.objectKey))
            {
                Debug.LogError("You have Duplicate Key.. this one will be ignored");
                continue;
            }
            else
            {
                objectsInstances.Add(obj.objectKey, transforms);
            }
        }
    }
    public Transform GetAnObject(string key)
    {
        if (objectsInstances.ContainsKey(key))
        {
            foreach (Transform tr in objectsInstances[key])
            {
                if (!tr.gameObject.activeSelf)
                {
                    tr.gameObject.SetActive(true);
                    return tr;
                }
            }
            //there is none left
            //instantiate one
            Transform prefab = FindAnObjectPrefab(key);
            if (prefab != null)
            {
                Transform instance = Instantiate(prefab, transform);
                instance.gameObject.SetActive(true);
                return instance;
            }
            else
            {
                return null;
            }

        }
        else
        {
            Debug.LogError("There is no Object with this key");
            return null;
        }
    }
    Transform FindAnObjectPrefab(string key)
    {
        foreach (PoolObject obj in objectsRef)
        {
            if (obj.objectKey == key)
            {
                return obj.objectPrefab;
            }
        }
        Debug.LogError("There is Object with this Key");
        return null;
    }
    public void RecycleAnObject(Transform objectInstance)
    {
        objectInstance.gameObject.SetActive(false);
    }
}
