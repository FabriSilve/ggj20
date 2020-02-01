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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int Weight() {
        int weight = 0;
        foreach (var item in storage) {
            weight += item.weight;
        }
        return weight;
    }

    public int Size() {
        return storage.Count;
    }

    public bool IsFull() {
        return Size() == maxSize;
    }

    public bool Insert(Item item) {
        if (IsFull()) {
            return false;
        } else {
            storage.Add(item);
            return true;
        }
    }

    public void Consume(Item item) {
        storage.Remove(item);
    }
}
