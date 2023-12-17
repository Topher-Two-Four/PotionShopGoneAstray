using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class PotionData : ItemData
{
    public bool isHealth;
    public bool isLove;
    public bool isHatred;
    public bool isAntidote;
    public bool isDeath;
    public bool isLucky;
    public bool isPoison;
    public bool isBenefit;
    public bool isCrippling;

    public int quality; // The quality of the item from 1-5
    public int baseValue; // The base currency value of this item
    public int sellPrice;
    public int numberOfIngredients;
}
