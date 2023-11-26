using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // Do not destroy this game object when new scene is loaded
    }

}
