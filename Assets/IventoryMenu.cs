using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IventoryMenu : MonoBehaviour
{
    private static IventoryMenu _instance;
    public static IventoryMenu Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;

        }

    }

    [SerializeField]
    InventoryUIItem singlePlank;

    [SerializeField]
    InventoryUIItem barel;

    [SerializeField]
    InventoryUIItem triplePlank;


    [SerializeField]
    TMPro.TextMeshProUGUI moneyLeft;


    bool hasSubscribedToButtons;
    bool hasSubscribedToInteract;

    public void UpdateMoney(int ammount)
    {
        moneyLeft.text = "$" + ammount.ToString();
    }


    public void HandleItemPurchased(Item newItem)
    {
        switch (newItem.type)
        {
            case Item.ItemType.SinglePlank:
                singlePlank.AddMoreAmmount(1);
                break;
            case Item.ItemType.Barrel:
                barel.AddMoreAmmount(1);
                break;
            case Item.ItemType.TriplePlank:
                triplePlank.AddMoreAmmount(1);
                break;
            default:
                break;
        }
    }

    public void HandleItemUsed(Item newItem)
    {
        switch (newItem.type)
        {
            case Item.ItemType.SinglePlank:
                singlePlank.RemoveAmmount(1);
                break;
            case Item.ItemType.Barrel:
                barel.RemoveAmmount(1);
                break;
            case Item.ItemType.TriplePlank:
                triplePlank.RemoveAmmount(1);
                break;
            default:
                break;
        }
    }


}
