using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [Header("Customers")]
    [SerializeField] private CustomerWindow[] customers;

    private void Start()
    {
        for (int i = 0; i < customers.Length; i++)
        {
            customers[i].CreateOrder();
        }
    }
}