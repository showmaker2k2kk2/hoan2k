using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int MaxHealth = 100;
    int curentHealth;

    void Start()
    {
        curentHealth = MaxHealth;

    }

    // Update is called once per frame
   public  void Takedame(int damage)
    {
        curentHealth -= damage;
        if (curentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("dm chet me r");
    }
}
