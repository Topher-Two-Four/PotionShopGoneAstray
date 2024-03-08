using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseQuestBook : MonoBehaviour
{
    [SerializeField] private GameObject questBookCanvas;
    [SerializeField] private Button closeCanvasButton;

    void Start()
    {
        closeCanvasButton.onClick.AddListener (() => CloseQuestBookCanvas());
    }

    private void CloseQuestBookCanvas()
    {
        questBookCanvas.SetActive(false);
    }

}
