using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBookManager : MonoBehaviour
{
    [Header("Recipe Book General Settings:")]
    [Tooltip("The UI canvas for the potion shop manual.")]
    [SerializeField] private Canvas apothecaryManualCanvas;
    //[Tooltip("The player's inventory grid.")]
    //[SerializeField] private GameObject inventoryGrid;

    [Header("Page Settings:")]
    [Tooltip("The index of the current book page.")]
    [SerializeField] private int bookPageIndex;
    [Tooltip("The list of book page game objects.")]
    [SerializeField] private GameObject[] bookPages; // Add in desired order

    [Header("Button Settings:")]
    [Tooltip("The button to turn the page forward.")]
    [SerializeField] private Button pageForwardButton;
    [Tooltip("The button to turn the page backwards.")]
    [SerializeField] private Button pageBackButton;
    [Tooltip("The button to toggle on and off the potion shop manual canvas.")]
    [SerializeField] private Button toggleBookCanvasButton;
    [Tooltip("The button to close the potion shop manual canvas.")]
    [SerializeField] private Button closeBookCanvasButton;



    private void Start()
    {
        bookPageIndex = 0;
        UpdatePageTurnButtonVisibility();
    }

    private void Awake()
    {
        toggleBookCanvasButton.onClick.AddListener(() => ToggleBook()); // Add button listener for toggling on and off manual from inventory
        if (closeBookCanvasButton != null && pageForwardButton != null && pageBackButton != null)
        {
            closeBookCanvasButton.onClick.AddListener(() => CloseBook()); // Add button listener for toggling on and off manual from inventory
            pageForwardButton.onClick.AddListener(() => TurnPageForward()); // Add button listener for first ingredient space
            pageBackButton.onClick.AddListener(() => TurnPageBack()); // Add button listener for second ingredient space\
        }
    }

    private void ToggleBook()
    {
        if (apothecaryManualCanvas.gameObject.activeSelf)
        {
            AudioManager.Instance.PlaySFX("CloseBook");
            apothecaryManualCanvas.gameObject.SetActive(false);
            //inventoryGrid.SetActive(true);
} 
        else
        {
            AudioManager.Instance.PlaySFX("OpenBook");
            apothecaryManualCanvas.gameObject.SetActive(true);
        }
    }

    private void CloseBook()
    {
        apothecaryManualCanvas.gameObject.SetActive(false);
    }

    private void TurnPageForward()
    {
        if (bookPageIndex < bookPages.Length)
        {
            AudioManager.Instance.PlaySFX("SwitchPage");
            bookPages[bookPageIndex].SetActive(false);
            bookPageIndex++;
            bookPages[bookPageIndex].SetActive(true);
            UpdatePageTurnButtonVisibility();
        }
    }

    private void TurnPageBack()
    {
        if (bookPageIndex > 0)
        {
            AudioManager.Instance.PlaySFX("SwitchPage");
            bookPages[bookPageIndex].SetActive(false);
            bookPageIndex--;
            bookPages[bookPageIndex].SetActive(true);
            UpdatePageTurnButtonVisibility();
        }
    }

    private void UpdatePageTurnButtonVisibility()
    {
        if (closeBookCanvasButton != null && pageForwardButton != null && pageBackButton != null)
        {
            if (bookPageIndex == 0) // First page condition
            {
                pageBackButton.gameObject.SetActive(false);
                pageForwardButton.gameObject.SetActive(true);
            }
            else if (bookPageIndex > 0 && bookPageIndex < bookPages.Length - 1) // Middle pages condition
            {
                pageBackButton.gameObject.SetActive(true);
                pageForwardButton.gameObject.SetActive(true);
            }
            else // Last page condition
            {
                pageBackButton.gameObject.SetActive(true);
                pageForwardButton.gameObject.SetActive(false);
            }
        }
    }

}
