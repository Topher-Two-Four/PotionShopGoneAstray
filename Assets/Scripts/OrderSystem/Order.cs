using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Order : MonoBehaviour
{
    [Header("Item Traits:")]

    public Image customerPortraitDisplay;
    public TMP_Text orderText;
    public Button turnInPotionButton;
    public Image turnInPotionButtonImage;
    public Image orderCompletedMask;

    [HideInInspector] public Customer customer;
    [HideInInspector] public string customerName;
    [HideInInspector] public Sprite customerPortrait;
    [HideInInspector] public PotionData potionRequested;

    [HideInInspector] public bool isHealth;
    [HideInInspector] public bool isLove;
    [HideInInspector] public bool isHatred;
    [HideInInspector] public bool isAntidote;
    [HideInInspector] public bool isDeath;
    [HideInInspector] public bool isLucky;
    [HideInInspector] public bool isPoison;
    [HideInInspector] public bool isBenefit;
    [HideInInspector] public bool isHarmful;
}
