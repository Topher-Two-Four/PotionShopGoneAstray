using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Customer : ScriptableObject
{
    [Header("Customer Info:")]
    [Tooltip("The name of the customer.")]
    public string customerName;
    [Tooltip("The portrait sprite for the customer.")]
    public Sprite customerPortrait;

}
