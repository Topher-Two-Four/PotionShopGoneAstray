using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToSpawnOnAwake : MonoBehaviour
{

    private void Awake()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(3))
        {
            gameObject.transform.position = Vector3.zero;
            Debug.Log("Moved to spawn.");
        } 
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(4))
        {
            gameObject.transform.position = Vector3.zero;
            Debug.Log("Moved to spawn.");
        }
    }

}
