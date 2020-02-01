using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        SinglePlank,
        Barrel,
        TriplePlank
    }

    private ItemType myItemClass;

    public static Action<Item> OnPurchaseMade;

    public int ID;
    public int weight = 1;
    public int price = 1;

    [SerializeField]
    Button myButton;

    bool hasSubscribedToEvent;


    void HandleCreditUpdated(int credit)
    {
        Debug.Log("Handling the event from button " + this.gameObject.name);

        myButton.interactable = credit >= price;

    }



    public void Interact()
    {
        switch (myItemClass)
        {
            default:
                break;
        }
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (!myButton)
        {
            myButton = GetComponent<Button>();
        }

        if (myButton)
        {
            if (Wallet.Instance)
            {
                if (!hasSubscribedToEvent)
                {
                    Wallet.OnCreditUpdated += HandleCreditUpdated;
                }
                myButton.interactable = Wallet.Instance.Credit() >= this.price;
            }

        }

    }



    public void BuyThisItem()
    {
        if (OnPurchaseMade != null)
        {
            OnPurchaseMade(this);

        }
    }
}
