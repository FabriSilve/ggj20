using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreBehavior : MonoBehaviour
{
    public List<Item> items;

    public int Weight()
    {
        int weight = 0;
        foreach (var item in items)
        {
            weight += item.weight;
        }
        return weight;
    }

    public int Size()
    {
        return items.Count;
    }

    public bool CanBuy(Item item, Wallet wallet, Inventory inventory)
    {
        if (!items.Contains(item)) return false;
        if (inventory.IsFull()) return false;
        if (wallet.CanWithdraw(item.price)) return false;
        return true;
    }

    public bool Buy(Item itemToBuy, Wallet playerWallet, Inventory playerInventory)
    {
        if (CanBuy(itemToBuy, playerWallet, playerInventory))
        {
            playerWallet.Withdraw(itemToBuy.price);
            items.Remove(itemToBuy);
            playerInventory.Insert(itemToBuy);
            return true;
        }
        else
        {
            return false;
        }

    }
}

