using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxSize = 10;
    private List<Item> storage = new List<Item>();

    // Start is called before the first frame update
    void Start()
    {
        bank = GetComponent<Bank>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int Weight() {
        int weight = 0;
        foreach (var item in storage) {
            weight += item.weight;
        }
        return weight;
    }

    int Size() {
        return storage.Count;
    }

    int IsFull() {
        return Size() == maxSize;
    }

    bool Insert(Item item) {
        if (IsFull()) {
            return false;
        } else {
            storage.Add(item);
            return true;
        }
    }

    void Consume(Item item) {
        storage.Remove(item);
    }
}
