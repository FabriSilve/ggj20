using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerBehavior : MonoBehaviour
{
    public static Action<Item> OnItemUsed;
    public enum ControlMode
    {
        SinglePlayer,
        CoOp
    }

    public ControlMode currentcontrolMode;

    [SerializeField]
    private CharacterController controller;

    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    GameObject barrelPrefab;

    GameObject itemSpawnPoint;

    void Interact()
    {
        Item item = inventory.GetActiveItem();

        //if (item != null)
        //{
        //    switch (item.type)
        //    {
        //        case Item.ItemType.SinglePlank:
        Debug.Log("using single plank");

        //break;

        //        default:
        //            break;
        //    }
        //} else {
        // TODO: play error sound
        //}

        if (item)
        {
            IventoryMenu.Instance.HandleItemUsed(item);
        }


       Instantiate(barrelPrefab, itemSpawnPoint.transform.position, Quaternion.identity);

    }

    void ControlInput()
    {
        switch (currentcontrolMode)
        {
            case ControlMode.SinglePlayer:
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Interact();
                }
                break;

            default:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Store reference to attached components
        controller = GetComponent<CharacterController>();
        inventory = GetComponent<Inventory>();

        itemSpawnPoint = GameObject.Find("ItemSpawnPoint");
    }

    // Update is called once per frame
    void Update()
    {
        ControlInput();
    }
}
