using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    
    private GameData gameData;
   
    private List<IDataPersistence> dataPersistenceObjects;

    private FileDataHandler dataHandler;

    public static DataPersistenceManager Instance { get; private set; }

    public void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene.");
        }
        Instance = this;
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        // Load saved data using data handler
        this.gameData = dataHandler.Load();

        // If no data to load, initialize to a new game
        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }

        // Push the Loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }

        Debug.Log("Loaded player currency: " + gameData.playerCurrency);
        Debug.Log("Loaded landlord payment: " + gameData.landlordPayment);
        Debug.Log("Loaded current day: " + gameData.currentDay);
        Debug.Log("Loaded player morality: " + gameData.playerMorality);
        //Debug.Log("Loaded player inventory: " + gameData.playerInventory);
}

    public void SaveGame()
    {
        // pass the data to other scripts so they can update it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        Debug.Log("Saved player currency: " + gameData.playerCurrency);
        Debug.Log("Saved landlord payment: " + gameData.landlordPayment);
        Debug.Log("Saved current day: " + gameData.currentDay);
        Debug.Log("Saved player morality: " + gameData.playerMorality);
        //Debug.Log("Saved player inventory: " + gameData.playerInventory);

        // save that data to a file using the data handler 
        dataHandler.Save(gameData);
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
