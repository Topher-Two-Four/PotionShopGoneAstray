using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseTutorialOnClick : MonoBehaviour
{
    private Button _tutorialWindowButton;
    private GameObject _tutorialWindow;

    void Start()
    {
        if (gameObject.GetComponent<Button>() != null)
        {
            _tutorialWindow = gameObject;
            _tutorialWindowButton = gameObject.GetComponent<Button>();
            _tutorialWindowButton.onClick.AddListener(() => CloseTutorialWindow());
        }
    }

    private void CloseTutorialWindow()
    {
        _tutorialWindow.SetActive(false);

        GameManager.Instance.ToggleCursorOn(); // Catch edge case where cursor gets disabled due to click occurring
    }

}
