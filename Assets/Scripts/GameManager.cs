using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using StarterAssets;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public float timeRemaining = 900; // Time remaining in day
    public bool isTimerRunning; // Variable to keep track of if timer is still running
    public bool isMorning; // Variable to keep track of whether it's morning
    public bool isAfternoon; // Variable to keep track of whether it's afternoon
    public bool isEvening; // Variable to keep track of whether it's evening
    public bool isEndOfDay; // Variable to keep track of whether the day is over
    public TMP_Text timeRemainingText; // Time remaining to display as text
    public TMP_Text timeOfDayText; // Time of day to display as text

    public int playerCurrency = 0;
    public int landlordPayment = 200;
    public int potionValue;

    public FirstPersonController controller; // First person controller game object
    public GameObject playerCapsule; // Player capsule collider

    public GameObject potionCraftingCanvas;
    public GameObject doorToMaze;

    private bool morningTransitionComplete = false; // Keep track of transition from beginning of new day to morning
    private bool afternoonTransitionComplete = false; // Keep track of transition from morning to afternoon
    private bool eveningTransitionComplete = false; // Keep track of transition from afternoon to evening
    private bool endOfDayTransitionComplete = false; // Keep track of transition from evening to end of day

    public static GameManager Instance { get; private set; } // Singleton logic
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        isTimerRunning = true; // Set timer running to true when starting game/day
        Cursor.lockState = CursorLockMode.Confined; // Lock cursor in one place
        Cursor.visible = true; // Hide cursor
        Debug.Log(playerCapsule); 
        playerCapsule.SetActive(false); // Deactivate player capsule
    }

    private void Update()
    {
        if (isTimerRunning == true) // Calculate time if timer is still running
        {
            timeRemaining -= Time.deltaTime; // Decrement time using deltaTime
            //Debug.Log(timeRemaining); // Print remaining time
            TimerUpdate(); // Update timer every frame
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            SetPlayerCapsuleActive();
            Invoke("LoadMazeLevel", .1f);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            TogglePotionCraftingCanvas();
        }

    }


    public void ExitGame()
    {
        Application.Quit(); // Exit game to desktop
    }

    public void TogglePotionCraftingCanvas()
    {
        if (potionCraftingCanvas.gameObject.activeSelf)
        {
            potionCraftingCanvas.SetActive(false);
        }
        else
        {
            potionCraftingCanvas.SetActive(true);
        }
    }
    public void ToggleOnPotionCraftingCanvas()
    {
            potionCraftingCanvas.SetActive(true);
    }
    public void ToggleOffPotionCraftingCanvas()
    {
            potionCraftingCanvas.SetActive(false);
    }
    public void ToggleOnDoorToMaze()
    {
        doorToMaze.SetActive(true);
    }
    public void ToggleOffDoorToMaze()
    {
        doorToMaze.SetActive(false);
    }

    public void SwitchSceneToMainMenu() // Use scene manager to switch to Main Menu
    {
        SceneManager.LoadScene(0); // Load scene through scene manager
        Cursor.lockState = CursorLockMode.Confined; // Unlock cursor, confine to game screen
        Cursor.visible = true; // Display cursor

    }

    public void SwitchSceneToSettingsMenu() // Use scene manager to switch to Settings Menu
    {
        SceneManager.LoadScene(1); // Load scene through scene manager
        Cursor.lockState = CursorLockMode.Confined; // Unlock cursor, confine to game screen
        Cursor.visible = true; // Display cursor

    }

    public void SwitchSceneToPotionLevel() // Use scene manager to switch to Potion Level
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(3))
        {
            LoadMazeLevel();
        }

        SceneManager.LoadScene(2); // Load scene through scene manager
        Cursor.lockState = CursorLockMode.Confined; // Lock cursor in one place
        Cursor.visible = true; // Hide cursor
        playerCapsule.SetActive(false); // Deactivate player capsule
    }

    public void SwitchSceneToMazeLevel() // Use scene manager to switch to Maze Level
    {
        SceneManager.LoadScene(3); // Use scene manager to load second scene from scene list (settings menu)
        LoadMazeLevel();
    }

    private void LoadMazeLevel() // Place player in correct spot when maze is loaded
    {
        Cursor.lockState = CursorLockMode.Locked; // Unlock cursor, confine to game screen
        Cursor.visible = false; // Display cursor

        SetPlayerCapsuleActive(); // Ensure the player capsule is active

        Vector3 spawnPoint = new Vector3(-25, -55, 25);

        Debug.Log(spawnPoint);

        controller.MoveToPosition(spawnPoint); // Move player controller to player spawn point

        Debug.Log("Maze level loaded");
    }

    private void SetPlayerCapsuleActive()
    {
        Debug.Log(playerCapsule);
        playerCapsule.SetActive(true); // Set player capsule active
    }

    private void TimerUpdate() // Update timer and keep track of what time of day it is
    {
        if (timeRemaining > 600)
        {
            isMorning = true; isAfternoon = false; isEvening = false; isEndOfDay = false; // Set time of day to morning

            if (morningTransitionComplete == false) // Morning transition block, which will run once at the beginning of the morning
            {
                // Add additional new day to morning transition logic here as needed
                morningTransitionComplete = true; // Set transition status to complete
                timeOfDayText.text = ("Morning"); // Display time of day text on screen
                timeOfDayText.color = Color.white; // Set color of text to white
                //Debug.Log("It's now morning."); // Print morning notification
            }
        }
        else if (timeRemaining <= 600 && timeRemaining > 300)
        {
            isMorning = false; isAfternoon = true; isEvening = false; isEndOfDay = false; // Set time of day to afternoon
            if (afternoonTransitionComplete == false) // Afternoon transition block, which will run once at the beginning of the afternoon
            {
                // Add additional morning to afternoon transition logic here as needed
                afternoonTransitionComplete = true; // Set transition status to complete
                timeOfDayText.text = ("Afternoon"); // Display time of day text on screen
                //Debug.Log("It's now afternoon."); // Print afternoon notification
            }
        }
        else if (timeRemaining <= 300 && timeRemaining > 0)
        {
            isMorning = false; isAfternoon = false; isEvening = true; isEndOfDay = false; // Set time of day to evening
            if (eveningTransitionComplete == false) // Evening transition block, which will run once at the beginning of the evening
            {
                // Add additional afternoon to evening transition logic here as needed
                eveningTransitionComplete = true; // Set transition status to complete
                timeOfDayText.text = ("Evening"); // Display time of day text on screen
                //Debug.Log("It's now evening."); // Print evening notification
            }
        }
        else
        {
            isMorning = false; isAfternoon = false; isEvening = false; isEndOfDay = true; // Set time of day to end of day
            timeRemaining = 0; // Stop timer at 0, instead of becoming negative value
            isTimerRunning = false; // Turn off timer
            if (endOfDayTransitionComplete == false) // End of day transition block, which will run once at the end of the day
            {
                // Add additional evening to end of day transition logic here as needed
                endOfDayTransitionComplete = true; // Set transition status to complete
                timeOfDayText.text = ("End of Day"); // Display time of day text on screen
                timeOfDayText.color = Color.red; // Set color of text to red
                Debug.Log("The day is now over."); // Print end of day notification
                EndDay();
            }
        }
        DisplayTime(timeRemaining); // Display time remaining
    }

    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1; // Add a second to the time value vefore calculations of minutes and seconds, to account for final second of countdown
        float minutes = Mathf.FloorToInt(timeRemaining / 60); // Calculate remaining minutes
        float seconds = Mathf.FloorToInt(timeRemaining % 60); // Calculate remaining seconds
        timeRemainingText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // The value on the left determines which values to use (minutes, seconds) and the right is how it's formatted
    }

    private void EndDay()
    {
        // Check whether player made enough money to pay landlord
        // Display whether or not they won
        // Button for return to main menu or quit
    }


}