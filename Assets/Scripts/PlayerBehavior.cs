using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float speed = 10;

    private CharacterController controller;

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
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
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
