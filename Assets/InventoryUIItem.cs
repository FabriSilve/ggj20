using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIItem : MonoBehaviour
{
    public Item.ItemType myItemType;

    public int ammountRemaining = 0;

    [SerializeField]
    Image iconImage;

    [SerializeField]
    TMPro.TextMeshProUGUI textAmmount;


    private void Awake()
    {
        UpdateVisuals();
    }

    public void AddMoreAmmount(int ammount)
    {
        ammountRemaining += ammount;
        UpdateVisuals();
    }

    public void RemoveAmmount(int ammount)
    {
        ammountRemaining -= ammount;
        UpdateVisuals();
    }
    void UpdateVisuals()
    {
        if (ammountRemaining > 0)
        {
            iconImage.color = Color.white;

        }
        else
        {
            Color faded = new Color(1, 1, 1, 0.2f);
            iconImage.color = faded;
        }
        textAmmount.text = ammountRemaining.ToString();
    }

}
