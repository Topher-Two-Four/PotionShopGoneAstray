using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData
{
	public int playerCurrency;
	public int landlordPayment;
	public int currentDay;
	public int playerMorality;
	public ItemGrid playerInventory;

	public GameData()
	{
		this.playerCurrency = 0;
		this.landlordPayment = 1400;
		this.currentDay = 1;
		this.playerMorality = 0;
		this.playerInventory = null;
	}
}
