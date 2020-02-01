using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Shop")
        {
            ApplicationManager.Instance.OpenShopingMenu();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Shop")
        {
            ApplicationManager.Instance.CloseShopingMenu();

        }
    }

    public float baseMovementSpeed = 50f;
    private float gravity = 0;

    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Inventory inventory;

    private void ControlPlayer()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), gravity, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * SpeedMultiplier());
    }

    private float SpeedMultiplier()
    {
        int weight = inventory.Weight();
        // Super simple formula to linearly decrease player speed based on inventory weight.
        float speedMultiplier = baseMovementSpeed - Mathf.Min(baseMovementSpeed / 2, weight);
        return speedMultiplier;
    }

    private void ComputeGravity()
    {
        gravity = controller.isGrounded ? 0 : gravity - (9.81f * Time.deltaTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        inventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        ComputeGravity();
        ControlPlayer();
    }
}
