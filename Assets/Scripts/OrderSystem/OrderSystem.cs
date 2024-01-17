using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderSystem : MonoBehaviour
{
    [Header("Order and Customer Lists:")]
    [Tooltip("The list of orders that is randomly generated each new day.")]
    [SerializeField] private Order[] orderList;
    [Tooltip("The list of customers that can be chosen at random to be the orderer.")]
    [SerializeField] private Customer[] customerList;

    [Header("Potion Icons:")]
    [Tooltip("The sprite for the antidote potion icon.")]
    [SerializeField] private Sprite antidoteIcon;
    [Tooltip("The sprite for the benefit potion icon.")]
    [SerializeField] private Sprite benefitIcon;
    [Tooltip("The sprite for the crippling potion icon.")]
    [SerializeField] private Sprite cripplingIcon;
    [Tooltip("The sprite for the death potion icon.")]
    [SerializeField] private Sprite deathIcon;
    [Tooltip("The sprite for the hatred potion icon.")]
    [SerializeField] private Sprite hatredIcon;
    [Tooltip("The sprite for the health potion icon.")]
    [SerializeField] private Sprite healthIcon;
    [Tooltip("The sprite for the love potion icon.")]
    [SerializeField] private Sprite loveIcon;
    [Tooltip("The sprite for the lucky potion icon.")]
    [SerializeField] private Sprite luckyIcon;
    [Tooltip("The sprite for the poison potion icon.")]
    [SerializeField] private Sprite poisonIcon;

    [Header("Text Settings:")]
    [SerializeField] private string potionTypeText;

    public static OrderSystem Instance { get; private set; } // Singleton logic

    void Start()
    {
        if (Instance != null && Instance != this) { Destroy(this); } else { Instance = this; } // Singleton logic

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

    public void GenerateOrderList()
    {
        for (int x = 0; x < orderList.Length; x++)
        {
            Order currentOrder = orderList[x];

            currentOrder.orderCompletedMask.gameObject.SetActive(false);
            currentOrder.turnInPotionButton.gameObject.SetActive(true);

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

    public void CheckForCompleteOrders()
    {
        foreach (Order order in orderList)
        {
            PotionData potion = InventoryController.Instance.FindPotionOfType(order);
            if (potion != null)
            {
                order.turnInPotionButton.interactable = true;
            } 
            else
            {
                order.turnInPotionButton.interactable = false;
            }
        }
    }

}
