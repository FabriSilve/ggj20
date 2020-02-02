using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnable : MonoBehaviour
{
    public GameObject template;

    public int poolSize = 1;

    [SerializeField]
    private List<GameObject> pool;

    public int defaultObjectLifetime = int.MaxValue;

    void Awake()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject instance = Instantiate(template);
            instance.SetActive(false);
            pool.Add(instance);
        }
    }

    public Transform Spawn(Transform transform)
    {
        return Spawn(transform, _ => _);
    }

    Transform Spawn(Transform transform, System.Func<GameObject, GameObject> setup)
    {
        return Spawn(transform, setup, defaultObjectLifetime);
    }

    Transform Spawn(Transform transform, System.Func<GameObject, GameObject> setup, int lifetime)
    {
        GameObject inactivePooledObject = pool.Find(i => !i.activeInHierarchy);

        if (inactivePooledObject != null)
        {
            inactivePooledObject.transform.position = transform.position;
            inactivePooledObject.transform.rotation = transform.rotation;
            setup(inactivePooledObject);
            inactivePooledObject.SetActive(true);
            StartCoroutine(DisposeObjectAfter(inactivePooledObject, lifetime));
            return inactivePooledObject.transform;
        }
        else
        {
            Debug.Log("Van't return spawn");
            return null;
        }
    }

    IEnumerator DisposeObjectAfter(GameObject obj, int lifetime)
    {
        if (lifetime != int.MaxValue)
        {
            //This will wait for 63.000 seconds
            yield return new WaitForSeconds(lifetime);
            obj.SetActive(false);
        }
    }

    public bool Hide(GameObject obj)
    {
        if (!pool.Contains(obj)) return false;
        if (!obj.active) return false;
        obj.SetActive(false);
        return true;
    }
}
