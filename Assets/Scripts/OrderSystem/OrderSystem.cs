using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSystem : MonoBehaviour
{

    public Order[] orderList;

    // Start is called before the first frame update
    void Start()
    {
        int randomPotionTypeIndex = Random.Range(0, 8);

        switch (randomPotionTypeIndex)
        {
            case 8:
                break;
            case 7:
                break;
            case 6:
                break;
            case 5:
                break;
            case 4:
                break;
            case 3:
                break;
            case 2:
                break;
            case 1:
                break;
            case 0:
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
