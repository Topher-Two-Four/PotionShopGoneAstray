using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using StarterAssets;
using System.Collections;

public class GameManager : MonoBehaviour, IDataPersistence
{
    [Header("General Settings:")]
    [Tooltip("The first person controller game object.")]
    [SerializeField] private FirstPersonController controller; // First person controller game object
    [Tooltip("The landlord payment amount.")]
    [SerializeField] private int landlordPayment = 1400;

    [Header("Time and Day Settings:")]
    [Tooltip("The total amount of time in a game day in seconds.")]
    [SerializeField] private float timeInDay = 900; // Time remaining in day
    [Tooltip("Is timer running or not running.")]
    [SerializeField] private bool isTimerRunning = false;

    [Header("Maze Settings:")]
    [Tooltip("The player capsule for the first person controller.")]
    [SerializeField] private GameObject playerCapsule; // Player capsule collider
    [Tooltip("The location where dropped items from the player will be dropped.")]
    [SerializeField] private GameObject dropSpawnLocation;
    [Tooltip("The player spawn point game object for Maze Level Alpha.")]
    [SerializeField] private GameObject alphaPlayerSpawn;
    [Tooltip("The player spawn point game object for Maze Level Bravo.")]
    [SerializeField] private GameObject bravoPlayerSpawn;
    [Tooltip("The player spawn point game object for Maze Level Charlie.")]
    [SerializeField] private GameObject charliePlayerSpawn;
    [Tooltip("The player spawn point game object for Maze Level Delta.")]
    [SerializeField] private GameObject deltaPlayerSpawn;

    [Header("Canvas Settings:")]
    [Tooltip("The canvas that holds the potion crafting UI elements.")]
    [SerializeField] private GameObject potionCraftingCanvas;
    [Tooltip("The canvas that holds the order system UI elements.")]
    [SerializeField] private GameObject orderCanvas;
    [Tooltip("The door from the potion shop to the maze.")]
    [SerializeField] private GameObject doorToMaze;
    [Tooltip("The canvas that holds the end of day screen UI elements.")]
    [SerializeField] private GameObject endOfDayCanvas;
    [Tooltip("The canvas that holds the win/loss screen UI elements.")]
    [SerializeField] private GameObject winLossCanvas;
    [Tooltip("The canvas that holds the pause menu screen UI elements.")]
    [SerializeField] private GameObject pauseMenuCanvas;

    [Header("Text-Related Settings:")]
    [Tooltip("The time remaining text for UI display.")]
    [SerializeField] private TMP_Text timeRemainingText; // Time remaining to display as text
    [Tooltip("The time of day text for UI display.")]
    [SerializeField] private TMP_Text timeOfDayText; // Time of day to display as text
    [Tooltip("The current time of day text for UI display.")]
    [SerializeField] private TMP_Text currentDayText;
    [Tooltip("The text showing player currency amount for UI display.")]
    [SerializeField] private TMP_Text playerCurrencyText;
    [Tooltip("The text showing landlord payment amount for UI display.")]
    [SerializeField] private TMP_Text landlordPaymentText;
    [Tooltip("The win/loss text for UI display.")]
    [SerializeField] private TMP_Text winOrLossText;
    [Tooltip("The end of day text for UI display.")]
    [SerializeField] private TMP_Text endOfDayText;
    [Tooltip("The text showing player currency amount for the end of day UI.")]
    [SerializeField] private TMP_Text dayEndPlayerCurrencyText;
    [Tooltip("The text showing landlord payment amount for the end of day UI.")]
    [SerializeField] private TMP_Text dayEndLandlordPaymentText;

    [HideInInspector] public float timeRemaining = 0; // Time remaining in day
    [HideInInspector] public bool isMorning; // Variable to keep track of whether it's morning
    [HideInInspector] public bool isAfternoon; // Variable to keep track of whether it's afternoon
    [HideInInspector] public bool isEvening; // Variable to keep track of whether it's evening
    [HideInInspector] public bool isEndOfDay; // Variable to keep track of whether the day is over

    private int playerCurrency = 0;
    private int endOfDayCurrency;
    private int endOfDayLandlordPayment;
    private int endOfDayMorality;
    private int currentDay = 0;
    private bool morningTransitionComplete = false; // Keep track of transition from beginning of new day to morning
    private bool afternoonTransitionComplete = false; // Keep track of transition from morning to afternoon
    private bool eveningTransitionComplete = false; // Keep track of transition from afternoon to evening
    private bool endOfDayTransitionComplete = false; // Keep track of transition from evening to end of day


    public static GameManager Instance { get; private set; } // Singleton logic

    // - GENERAL -

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
        ToggleCursorOn();
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

    /*public void OnLevelWasLoaded(int level)
    {
        if (GameObject.FindGameObjectWithTag("PlayerSpawnPoint") != null)
        {
            SpawnPlayerIntoMaze(level);
            controller.CallMove();
            //controller.StopMovement();
            //controller.ResumeMovement();
        }
    }
    */

    // - SAVE/LOAD GAME -

    public void SaveData(ref GameData data)
    {
        data.playerCurrency = this.endOfDayCurrency;
        data.landlordPayment = this.endOfDayLandlordPayment;
        data.currentDay = this.currentDay;
        data.playerMorality = this.endOfDayMorality;
        //data.playerInventory = InventoryController.Instance.inventoryGrid;
    }

    public void LoadData(GameData data)
    {
        Debug.Log("Loaded player currency: " + data.playerCurrency);
        Debug.Log("Loaded landlord payment: " + data.landlordPayment);
        Debug.Log("Loaded current day: " + data.currentDay);
        Debug.Log("Loaded player morality: " + data.playerMorality);
        Debug.Log("Loaded player inventory: " + data.playerInventory);
        GameManager.Instance.playerCurrency = data.playerCurrency;
        GameManager.Instance.landlordPayment = data.landlordPayment;
        GameManager.Instance.currentDay = data.currentDay;
        InventoryController.Instance.SetInventoryGrid(data.playerInventory);
        dayEndPlayerCurrencyText.text = ("Player Currency: $" + playerCurrency);
        dayEndLandlordPaymentText.text = ("Landlord Payment: $" + landlordPayment);
        MoralitySystem.Instance.SetMoralityCount(data.playerMorality);
        MoralitySystem.Instance.UpdateMoralityUI();
    }

    // - GAME TIMER -

    public void StartGameTimer()
    {
        isTimerRunning = true; // Set the timer to run

    }

    public void PauseGameTimer()
    {
        isTimerRunning = false; // Set the timer to pause
    }

    public void ResetGameTimer()
    {
        timeRemaining = timeInDay; // Reset time of day timer
    }

    public bool CheckIfTimerRunning()
    {
        return isTimerRunning;
    }

    private void StartNewDayTimer()
    {
        GameManager.Instance.isTimerRunning = true; // Run timer
        GameManager.Instance.timeRemaining = timeInDay; // Reset time of day
    }

    private void TimerUpdate() // Update timer and keep track of what time of day it is
    {
        if (timeRemaining > 600)
        {
            isMorning = true; isAfternoon = false; isEvening = false; isEndOfDay = false; // Set time of day to morning

            if (morningTransitionComplete == false) // Morning transition block, which will run once at the beginning of the morning
            {
                morningTransitionComplete = true; // Set transition status to complete
                timeOfDayText.text = ("Morning"); // Display time of day text on screen
                timeOfDayText.color = Color.white; // Set color of text to white
            }
        }
        else if (timeRemaining <= 600 && timeRemaining > 300)
        {
            isMorning = false; isAfternoon = true; isEvening = false; isEndOfDay = false; // Set time of day to afternoon
            if (afternoonTransitionComplete == false) // Afternoon transition block, which will run once at the beginning of the afternoon
            {
                afternoonTransitionComplete = true; // Set transition status to complete
                timeOfDayText.text = ("Afternoon"); // Display time of day text on screen
            }
        }
        else if (timeRemaining <= 300 && timeRemaining > 0)
        {
            isMorning = false; isAfternoon = false; isEvening = true; isEndOfDay = false; // Set time of day to evening
            if (eveningTransitionComplete == false) // Evening transition block, which will run once at the beginning of the evening
            {
                eveningTransitionComplete = true; // Set transition status to complete
                timeOfDayText.text = ("Evening"); // Display time of day text on screen
            }
        }
        else
        {
            isMorning = false; isAfternoon = false; isEvening = false; isEndOfDay = true; // Set time of day to end of day
            timeRemaining = 0; // Stop timer at 0, instead of becoming negative value
            isTimerRunning = false; // Turn off timer
            if (endOfDayTransitionComplete == false) // End of day transition block, which will run once at the end of the day
            {
                endOfDayTransitionComplete = true; // Set transition status to complete
                timeOfDayText.text = ("End of Day"); // Display time of day text on screen
                timeOfDayText.color = Color.red; // Set color of text to red
                EndDay();
            }
        }
        DisplayTime(timeRemaining); // Display time remaining
    }

    // - GAME MANAGEMENT -

    public void TogglePauseMenuCanvas()
    {
        if (pauseMenuCanvas.gameObject.activeSelf) // If on, toggle off
        {
            isTimerRunning = true; // Resume timer
            pauseMenuCanvas.SetActive(false); // Deactivate pause menu canvas

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(3) ||
                SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(4) ||
                SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(5))
            {
                SetPlayerCapsuleActive(); // Activate player capsule
                ToggleCursorOff(); // Lock and hide cursor
            }
            
            var foundAIObjects = FindObjectsOfType<MazeAIController>();
            foreach (MazeAIController mazeAI in foundAIObjects)
            {
                mazeAI.ResumeMovement(); // Resume AI movement when game is unpaused
            }
        }
        else
        {
            isTimerRunning = false; // Pause timer
            pauseMenuCanvas.SetActive(true); // Activate pause menu canvas
            SetPlayerCapsuleInactive(); // Deactivate player capsule
            ToggleCursorOn(); // Unlock and display cursor

            var foundAIObjects = FindObjectsOfType<MazeAIController>();
            foreach (MazeAIController mazeAI in foundAIObjects)
            {
                mazeAI.StopMovement(); // Pause AI movement when game is paused
            }
        }
    }

    public void BeginNewDay()
    {
        ResetDayTransitionStates();
        GameManager.Instance.currentDay++;
        OrderSystem.Instance.GenerateOrderList();
        DataPersistenceManager.Instance.SaveGame();
        SwitchSceneToPotionShopWithNewDay();
    }

    public void CloseShopEarly()
    {
        GameManager.Instance.timeRemaining = 0f;
        GameManager.Instance.pauseMenuCanvas.SetActive(false);
        EndDay();
    }

    private void EndDay()
    {
        if (currentDay < 5)
        {
            this.endOfDayCurrency = this.playerCurrency;
            this.endOfDayLandlordPayment = this.landlordPayment;
            this.endOfDayMorality = MoralitySystem.Instance.GetMoralityCount();
            if (potionCraftingCanvas != null)
            {
                potionCraftingCanvas.SetActive(false);
            }
            orderCanvas.SetActive(false);
            playerCapsule.SetActive(false);
            ToggleCursorOn(); // Unlock and display cursor
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
        }
        else
        {
            winOrLossText.text = ("You've lost!");
        }
        DataPersistenceManager.Instance.NewGame();
    }

    private void ResetDayTransitionStates()
    {
        GameManager.Instance.endOfDayTransitionComplete = false;
        GameManager.Instance.morningTransitionComplete = false;
        GameManager.Instance.afternoonTransitionComplete = false;
        GameManager.Instance.eveningTransitionComplete = false;
    }

    public void ExitGame()
    {
        Application.Quit(); // Exit game to desktop
    }

    // - SCENE MANAGEMENT -

    public void SwitchSceneToMainMenu() // Use scene manager to switch to Main Menu
    {
        SceneManager.LoadScene(0); // Load main menu scene
        ToggleCursorOn(); // Unlock and display cursor
    }

    public void SwitchSceneToSettingsMenu() // Use scene manager to switch to Settings Menu
    {
        SceneManager.LoadScene(1); // Load scene through scene manager
        ToggleCursorOn(); // Unlock and display cursor
    }

    public void SwitchSceneToPotionShopWithNewGame() // Use scene manager to switch to Main Menu
    {
        GameManager.Instance.winLossCanvas.SetActive(false); // Hide win/loss canvas
        ToggleCursorOn(); // Unlock and display cursor
        GameManager.Instance.currentDay = 1; // Reset to day 1
        StartNewDayTimer(); // Restart and resume timer at beginning of day
        GameManager.Instance.playerCurrency = 0;
        GameManager.Instance.landlordPayment = 1400;
        ResetDayTransitionStates();
        MoralitySystem.Instance.ResetMoralityCounter();
        InventoryController.Instance.ClearInventoryGrid();
        DayUIUpdate();
        OrderSystem.Instance.GenerateOrderList();
        playerCurrencyText.text = ("Player Currency: $" + playerCurrency);
        landlordPaymentText.text = ("Landlord Payment: $" + landlordPayment);
        MoralitySystem.Instance.ResetMoralityCounter();
        MoralitySystem.Instance.UpdateMoralityUI();
        InventoryController.Instance.ToggleInventoryCanvasOff();
        SceneManager.LoadScene(2);
    }

    public void SwitchSceneToPotionShopWithNewDay()
    {
        GameManager.Instance.endOfDayCanvas.SetActive(false);
        ToggleCursorOn(); // Unlock and display cursor
        StartNewDayTimer(); // Restart and resume timer at beginning of day
        DayUIUpdate(); // Update UI
        OrderSystem.Instance.GenerateOrderList(); // Generate new list of orders
        SceneManager.LoadScene(2); // Load scene through scene manager
    }

    public void SwitchSceneToPotionShopWithLoadGame()
    {
        DataPersistenceManager.Instance.LoadGame();
        ToggleCursorOn(); // Unlock and display cursor
        StartNewDayTimer(); // Restart and resume timer at beginning of day
        GameManager.Instance.playerCurrency = this.playerCurrency; // Set player currency from loaded game
        GameManager.Instance.landlordPayment = this.landlordPayment; // Set landlord payment amount from loaded game
        DayUIUpdate(); // Update UI
        AddCurrencyToPlayer(0); // Update currency
        OrderSystem.Instance.GenerateOrderList(); // Generate new list of orders
        InventoryController.Instance.ToggleInventoryCanvasOff(); // Toggle off inventory canvas (***Important for inventory functioning***)
        SceneManager.LoadScene(2); // Load scene through scene manager
    }

    public void SwitchSceneToPotionLevel() // Use scene manager to switch to Potion Level from Maze Level
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(3))
        {
            LoadMazeLevelAlpha();
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(4))
        {
            LoadMazeLevelBravo();
        }

        ToggleCursorOn();
        if (playerCapsule != null)
        {
            SetPlayerCapsuleInactive(); // Deactivate player capsule
        }
        OrderSystem.Instance.CheckForCompleteOrders(); // Check for any complete orders to update order UI
        CallLoadPotionShop(); // Call load potion shop function, which is staggered to allow it to be invoked if neccessary
    }

    public void CallLoadPotionShop()
    {
        SceneManager.LoadScene(2); // Load scene through scene manager
    }

    public void SwitchSceneToMazeLevel() // Use scene manager to switch to Maze Level
    {
        int randomSceneIndex = Random.Range(3, 5); // Choose random maze scene to load
        if (randomSceneIndex == 3)
        {
            LoadMazeLevelAlpha();
            SceneManager.LoadScene(3); // Use scene manager to load second scene from scene list (settings menu)
        }
        else
        {
            LoadMazeLevelBravo();
            SceneManager.LoadScene(4); // Use scene manager to load second scene from scene list (settings menu)
        }
    }

    private void LoadMazeLevelAlpha() // Place player in correct spot when maze is loaded
    {
        ToggleCursorOff(); // Lock and hide cursor

        SetPlayerCapsuleActive(); // Ensure the player capsule is active

        controller.MoveToPosition(alphaPlayerSpawn.transform.position);
        playerCapsule.transform.rotation = alphaPlayerSpawn.transform.rotation;

        controller.StopMovement(); // Attempt to control player movement
        controller.ResumeMovement();
    }

    private void LoadMazeLevelBravo() // Place player in correct spot when maze is loaded
    {
        ToggleCursorOff(); // Lock and hide cursor

        SetPlayerCapsuleActive(); // Ensure the player capsule is active
        controller.MoveToPosition(bravoPlayerSpawn.transform.position);
        playerCapsule.transform.rotation = bravoPlayerSpawn.transform.rotation;

        controller.StopMovement(); // Attempt to control player movement
        controller.ResumeMovement();
    }

    // - UI -

    public void ToggleOnPotionCraftingCanvas()
    {
            potionCraftingCanvas.SetActive(true); // Display potion crafting canvas
    }

    public void ToggleOffPotionCraftingCanvas()
    {
            potionCraftingCanvas.SetActive(false); // Hide potion crafting canvas
    }

    public void ToggleOnDoorToMaze()
    {
        doorToMaze.SetActive(true); // Display door to maze from potion shop
    }

    public void ToggleOffDoorToMaze()
    {
        doorToMaze.SetActive(false); // Hide door to maze from potion shop
    }

    public void ToggleOnOrderDisplay()
    {
        orderCanvas.SetActive(true); // Display order canvas
    }

    public void ToggleOffOrderDisplay()
    {
        orderCanvas.SetActive(false); // Hide order canvas
    }

    public GameObject GetDoorToMaze()
    {
        return doorToMaze;
    }

    public GameObject GetPauseMenuCanvas()
    {
        return pauseMenuCanvas;
    }

    private void DisplayEndOfDayUI()
    {
        endOfDayText.gameObject.SetActive(true);
        endOfDayText.text = ("End of Day " + currentDay);
        dayEndPlayerCurrencyText.text = ("Player Currency: $" + playerCurrency);
        dayEndLandlordPaymentText.text = ("Landlord Payment: $" + landlordPayment);
        endOfDayCanvas.SetActive(true);
    }

    private void DayUIUpdate()
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


    private void ToggleCursorOn()
    {
        Cursor.lockState = CursorLockMode.Confined; // Unlock cursor, confine to game screen
        Cursor.visible = true; // Display cursor
    }

    private void ToggleCursorOff()
    {
        Cursor.lockState = CursorLockMode.None; // Unlock cursor, confine to game screen
        Cursor.visible = false; // Display cursor
    }

    // - PLAYER -

    public GameObject GetPlayerCapsule()
    {
        return playerCapsule;
    }

    public GameObject GetDropSpawnLocation()
    {
        return dropSpawnLocation;
    }

    public void AddCurrencyToPlayer(int currencyToAdd)
    {
        playerCurrency += currencyToAdd; // Add money to player currency
        playerCurrencyText.text = ("Player Currency: $" + playerCurrency); // Update player currency UI display
    }

    private void SetPlayerCapsuleActive()
    {
        playerCapsule.SetActive(true); // Set player capsule active
    }

    private void SetPlayerCapsuleInactive()
    {
        playerCapsule.SetActive(false); // Set player capsule active
    }

}