using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreInventory : MonoBehaviour
{
    public List<Item> items;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int Weight() {
        int weight = 0;
        foreach (var item in items) {
            weight += item.weight;
        }
        return weight;
    }

    int Size() {
        return items.Count;
    }

    void Pick(Item item) {
        items.Remove(item);
    }
}

