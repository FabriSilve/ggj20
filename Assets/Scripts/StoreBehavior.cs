using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreBehavior : MonoBehaviour
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

    bool Buy(Item item, Bank bank, Inventory inventory) {
        if (!items.Contains(item)) return false;
        if (inventory.IsFull()) return false;
        if (bank.CanWithDraw(item.price)) return false;
        bank.Withdraw(item.price);
        items.Remove(item);
        inventory.Insert(item);
        return true;
    }
}

