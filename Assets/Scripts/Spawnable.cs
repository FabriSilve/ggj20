﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnable : MonoBehaviour
{
    public GameObject template;
    
    public int poolSize = 1;
    
    [SerializeField]
    private List<GameObject> pool;

    public int defaultObjectLifetime = int.MaxValue;

    void Awake() {
        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++) {
            GameObject instance = Instantiate(template);
            instance.SetActive(false);
            pool.Add(instance);
        }
    }

    bool Spawn(Transform transform) {
        return Spawn(transform, _ => _);
    }

    bool Spawn(Transform transform, System.Func<GameObject, GameObject> setup) {
        return Spawn(transform, match, defaultObjectLifetime);
    }

    bool Spawn(Transform transform, System.Func<GameObject, GameObject> setup, int lifetime) {
        GameObject inactivePooledObject = pool.Find(i => !i.activeInHierarchy);

        if (inactivePooledObject != null) {
            inactivePooledObject.transform.position = transform.position;
            inactivePooledObject.transform.rotation = transform.rotation;
            setup(inactivePooledObject);
            inactivePooledObject.SetActive(true);
            StartCoroutine(DisposeObjectAfter(inactivePooledObject, lifetime));
            return true;
        } else {
            return false;
        }
    }

    IEnumerator DisposeObjectAfter(GameObject obj, int lifetime) {
        if (lifetime != int.MaxValue) {
            yield return new WaitForSeconds(lifetime);
            obj.SetActive(false);
        }
    }

    bool Hide(GameObject obj) {
        if (!pool.Contains(obj)) return false;
        if (!obj.active) return false;
        obj.SetActive(false);
        return true;
    }
}
