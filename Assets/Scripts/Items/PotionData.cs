using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class PotionData : ItemData
{
    [Header("Potion Type:")]
    public bool isHealth;
    public bool isLove;
    public bool isHatred;
    public bool isAntidote;
    public bool isDeath;
    public bool isLucky;
    public bool isPoison;
    public bool isBenefit;
    public bool isCrippling;

    [HideInInspector] public int quality; // The quality of the item from 1-5
    [HideInInspector] public int baseValue; // The base currency value of this item
    [HideInInspector] public int sellPrice;
    [HideInInspector] public int numberOfIngredients;
}
