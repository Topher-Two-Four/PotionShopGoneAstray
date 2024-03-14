using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    private float fieldOfView = 90.0f;
    [SerializeField] private Slider fieldOfViewSlider;
    [SerializeField] private TMP_Text fieldOfViewDisplay;
    private float playerLookSpeed = 1.0f;
    [SerializeField] private Slider playerLookSpeedSlider;

    [SerializeField] private float defaultFieldOfView = 90.0f;
    [SerializeField] private float defaultLookSpeed = 1.0f;


    private void Awake()
    {
        UpdateFOVDisplay();
    }

    public void ToggleFullScreen()
    {
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
    }

    public void ToggleMaxWindowed()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
    }

    public void ChangeHorizontalFOV()
    {
        fieldOfView = fieldOfViewSlider.value;
        playerCamera.m_Lens.FieldOfView = fieldOfViewSlider.value;
        UpdateFOVDisplay();
        PlayerPrefs.SetFloat("fieldOfView", fieldOfViewSlider.value);
    }

    private void LoadFOVPref()
    {
        fieldOfViewSlider.value = PlayerPrefs.GetFloat("fieldOfView");
    }

    private void SaveFOVPref()
    {
        PlayerPrefs.SetFloat("fieldOfView", fieldOfViewSlider.value);
    }

    private void UpdateFOVDisplay()
    {
        fieldOfViewDisplay.text = fieldOfView.ToString();
    }

    public void ChangeLookSpeed()
    {
        playerLookSpeed = playerLookSpeedSlider.value;
        GameManager.Instance.ChangePlayerLookSpeed(playerLookSpeed);
    }

    public void RestoreDefaultSettings()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
        
        playerLookSpeedSlider.value = defaultLookSpeed;
        GameManager.Instance.ChangePlayerLookSpeed(defaultLookSpeed);
        
        fieldOfViewSlider.value = defaultFieldOfView;
        playerCamera.m_Lens.FieldOfView = 90.0f;

        TutorialManager.Instance.ToggleOnTutorials();
    }

}