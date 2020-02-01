using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Wallet : MonoBehaviour
{
    public static Action<int> OnCreditUpdated;
    private static Wallet _instance;
    public static Wallet Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            Item.OnPurchaseMade += HandleItemPurchase;

        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void HandleItemPurchase(Item newItem)
    {
        credit -= newItem.price;
        if (OnCreditUpdated != null)
        {
            OnCreditUpdated(credit);

            IventoryMenu.Instance.UpdateMoney(credit);
        }
        else
        {
            Debug.Log("Noone is listening");
        }
        Debug.LogFormat("I purchased item with id {0} for price {1} my remaining money is {2}", newItem.ID, newItem.price, credit);
    }
    [SerializeField]
    private int credit = 0;



    public int Credit()
    {
        return credit;
    }

    public void AddCredit(int amount)
    {
        credit += amount;
    }

    public bool CanWithdraw(int amount)
    {
        return amount <= credit;
    }

    public bool Withdraw(int amount)
    {
        if (CanWithdraw(amount))
        {
            credit -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }
}
