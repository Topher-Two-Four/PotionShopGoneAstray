using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderSystem : MonoBehaviour
{
    public Order[] orderList;
    public Customer[] customerList;

    public Sprite antidoteIcon;
    public Sprite benefitIcon;
    public Sprite cripplingIcon;
    public Sprite deathIcon;
    public Sprite hatredIcon;
    public Sprite healthIcon;
    public Sprite loveIcon;
    public Sprite luckyIcon;
    public Sprite poisonIcon;

    public string potionTypeText;

    public static OrderSystem Instance { get; private set; } // Singleton logic

    void Start()
    {
        for (int x = 0; x < orderList.Length; x++)
        {
            Order currentOrder = orderList[x];

            int randomCustomerIndex = Random.Range(0, customerList.Length);
            currentOrder.customer = customerList[randomCustomerIndex];
            currentOrder.customerName = customerList[randomCustomerIndex].customerName;
            currentOrder.customerPortrait = customerList[randomCustomerIndex].customerPortrait;

            int randomPotionTypeIndex = Random.Range(0, 8);

            currentOrder.turnInPotionButton.onClick.AddListener(() => InventoryController.Instance.SellPotion(currentOrder));
            currentOrder.turnInPotionButton.interactable = false;

            switch (randomPotionTypeIndex) // Assign random potion type for request
            {
                case 8:
                    currentOrder.isAntidote = true;
                    potionTypeText = "Antidote";
                    currentOrder.turnInPotionButtonImage.sprite = antidoteIcon;
                    break;
                case 7:
                    currentOrder.isBenefit = true;
                    potionTypeText = "Benefit";
                    currentOrder.turnInPotionButtonImage.sprite = benefitIcon;
                    break;
                case 6:
                    currentOrder.isCrippling = true;
                    potionTypeText = "Crippling";
                    currentOrder.turnInPotionButtonImage.sprite = cripplingIcon;
                    break;
                case 5:
                    currentOrder.isDeath = true;
                    potionTypeText = "Death";
                    currentOrder.turnInPotionButtonImage.sprite = deathIcon;
                    break;
                case 4:
                    currentOrder.isHatred = true;
                    potionTypeText = "Hatred";
                    currentOrder.turnInPotionButtonImage.sprite = hatredIcon;
                    break;
                case 3:
                    currentOrder.isHealth = true;
                    potionTypeText = "Health";
                    currentOrder.turnInPotionButtonImage.sprite = healthIcon;
                    break;
                case 2:
                    currentOrder.isLove = true;
                    potionTypeText = "Love";
                    currentOrder.turnInPotionButtonImage.sprite = loveIcon;
                    break;
                case 1:
                    currentOrder.isLucky = true;
                    potionTypeText = "Luck";
                    currentOrder.turnInPotionButtonImage.sprite = luckyIcon;
                    break;
                case 0:
                    currentOrder.isPoison = true;
                    potionTypeText = "Poison";
                    currentOrder.turnInPotionButtonImage.sprite = poisonIcon;
                    break;
                default:
                    break;
            }

            currentOrder.customerPortraitDisplay.sprite = currentOrder.customerPortrait;
            currentOrder.orderText.SetText(currentOrder.customerName + " would like to buy a " + potionTypeText + " potion.");
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(this); } else { Instance = this; } // Singleton logic
    }

    public void CheckForCompleteOrders()
    {
        foreach (Order order in orderList)
        {
            PotionData potion = InventoryController.Instance.FindPotionOfType(order);
            if (potion != null)
            {
                order.turnInPotionButton.interactable = true;
                Debug.Log(potion);
            } 
            else
            {
                order.turnInPotionButton.interactable = false;
            }
        }
    }

}
