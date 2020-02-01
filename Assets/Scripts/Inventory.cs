using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    int maxSize = 4;
    private List<Item> storage = new List<Item>();


    float forwardThreshold = 0.01f;
    float backwardThreshold = -0.01f;

    private Item currentActiveItem = null;

    public Item GetActiveItem()
    {
        Item itemMock = new Item();
        itemMock.type = Item.ItemType.SinglePlank;
        return itemMock;
    }

    void ScrollForActiveItem()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float delta = Input.GetAxis("Mouse ScrollWheel");
            Debug.Log("delta: " + delta);

            if (delta > forwardThreshold)
            {
                Debug.Log("I should move Forward the active Item");
            }
            /* do stuff */
        }
    }

    //void UseOfActiveItem()
    //{
    //    if (currentActiveItem)
    //    {
    //        currentActiveItem.Interact();
    //    }
    //    else
    //    {
    //        //Play a sound of error
    //    }
    //}
    // Update is called once per frame
    void Update()
    {
        ScrollForActiveItem();
        //UseOfActiveItem();
    }

    public int Weight()
    {
        int weight = 0;
        foreach (var item in storage)
        {
            weight += item.weight;
        }
        return weight;
    }

    public int Size()
    {
        return storage.Count;
    }

    public bool IsFull()
    {
        return Size() == maxSize;
    }

    public bool Insert(Item item)
    {
        if (IsFull())
        {
            return false;
        }
        else
        {
            storage.Add(item);
            return true;
        }
    }

    public void Consume(Item item)
    {
        storage.Remove(item);
    }
}
