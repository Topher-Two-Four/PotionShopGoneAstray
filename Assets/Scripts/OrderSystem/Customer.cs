using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Customer : ScriptableObject
{
    [Header("Customer Info:")]
    public string customerName;
    public Sprite customerPortrait;

}
