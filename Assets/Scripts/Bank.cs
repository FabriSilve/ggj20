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

    public int Credit() {
        return credit;
    }

    public void AddCredit(int amount) {
        credit += amount;
    }

    public bool CanWithdraw(int amount) {
        return amount <= credit;
    }

    public bool Withdraw(int amount) {
        if (CanWithdraw(amount)) {
            credit -= amount;
            return true;
        } else {
            return false;
        }
    }
}
