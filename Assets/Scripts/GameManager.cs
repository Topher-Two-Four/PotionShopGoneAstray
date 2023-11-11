using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float timeRemaining = 90; // Time remaining in day
    public bool isTimerRunning; // Variable to keep track of if timer is still running
    public bool isMorning; // Variable to keep track of whether it's morning
    public bool isAfternoon; // Variable to keep track of whether it's afternoon
    public bool isEvening; // Variable to keep track of whether it's evening
    public bool isEndOfDay; // Variable to keep track of whether the day is over
    public TMP_Text timeRemainingText; // Time remaining to display as text
    public TMP_Text timeOfDayText; // Time of day to display as text

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
    }

    private void Update()
    {
        if (isTimerRunning == true) // Calculate time if timer is still running
        {
            timeRemaining -= Time.deltaTime; // Decrement time using deltaTime
            //Debug.Log(timeRemaining); // Print remaining time
            TimerUpdate(); // Update timer every frame
        }
    }

    public void SwitchSceneToMainMenu() // Use scene manager to switch to Main Menu
    {
        SceneManager.LoadScene(0); // Use scene manager to load first scene from scene list (main menu)
        Cursor.lockState = CursorLockMode.Confined; // Unlock cursor, confine to game screen
        Cursor.visible = true; // Display cursor
    }

    public void SwitchSceneToSettingsMenu() // Use scene manager to switch to Settings Menu
    {
        SceneManager.LoadScene(1); // Use scene manager to load second scene from scene list (settings menu)
        Cursor.lockState = CursorLockMode.Confined; // Unlock cursor, confine to game screen
        Cursor.visible = true; // Display cursor
    }

    public void SwitchSceneToPotionLevel() // Use scene manager to switch to Potion Level
    {

        // Need to make the timer start after leaving the main menu
        /*
        if (isTimerRunning == false) // Calculate time if timer is still running
        {
            timeRemaining = 90;
            StartTimer();
            Debug.Log("Timer started.");
        }
      */

        SceneManager.LoadScene(2); // Use scene manager to load third scene from scene list (potion shop level)
        Cursor.lockState = CursorLockMode.Confined; // Unlock cursor, confine to game screen
        Cursor.visible = true; // Display cursor
    }

    public void SwitchSceneToMazeLevel() // Use scene manager to switch to Maze Level
    {
        // Ranomize which maze scene is loaded
        SceneManager.LoadScene(3); // Use scene manager to load fourth scene from scene list (maze level)
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor in one place
        Cursor.visible = false; // Hide cursor
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void StartTimer()
    {
        isTimerRunning = true;
    }

    private void StopTimer()
    {
        isTimerRunning = false;
    }

    private void TimerUpdate() // Update timer and keep track of what time of day it is
    {
        if (timeRemaining > 60)
        {
            isMorning = true;
            isAfternoon = false;
            isEvening = false;
            isEndOfDay = false;
            if (morningTransitionComplete == false) // Morning transition block, which will run once at the beginning of the morning
            {
                // Add additional new day to morning transition logic here as needed
                morningTransitionComplete = true; // Set transition status to complete
                timeOfDayText.text = ("Morning"); // Display time of day text on screen
                timeOfDayText.color = Color.white; // Set color of text to white
                //Debug.Log("It's now morning."); // Print morning notification
            }
        }
        else if (timeRemaining <= 60 && timeRemaining > 30)
        {
            isMorning = false;
            isAfternoon = true;
            isEvening = false;
            isEndOfDay = false;
            if (afternoonTransitionComplete == false) // Afternoon transition block, which will run once at the beginning of the afternoon
            {
                // Add additional morning to afternoon transition logic here as needed
                afternoonTransitionComplete = true; // Set transition status to complete
                timeOfDayText.text = ("Afternoon"); // Display time of day text on screen
                //Debug.Log("It's now afternoon."); // Print afternoon notification
            }
        }
        else if (timeRemaining <= 30 && timeRemaining > 0)
        {
            isMorning = false;
            isAfternoon = false;
            isEvening = true;
            isEndOfDay = false;
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
            timeRemaining = 0; // Stop timer at 0, instead of becoming negative value
            isTimerRunning = false; // Turn off timer
            if (endOfDayTransitionComplete == false) // End of day transition block, which will run once at the end of the day
            {
                // Add additional evening to end of day transition logic here as needed
                endOfDayTransitionComplete = true; // Set transition status to complete
                timeOfDayText.text = ("End of Day"); // Display time of day text on screen
                timeOfDayText.color = Color.red; // Set color of text to red
                //Debug.Log("The day is now over."); // Print end of day notification
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

}