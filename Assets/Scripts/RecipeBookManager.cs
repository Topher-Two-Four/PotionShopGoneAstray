using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBookManager : MonoBehaviour
{
    public Canvas apothecaryManualCanvas;

    public int bookPageIndex;
    public GameObject[] bookPages; // Add in desired order

    public Button pageForwardButton;
    public Button pageBackButton;
    public Button toggleBookCanvasButton;
    public GameObject inventoryGrid;

    private void Start()
    {
        bookPageIndex = 0;
        UpdatePageTurnButtonVisibility();
    }

    private void Awake()
    {
        toggleBookCanvasButton.onClick.AddListener(() => ToggleBook()); // Add button listener for toggling on and off manual from inventory
        pageForwardButton.onClick.AddListener(() => TurnPageForward()); // Add button listener for first ingredient space
        pageBackButton.onClick.AddListener(() => TurnPageBack()); // Add button listener for second ingredient space
    }

    private void ToggleBook()
    {
        if (apothecaryManualCanvas.gameObject.activeSelf)
        {
            apothecaryManualCanvas.gameObject.SetActive(false);
            inventoryGrid.SetActive(true);
} 
        else
        {
            apothecaryManualCanvas.gameObject.SetActive(true);
        }
    }

    private void TurnPageForward()
    {
        if (bookPageIndex < bookPages.Length)
        {
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
            bookPages[bookPageIndex].SetActive(false);
            bookPageIndex--;
            bookPages[bookPageIndex].SetActive(true);
            UpdatePageTurnButtonVisibility();
        }
    }

    private void UpdatePageTurnButtonVisibility()
    {
        if (bookPageIndex <= 0) // First page condition
        {
            pageBackButton.gameObject.SetActive(false);
            pageForwardButton.gameObject.SetActive(true);
        }
        else if (bookPageIndex > 0 && bookPageIndex < bookPages.Length) // Middle pages condition
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
