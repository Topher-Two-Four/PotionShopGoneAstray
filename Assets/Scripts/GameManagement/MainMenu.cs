using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("Main Menu Settings:")]
    [Tooltip("The canvas that holds the main menu UI elements.")]
    [SerializeField] private GameObject mainMenuCanvas;
    [Tooltip("The canvas that holds the settings UI elements.")]
    [SerializeField] private GameObject settingsCanvas;
    [Tooltip("The canvas that holds the credits UI elements.")]
    [SerializeField] private GameObject creditsCanvas;

    public void Start()
    {
        AudioManager.Instance.PlayMusic("MenuMusic");
    }

    public void OnNewGamePressed()
    {
        DataPersistenceManager.Instance.NewGame();
        GameManager.Instance.SwitchSceneToPotionShopWithNewGame();
    }

    public void OnContinueGamePressed()
    {
        GameManager.Instance.SwitchSceneToPotionShopWithLoadGame();
    }

    public void OnSettingsPressed()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(true);
        creditsCanvas.gameObject.SetActive(false);
    }
    public void OnCreditsPressed()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(false);
        creditsCanvas.gameObject.SetActive(true);
    }

    public void OnReturnToMainMenuPressed()
    {
        mainMenuCanvas.gameObject.SetActive(true);
        settingsCanvas.gameObject.SetActive(false);
        creditsCanvas.gameObject.SetActive(false);
    }

        public void OnQuitPressed()
    {
        Application.Quit();
    }

}
