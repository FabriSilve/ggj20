using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float speed = 50f;

    private CharacterController controller;

    private float gravity = 0;

    public Item currentItem;

    void Interact()
    {
        if (currentItem)
        {
            currentItem.Interact();
        }
    }


    void ControlPlayer()
    {
        gravity = controller.isGrounded ? 0 : gravity - (9.81f * Time.deltaTime);
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), gravity, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * speed);
    }



    // Start is called before the first frame update
    void Start(){
		// Store reference to attached component
		controller = GetComponent<CharacterController>();
	}

    // Update is called once per frame
    void Update()
    {
        ControlPlayer();
    }
}
