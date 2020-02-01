using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float baseMovementSpeed = 50f;
    private float gravity = 0;

    [SerializeField]
    private CharacterController controller;

    private void ControlPlayer()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), gravity, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * SpeedMultiplier());
    }

    private void ComputeGravity()
    {
        gravity = controller.isGrounded ? 0 : gravity - (9.81f * Time.deltaTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        ComputeGravity();
        ControlPlayer();
    }
}
