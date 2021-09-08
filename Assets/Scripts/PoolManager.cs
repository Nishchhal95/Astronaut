using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PoolManager
{
    private static Dictionary<int, Queue<GameObject>> poolDictonary = new Dictionary<int, Queue<GameObject>>();
    public static void CreatePool(GameObject prefabToPool, Transform parentTransform, int initialAmount)
    {
        int goInstanceID = prefabToPool.GetInstanceID();

        if (poolDictonary.ContainsKey(goInstanceID))
        {
            Debug.Log("Pool Already Exist for this item");
            return;
        }

        Queue<GameObject> poolQueue = new Queue<GameObject>();

        for (int i = 0; i < initialAmount; i++)
        {
            GameObject go = UnityEngine.Object.Instantiate(prefabToPool, parentTransform);
            go.SetActive(false);
            poolQueue.Enqueue(go);
        }

        poolDictonary.Add(goInstanceID, poolQueue);
    }

    public static GameObject SpawnFromPool(GameObject goToSpawn, Vector3 position, Quaternion rotation, Transform parentTransform)
    {
        if (!poolDictonary.ContainsKey(goToSpawn.GetInstanceID()))
        {
            Debug.Log("No Pool added for this Gameobject");
            return null;
        }

        Queue<GameObject> poolQueue = poolDictonary[goToSpawn.GetInstanceID()];
        return poolQueue.Dequeue();
    }

    public static void ReturnToPool(GameObject prefabOfGO, GameObject goToReturn)
    {
        if (!poolDictonary.ContainsKey(prefabOfGO.GetInstanceID()))
        {
            Debug.Log("No Pool added for this Gameobject");
            return;
        }
        if (goToReturn.activeSelf)
        {
            goToReturn.SetActive(false);
        }
        poolDictonary[prefabOfGO.GetInstanceID()].Enqueue(goToReturn);
    }
}
