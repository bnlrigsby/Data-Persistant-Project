using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string PlayerName;
    public float Difficulty;
    public int HighScore;

    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(Difficulty == 0)
        {
            Difficulty = 0.1f;
        }

        LoadAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Serializable]
    class SaveData
    {
        public string PlayerName;
        public float Difficulty;
        public int HighScore;
    }

    public void SaveAll()
    {
        SaveData data = new SaveData();

        data.PlayerName = PlayerName;
        data.Difficulty = Difficulty;
        data.HighScore = HighScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadAll()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            PlayerName = data.PlayerName;
            Difficulty = data.Difficulty;
            HighScore = data.HighScore;
        }
    }
}
