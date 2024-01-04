using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using StarterAssets;
using System.Collections;

public class GameManager : MonoBehaviour, IDataPersistence
{
    public float timeRemaining = 0; // Time remaining in day
    public float timeInDay = 900; // Time remaining in day
    public bool isMorning; // Variable to keep track of whether it's morning
    public bool isAfternoon; // Variable to keep track of whether it's afternoon
    public bool isEvening; // Variable to keep track of whether it's evening
    public bool isEndOfDay; // Variable to keep track of whether the day is over
    public TMP_Text timeRemainingText; // Time remaining to display as text
    public TMP_Text timeOfDayText; // Time of day to display as text
    public TMP_Text currentDayText;
    public TMP_Text playerCurrencyText;
    public TMP_Text landlordPaymentText;
    public TMP_Text winOrLossText;
    public TMP_Text endOfDayText;
    public TMP_Text dayEndPlayerCurrencyText;
    public TMP_Text dayEndLandlordPaymentText;

    public int playerCurrency = 0;
    public int landlordPayment = 1400;
    public int potionValue;

    public int endOfDayCurrency;
    public int endOfDayLandlordPayment;
    public int endOfDayMorality;

    private int currentDay = 0;

    public FirstPersonController controller; // First person controller game object
    public GameObject playerCapsule; // Player capsule collider
    public GameObject dropSpawnLocation;

    public GameObject potionCraftingCanvas;
    public GameObject orderCanvas;
    public GameObject doorToMaze;
    public GameObject endOfDayCanvas;
    public GameObject winLossCanvas;
    public GameObject pauseMenuCanvas;

    public bool isTimerRunning = false;
    public bool morningTransitionComplete = false; // Keep track of transition from beginning of new day to morning
    public bool afternoonTransitionComplete = false; // Keep track of transition from morning to afternoon
    public bool eveningTransitionComplete = false; // Keep track of transition from afternoon to evening
    public bool endOfDayTransitionComplete = false; // Keep track of transition from evening to end of day

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
        isTimerRunning = false;
        Cursor.lockState = CursorLockMode.Confined; // Lock cursor in one place
        Cursor.visible = true; // Hide cursor
        playerCapsule.SetActive(false); // Deactivate player capsule
        playerCurrencyText.text = ("Player Currency: $" + playerCurrency);
        landlordPaymentText.text = ("Landlord Payment: $" + landlordPayment);
        currentDayText.text = ("Day " + currentDay);

    }

    private void Update()
    {
        if (isTimerRunning == true) // Calculate time if timer is still running
        {
            timeRemaining -= Time.deltaTime; // Decrement time using deltaTime
            //Debug.Log(timeRemaining); // Print remaining time
            TimerUpdate(); // Update timer every frame
        } 
        else
        {
            TimerUpdate();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(0)) // Do not allow pause menu toggle when in main mainu
            TogglePauseMenuCanvas(); // Toggle pause menu
        } 
    }

    public void LoadData(GameData data)
    {
        Debug.Log("Loaded player currency: " + data.playerCurrency);
        Debug.Log("Loaded landlord payment: " + data.landlordPayment);
        Debug.Log("Loaded current day: " + data.currentDay);
        Debug.Log("Loaded player morality: " + data.playerMorality);
        GameManager.Instance.playerCurrency = data.playerCurrency;
        GameManager.Instance.landlordPayment = data.landlordPayment;
        GameManager.Instance.currentDay = data.currentDay;
        dayEndPlayerCurrencyText.text = ("Player Currency: $" + playerCurrency);
        dayEndLandlordPaymentText.text = ("Landlord Payment: $" + landlordPayment);
        MoralitySystem.Instance.moralityCounter = data.playerMorality;
        MoralitySystem.Instance.UpdateMoralityUI();
        //InventoryController.Instance.inventoryGrid = data.playerInventory;
    }

    public void SaveData(ref GameData data)
    {
        data.playerCurrency = this.endOfDayCurrency;
        data.landlordPayment = this.endOfDayLandlordPayment;
        data.currentDay = this.currentDay;
        data.playerMorality = this.endOfDayMorality;
        //data.playerInventory = InventoryController.Instance.inventoryGrid;
    }

    public void StartGameTimer()
    {
        //Debug.Log(isTimerRunning);
        isTimerRunning = true; // Set the timer to run
        //Debug.Log(isTimerRunning);

    }

    public void PauseGameTimer()
    {
        isTimerRunning = false; // Set the timer to pause
    }

    public void ResetGameTimer()
    {
        timeRemaining = timeInDay;
    }

    public void ExitGame()
    {
        Application.Quit(); // Exit game to desktop
    }

    public void TogglePauseMenuCanvas()
    {
        if (pauseMenuCanvas.gameObject.activeSelf) // If on, toggle off
        {
            isTimerRunning = true; // Resume timer
            pauseMenuCanvas.SetActive(false); // Deactivate pause menu canvas

            if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(0) ||
                SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(2))
            {
                SetPlayerCapsuleActive(); // Activate player capsule
                Cursor.lockState = CursorLockMode.None; // Unlock cursor, confine to game screen
                Cursor.visible = false; // Display cursor
            }
            
            var foundAIObjects = FindObjectsOfType<MazeAIController>();
            foreach (MazeAIController mazeAI in foundAIObjects)
            {
                Debug.Log(mazeAI);
                mazeAI.ResumeMovement();
            }
        }
        else
        {
            isTimerRunning = false; // Pause timer
            pauseMenuCanvas.SetActive(true); // Activate pause menu canvas
            SetPlayerCapsuleInactive(); // Deactivate player capsule
            Cursor.lockState = CursorLockMode.Confined; // Unlock cursor, confine to game screen
            Cursor.visible = true; // Display cursor

            var foundAIObjects = FindObjectsOfType<MazeAIController>();
            foreach (MazeAIController mazeAI in foundAIObjects)
            {
                Debug.Log(mazeAI);
                mazeAI.StopMovement();
            }
        }
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
    public void ToggleOnOrderDisplay()
    {
        orderCanvas.SetActive(true);
    }
    public void ToggleOffOrderDisplay()
    {
        orderCanvas.SetActive(false);
    }

    public void AddCurrency(int currencyToAdd)
    {
        playerCurrency += currencyToAdd;
        playerCurrencyText.text = ("Player Currency: $" + playerCurrency);
    }

    public void SwitchSceneToMainMenu() // Use scene manager to switch to Main Menu
    {
        SceneManager.LoadScene(0); // Load scene through scene manager
        Cursor.lockState = CursorLockMode.Confined; // Unlock cursor, confine to game screen
        Cursor.visible = true; // Display cursor
    }

    public void SwitchSceneToPotionShopWithNewGame() // Use scene manager to switch to Main Menu
    {
        GameManager.Instance.winLossCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined; // Unlock cursor, confine to game screen
        Cursor.visible = true; // Display cursor
        GameManager.Instance.isTimerRunning = true;
        GameManager.Instance.timeRemaining = timeInDay;
        GameManager.Instance.currentDay = 1;
        GameManager.Instance.playerCurrency = 0;
        GameManager.Instance.landlordPayment = 1400;
        GameManager.Instance.endOfDayTransitionComplete = false;
        GameManager.Instance.morningTransitionComplete = false;
        GameManager.Instance.afternoonTransitionComplete = false;
        GameManager.Instance.eveningTransitionComplete = false;
        MoralitySystem.Instance.moralityCounter = 0;
        InventoryController.Instance.ClearInventoryGrid();
        DayUIUpdate();
        OrderSystem.Instance.GenerateOrderList();
        playerCurrencyText.text = ("Player Currency: $" + playerCurrency);
        landlordPaymentText.text = ("Landlord Payment: $" + landlordPayment);
        MoralitySystem.Instance.moralityCounter = 0;
        MoralitySystem.Instance.UpdateMoralityUI();
        SceneManager.LoadScene(2); // Load scene through scene manager

    }

    public void SwitchSceneToPotionShopWithNewDay() // Use scene manager to switch to Main Menu
    {
        GameManager.Instance.endOfDayCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined; // Unlock cursor, confine to game screen
        Cursor.visible = true; // Display cursor
        GameManager.Instance.isTimerRunning = true;
        GameManager.Instance.timeRemaining = timeInDay;
        DayUIUpdate();
        OrderSystem.Instance.GenerateOrderList();
        SceneManager.LoadScene(2); // Load scene through scene manager
    }

    public void SwitchSceneToPotionShopWithLoadGame() // Use scene manager to switch to Main Menu
    {
        DataPersistenceManager.Instance.LoadGame();
        Cursor.lockState = CursorLockMode.Confined; // Unlock cursor, confine to game screen
        Cursor.visible = true; // Display cursor
        GameManager.Instance.isTimerRunning = true;
        GameManager.Instance.timeRemaining = timeInDay;
        GameManager.Instance.playerCurrency = this.playerCurrency;
        GameManager.Instance.landlordPayment = this.landlordPayment;
        DayUIUpdate();
        AddCurrencyToPlayer(0);
        OrderSystem.Instance.GenerateOrderList();
        SceneManager.LoadScene(2); // Load scene through scene manager
    }

    public void SwitchSceneToBootstrapLevel() // Use scene manager to switch to Main Menu
    {
        SceneManager.LoadScene(1); // Load scene through scene manager
        Cursor.lockState = CursorLockMode.Confined; // Unlock cursor, confine to game screen
        Cursor.visible = true; // Display cursor
    }

    public void SwitchSceneToPotionLevel() // Use scene manager to switch to Potion Level from Maze Level
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(3))
        {
            LoadMazeLevel();
        }

        Cursor.lockState = CursorLockMode.Confined; // Lock cursor in one place
        Cursor.visible = true; // Hide cursor
        if (playerCapsule != null)
        {
            playerCapsule.SetActive(false); // Deactivate player capsule
        }
        OrderSystem.Instance.CheckForCompleteOrders();
        SceneManager.LoadScene(2); // Load scene through scene manager

    }

    public void SwitchSceneToMazeLevel() // Use scene manager to switch to Maze Level
    {
        SceneManager.LoadScene(3); // Use scene manager to load second scene from scene list (settings menu)
        LoadMazeLevel();
        controller._speed = 0; // Make it so player doesn't jut forward when entering maze
        controller._rotationVelocity = 0; // Make it so player doesn't rotate uncontrollably when entering maze
    }

    public void SwitchSceneToSettingsMenu() // Use scene manager to switch to Settings Menu
    {
        SceneManager.LoadScene(4); // Load scene through scene manager
        Cursor.lockState = CursorLockMode.Confined; // Unlock cursor, confine to game screen
        Cursor.visible = true; // Display cursor

    }

    public void BeginNewDay()
    {
        // To Do: Invoke scene transition
        GameManager.Instance.endOfDayTransitionComplete = false;
        GameManager.Instance.morningTransitionComplete = false;
        GameManager.Instance.afternoonTransitionComplete = false;
        GameManager.Instance.eveningTransitionComplete = false;
        GameManager.Instance.currentDay++;
        OrderSystem.Instance.GenerateOrderList();
        DataPersistenceManager.Instance.SaveGame();
        SwitchSceneToPotionShopWithNewDay();
    }

    public void CloseShopEarly()
    {
        GameManager.Instance.timeRemaining = 5.0f;
        TogglePauseMenuCanvas();
    }

    private void DisplayEndOfDayUI()
    {
        endOfDayText.gameObject.SetActive(true);
        endOfDayText.text = ("End of Day " + currentDay);
        dayEndPlayerCurrencyText.text = ("Player Currency: $" + playerCurrency);
        dayEndLandlordPaymentText.text = ("Landlord Payment: $" + landlordPayment);
        endOfDayCanvas.SetActive(true);
    }

    private void LoadMazeLevel() // Place player in correct spot when maze is loaded
    {
        Cursor.lockState = CursorLockMode.Locked; // Unlock cursor, confine to game screen
        Cursor.visible = false; // Display cursor

        SetPlayerCapsuleActive(); // Ensure the player capsule is active

        Vector3 spawnPoint = new Vector3(136f, 4f, -125.9f); // ************** EVENTUALLY NEED TO MAKE THIS INTO A SPAWN POINT AND USE MAZE SPAWN MANAGER *************************

        controller.MoveToPosition(spawnPoint); // Move player controller to player spawn point
    }

    private void SetPlayerCapsuleActive()
    {
        playerCapsule.SetActive(true); // Set player capsule active
    }

    private void SetPlayerCapsuleInactive()
    {
        playerCapsule.SetActive(false); // Set player capsule active
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
                //Debug.Log("The day is now over."); // Print end of day notification
                EndDay();
            }
        }
        DisplayTime(timeRemaining); // Display time remaining
    }

    public void DayUIUpdate()
    {
        GameManager.Instance.currentDayText.text = ("Day " + GameManager.Instance.currentDay);
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
        if (currentDay < 5)
        {
            this.endOfDayCurrency = this.playerCurrency;
            this.endOfDayLandlordPayment = this.landlordPayment;
            this.endOfDayMorality = MoralitySystem.Instance.moralityCounter;
            OrderSystem.Instance.GenerateOrderList();
            potionCraftingCanvas.SetActive(false);
            orderCanvas.SetActive(false);
            playerCapsule.SetActive(false);
            Cursor.lockState = CursorLockMode.Confined; // Unlock cursor, confine to game screen
            Cursor.visible = true; // Display cursor
            DisplayEndOfDayUI();
        } 
        else
        {
            EndWeek();
        }
    }

    private void EndWeek()
    {
        winOrLossText.gameObject.SetActive(true);

        winLossCanvas.SetActive(true);

        if (playerCurrency >= landlordPayment)
        {
            winOrLossText.text = ("You've won!");
        } else
        {
            winOrLossText.text = ("You've lost!");
        }
        DataPersistenceManager.Instance.NewGame();
    }

    public void AddCurrencyToPlayer(int amountToAdd)
    {
        playerCurrency += amountToAdd;
        playerCurrencyText.text = ("Player Currency: $" + playerCurrency);
    }

}