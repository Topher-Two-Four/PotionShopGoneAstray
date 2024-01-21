using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButtonScript : MonoBehaviour
{
    public Button button;



    private void Awake()
    {
        button.onClick.AddListener(() => ReturnToMainMenu());

    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }





}
