using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
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

    public Item currentItem;

    void Interact()
    {
        //if (currentItem)
        //{
        //    currentItem.Interact();
        //}
    }

    void ControlPlayerWithArrows()
    {
        switch (currentcontrolMode)
        {
            case ControlMode.SinglePlayer:
                //TODO use some sort of controls
                //TODO Use for now the Keyboard
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    this.transform.position += transform.right * -1;
                    //Rotate Left
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    this.transform.position += transform.right;

                    //Rotate Right

                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    this.transform.position += transform.forward;

                    //Move Forward
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    this.transform.position += transform.forward * -1;

                    //Move Backwards

                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    //Interact
                }
                break;

            case ControlMode.CoOp:
                //TODO do the same but split the parts.
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    //Rotate Left
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    //Rotate Right

                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    //Move Forward
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    //Move Backwards

                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    //Interact
                    Interact();
                }

                break;
            default:
                break;
        }
    }

    private float SpeedMultiplier() {
        int weight = inventory.Weight();
        // Super simple formula to linearly decrease player speed based on inventory weight.
        float speedMultiplier = baseMovementSpeed - Mathf.Min(baseMovementSpeed/2, weight);
        return speedMultiplier;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Store reference to attached components
        controller = GetComponent<CharacterController>();
        inventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
