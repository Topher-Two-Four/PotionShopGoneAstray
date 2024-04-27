using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class PotionData : ItemData
{
    [Header("Potion Type: (Only Select One)")]
    [Tooltip("Whether this is a health-type potion.")]
    public bool isHealth;
    [Tooltip("Whether this is a love potion.")]
    public bool isLove;
    [Tooltip("Whether this is a hatred potion.")]
    public bool isHatred;
    [Tooltip("Whether this is an antidote potion.")]
    public bool isAntidote;
    [Tooltip("Whether this is a death potion.")]
    public bool isDeath;
    [Tooltip("Whether this is a lucky potion.")]
    public bool isLucky;
    [Tooltip("Whether this is a poison potion.")]
    public bool isPoison;
    [Tooltip("Whether this is a benefit potion.")]
    public bool isBenefit;
    [Tooltip("Whether this is a harmful potion.")]
    public bool isHarmful;

    [HideInInspector] public int quality; // The quality of the item from 1-5
    [HideInInspector] public int baseValue; // The base currency value of this item
    [HideInInspector] public int sellPrice;
    [HideInInspector] public int numberOfIngredients;
}
