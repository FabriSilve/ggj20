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


    public float movementSpeed = 50f;

    private CharacterController controller;

    private float gravity = 0;

    public Item currentItem;

    void Interact()
    {
        //if (currentItem)
        //{
        //    currentItem.Interact();
        //}
    }


    void ControlPlayer()
    {
        //TODO we can use Kinnematic is is to avoid the gravity.

        gravity = controller.isGrounded ? 0 : gravity - (9.81f * Time.deltaTime);
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), gravity, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * movementSpeed);
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




    // Start is called before the first frame update
    void Start()
    {
        // Store reference to attached component
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        ControlPlayer();
    }
}
