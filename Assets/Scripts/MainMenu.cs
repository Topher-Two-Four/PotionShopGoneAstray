using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
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

    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }

}
