using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    [Header("File Storage")]
    [SerializeField] private string file_name;
    [SerializeField] private bool encryption_active;

    private GameData game_data;
    private List<IDataManager> data_manager_object;
    private DataFile data_file;
    public static DataManager instance {  get; private set; }
    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("Found more data persistance in the scene. Destroy the newest one");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        string dataPath = Application.persistentDataPath;
        Debug.Log("Data Path: " + dataPath);

        data_file = new DataFile(Application.persistentDataPath, file_name, encryption_active);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }
    public void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("SceneLoaded called");
        data_manager_object = FindAllDataManagerObject();
        LoadGame();
    }
    public void NewGame()
    {
        Debug.Log("New game data");
        game_data = new GameData();
        game_data.Initialize();
    }
    public void LoadGame()
    {
        game_data = data_file.Load(); // load any saved data from file using data file

        if(game_data == null) // if data is not found, don't continue
        {
            Debug.Log("No game data was found. New game must be started to continue the game");
            return;
        }

        foreach(IDataManager data_manager_obj in data_manager_object) // push loaded data to all other script that need it
        {
            data_manager_obj.LoadData(game_data);
        }
    }
    public void SaveGame()
    {
        Debug.Log("Calling savegame");
        if(game_data == null)
        {
            Debug.LogWarning("No game data was found. New game must be started to continue the game");
            return;
        }

        foreach(IDataManager data_manager_obj in data_manager_object)
        {
            data_manager_obj.SaveData(game_data);
        }
        data_file.Save(game_data);
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private List<IDataManager> FindAllDataManagerObject()
    {
        IEnumerable<IDataManager> data_manager_object = FindObjectsOfType<MonoBehaviour>().OfType<IDataManager>();

        return new List<IDataManager>(data_manager_object);
    }
    public bool HasGameData()
    {
        return game_data != null;
    }
/*    private void Update()
    {
        if(game_data != null)
        {
            SaveGame();
        }
    }*/
}
