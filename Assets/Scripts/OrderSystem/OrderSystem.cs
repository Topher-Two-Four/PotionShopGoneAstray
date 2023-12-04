using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderSystem : MonoBehaviour
{

    public Order[] orderList;
    public Customer[] customerList;

    void Start()
    {
        for (int x = 0; x < orderList.Length; x++)
        {
            Order currentOrder = orderList[x];
            Debug.Log(currentOrder);

            int randomCustomerIndex = Random.Range(0, customerList.Length);
            currentOrder.customer = customerList[randomCustomerIndex];
            currentOrder.customerName = customerList[randomCustomerIndex].customerName;
            currentOrder.customerPortrait = customerList[randomCustomerIndex].customerPortrait;

            currentOrder.customerPortraitDisplay.sprite = currentOrder.customerPortrait;
            currentOrder.orderText.SetText(currentOrder.customerName + " would like to buy a " + "_______ potion.");
            //currentOrder.turnInPotionButton.image = currentOrder.potionRequested.

            int randomPotionTypeIndex = Random.Range(0, 8);
            Debug.Log(randomPotionTypeIndex);
            /*
            switch (randomPotionTypeIndex) // Assign random potion type for request
            {
                case 8:
                    currentOrder.potionRequested.isAntidote = true;
                    break;
                case 7:
                    currentOrder.potionRequested.isBenefit = true;
                    break;
                case 6:
                    currentOrder.potionRequested.isCrippling = true;
                    break;
                case 5:
                    currentOrder.potionRequested.isDeath = true;
                    break;
                case 4:
                    currentOrder.potionRequested.isHatred = true;
                    break;
                case 3:
                    currentOrder.potionRequested.isHealth = true;
                    break;
                case 2:
                    currentOrder.potionRequested.isLove = true;
                    break;
                case 1:
                    currentOrder.potionRequested.isLucky = true;
                    break;
                case 0:
                    currentOrder.potionRequested.isPoison = true;
                    break;
                default:
                    break;
            }
            */
        }
    }

}
