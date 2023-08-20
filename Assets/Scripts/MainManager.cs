using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreValue;
    public GameObject GameOverText;
    public GameObject HighScoreText;

    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;
    private int highScore;
    private string playerName;
    private float difficulty;
    private Vector3 positionStack;
    private int[] pointCountArrayDifficulty;

    // Start is called before the first frame update
    void Start()
    {

        difficulty = GameManager.Instance.Difficulty;
        switch(difficulty)
        {
            case 0.1f:
                LineCount = 6;
                pointCountArrayDifficulty = new[] { 1, 1, 2, 2, 5, 5 };
                break;
            case 0.15f:
                LineCount = 7;
                pointCountArrayDifficulty = new[] { 1, 1, 2, 2, 5, 5, 5 };
                break;
            case 0.2f:
                LineCount = 10;
                pointCountArrayDifficulty = new[] { 1, 1, 1, 2, 2, 2, 5, 5, 5, 5 };
                break;

        }

        GameManager.Instance.LoadAll();

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        int[] pointCountArray = pointCountArrayDifficulty;

        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                switch (difficulty)
                {
                    case 0.1f:
                        positionStack = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                        break;
                    case 0.15f:
                        positionStack = new Vector3(-1.5f + step * x, 2.25f + i * 0.3f, 0);
                        break;
                    case 0.2f:
                        positionStack = new Vector3(-1.5f + step * x, 1.5f + i * 0.3f, 0);
                        break;

                }
                Vector3 position = positionStack;
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
        
        playerName = GameManager.Instance.PlayerName;
        highScore = GameManager.Instance.HighScore;
        BestScoreValue.text = $"{playerName} : {highScore}";
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    void CheckScrore(int score)
    {
        highScore = GameManager.Instance.HighScore;
        if(score > highScore)
        {
            playerName = GameManager.Instance.PlayerName;
            GameManager.Instance.HighScore = score;
            HighScoreText.SetActive(true);
            BestScoreValue.text = $"{playerName} : {highScore}";
            GameManager.Instance.SaveAll();
        }
    }

    public void MenuClicked()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void GameOver()
    {
        CheckScrore(m_Points);
        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}
