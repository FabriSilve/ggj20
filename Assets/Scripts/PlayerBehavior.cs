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

    public LevelManager levelManager;

    private CurrentTileDetector tileDetector;

    void Interact()
    {
        Item item = inventory.GetActiveItem();
        GameObject tile = tileDetector.GetCurrentTile();

        Debug.Log("Interact with " + item + " and " + tile);

        if (tile != null)
        {
            //switch (item.type)
            //{
            //    case Item.ItemType.SinglePlank:
            levelManager.FixTile(tile);

            //IventoryMenu.Instance.HandleItemUsed(item);

            Instantiate(barrelPrefab, tile.transform.position, Quaternion.identity);
            //break;

            //        default:
            //            break;
            //    }
            //} else {
            // TODO: play error sound
            //}
        }
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
        tileDetector = itemSpawnPoint.GetComponent<CurrentTileDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        ControlInput();
    }
}
