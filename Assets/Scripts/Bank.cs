using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    [SerializeField]
    private int credit = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int Credit() {
        return credit;
    }

    void AddCredit(int amount) {
        credit += amount;
    }

    bool CanWithDraw(int amount) {
        return amount <= credit;
    }

    bool Withdraw(int amount) {
        if (CanWithDrawCredit(amount)) {
            credit -= amount;
            return true;
        } else {
            return false;
        }
    }
}
