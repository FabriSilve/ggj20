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
    public float rotationSpeed;
   
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Inventory inventory;
    [SerializeField]
    private CurrentTileDetector tileDetector;

    private Animator animator;
    private Vector3 prevPosition;

    private float gravity = 0;

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
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), gravity, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * SpeedMultiplier());
    }

    private void ComputeGravity()
    {
        gravity = controller.isGrounded ? 0 : gravity - (9.81f * Time.deltaTime);
    }

    private float SpeedMultiplier()
    {
        int weight = inventory.Weight();
        float waterSlowDown = tileDetector.IsOnHole() ? 0.5f : 1.0f; 
        // Super simple formula to linearly decrease player speed based on inventory weight.
        float speedMultiplier = (baseMovementSpeed - Mathf.Min(baseMovementSpeed / 2, weight)) * waterSlowDown;
        return speedMultiplier;
    }

    // Start is called before the first frame update
    void Start()
    {
        prevPosition = transform.position;
        controller = GetComponent<CharacterController>();
        inventory = GetComponent<Inventory>();
        animator = GetComponent<Animator>();
        tileDetector = GameObject.Find("ItemSpawnPoint").GetComponent<CurrentTileDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        ComputeGravity();
        ChangeSpeed();
        Rotate();
        prevPosition = transform.position;
        Move();
    }
}
