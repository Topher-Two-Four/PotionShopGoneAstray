using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemObject : MonoBehaviour
{

    public ItemData itemData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log(other);
            InventoryController.Instance.PickUpItemObject(itemData);
            Debug.Log(itemData);
            Destroy(this.gameObject);
            //Add inventoryitem to inventory grid, if space available
            //If not, display message that there is no space left in inventory
            //Destroy 3D item
        }
    }

    public ItemData GetItemData()
    {
        return itemData;
    }

}
