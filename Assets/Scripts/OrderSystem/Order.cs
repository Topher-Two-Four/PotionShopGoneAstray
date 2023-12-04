using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Order : MonoBehaviour
{
    public Customer customer;
    public string customerName;
    public Sprite customerPortrait;
    public PotionData potionRequested;
    public Image customerPortraitDisplay;
    public TMP_Text orderText;
    public Button turnInPotionButton;

}
