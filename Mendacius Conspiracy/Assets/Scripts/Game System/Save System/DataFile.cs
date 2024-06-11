using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class DataFile
{
    private string data_directory = "";
    private string file_name = "";
    private bool encryption_active = false;
    private readonly string encryption_code = "code";
    public DataFile(string data_directory, string file_name, bool encryption_active)
    {
        this.data_directory = data_directory;
        this.file_name = file_name;
        this.encryption_active = encryption_active;
    }
    public GameData Load()
    {
        string full_path = Path.Combine(data_directory, file_name); // to account for different OS's having different path seperators
        GameData loaded_data = null;
        if (File.Exists(full_path))
        {
            try
            {
                // Load serialized data from the file
                string data_to_load = "";
                using (FileStream stream = new FileStream(full_path, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        data_to_load = reader.ReadToEnd();
                    }
                }
                // Decrypt the data
                if (encryption_active)
                {
                    data_to_load = EncryptDecrypt(data_to_load);
                }
                // Take or deserialized data from json back to C# object
                loaded_data = JsonUtility.FromJson<GameData>(data_to_load);
            }
            catch(Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file : " + full_path + "\n" + e);
            }
        }
        else
        {
            Debug.LogWarning("File does not exist: " + full_path);
        }
        return loaded_data;
    }
    public void Save(GameData data)
    {
        string full_path = Path.Combine(data_directory, file_name); // to account for different OS's having different path seperators
        try
        {
            Debug.Log("Creating directory path: " + Path.GetDirectoryName(full_path));
            Directory.CreateDirectory(Path.GetDirectoryName(full_path)); // create directory path that will be written if it doesn't already exist
            string data_to_store = JsonUtility.ToJson(data, true); // serialize c# game data to json

            //Encrypt the data
            if(encryption_active)
            {
                data_to_store = EncryptDecrypt(data_to_store);
            }
            Debug.Log("Saving data to: " + full_path);
            using (FileStream stream = new FileStream(full_path, FileMode.Create)) // write the serialized data to the file
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(data_to_store);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file : " + full_path + "\n" + e);
        }
    }
    public void Delete()
    {
        string full_path = Path.Combine(data_directory, file_name); // to account for different OS's having different path seperators
        if (File.Exists(full_path))
        {
            File.Delete(full_path);
            Debug.Log("Game Data file deleted");
        }
        else
        {
            Debug.LogWarning("No game data found to delete");
        }
    }
    private string EncryptDecrypt(string data) // XOR Encryption implementation
    {
        string modified_data = "";
        for(int i  = 0; i < data.Length; i++)
        {
            modified_data += (char)(data[i] ^ encryption_code[i % encryption_code.Length]);
        }
        return modified_data;
    }
}
