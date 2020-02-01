using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float baseMovementSpeed = 50f;
    public float rotationSpeed;
   
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Inventory inventory;

    private Animator animator;
    private Vector3 prevPosition;

    private void Rotate()
    {
        Vector3 direction = transform.position - prevPosition;
        direction.Normalize();

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(direction),
                Time.deltaTime * rotationSpeed
            );
        }
    }

    private void ChangeSpeed()
    {
        float speed = (prevPosition - transform.position).magnitude / Time.deltaTime;
        animator.SetFloat("speed", speed);
    }

    private void Move()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * SpeedMultiplier());
    }

    private float SpeedMultiplier()
    {
        int weight = inventory.Weight();
        // Super simple formula to linearly decrease player speed based on inventory weight.
        float speedMultiplier = baseMovementSpeed - Mathf.Min(baseMovementSpeed / 2, weight);
        return speedMultiplier;
    }

    // Start is called before the first frame update
    void Start()
    {
        prevPosition = transform.position;
        controller = GetComponent<CharacterController>();
        inventory = GetComponent<Inventory>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSpeed();
        Rotate();
        prevPosition = transform.position;
        Move();
    }
}
