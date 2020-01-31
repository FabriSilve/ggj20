using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    GameObject theHammer;

    Rigidbody hammerRB;
    [SerializeField]
    float magnitude = 10;
    // Start is called before the first frame update
    void Start()
    {
        hammerRB = theHammer.GetComponent<Rigidbody>();
        hammerRB.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        HandleHammerThrowing();
    }
    //TODO move this logic to the hammer

    bool shouldHammerMoveForward;
    void HandleHammerThrowing()
    {
        shouldHammerMoveForward = Input.GetMouseButton(0);
        hammerRB.isKinematic = !shouldHammerMoveForward;

        if (shouldHammerMoveForward)
        {
            hammerRB.transform.eulerAngles += new Vector3(1, 0, 0);
            hammerRB.AddForce(Camera.main.transform.forward * magnitude);

        }
    }


}
