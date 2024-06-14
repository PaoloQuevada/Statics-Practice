using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
	[Header("File Storage Config")]
	[SerializeField]
	private string fileName;

	private GameData gameData;

	private List<IDataPersistence> dataPersistenceObjects;

	private FileDataHandler dataHandler;

	public static DataPersistenceManager instance { get; private set; }

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogError("Found more than one Data Persistence Manager in the scene.");
		}
		instance = this;
	}

	private void Start()
	{
		Debug.Log("Game loaded.");
		dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
		dataPersistenceObjects = FindAllDataPersistenceObjects();
		LoadGame();
	}

	public void NewGame()
	{
		gameData = new GameData();
	}

	public void LoadGame()
	{
		gameData = dataHandler.Load();
		if (gameData == null)
		{
			Debug.Log("No data found, creating data.");
			NewGame();
		}
		foreach (IDataPersistence dataPersistenceObject in dataPersistenceObjects)
		{
			dataPersistenceObject.LoadData(gameData);
		}
		Debug.Log("Loaded win count = " + gameData.winCount);
	}

	public void SaveGame()
	{
		foreach (IDataPersistence dataPersistenceObject in dataPersistenceObjects)
		{
			Debug.Log("did a thing");
			dataPersistenceObject.SaveData(ref gameData);
		}
		dataHandler.Save(gameData);
		Debug.Log("Saved win count = " + gameData.winCount);
	}

	private void OnApplicationQuit()
	{
		Debug.Log("Game saved via quit.");
		SaveGame();
	}

	private void OnDestroy()
	{
		Debug.Log("Game saved scene destroy.");
		SaveGame();
	}

	private List<IDataPersistence> FindAllDataPersistenceObjects()
	{
		return new List<IDataPersistence>(Enumerable.OfType<IDataPersistence>(Object.FindObjectsOfType<MonoBehaviour>()));
	}
}
