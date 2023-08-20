using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class UIMenu : MonoBehaviour
{ 
    public TMP_InputField playerInfo;
    private string playerName;

    // Start is called before the first frame update
    void Start()
    {
        playerName = GameManager.Instance.PlayerName;

        if (playerName == null)
        {

        }
        else
        {
            playerInfo.text = GameManager.Instance.PlayerName;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void PlayerInfoDeselect()
    {
        GameManager.Instance.PlayerName = playerInfo.text;
    }
    public void EasyClicked()
    {
        GameManager.Instance.Difficulty = 0.1f;
    }

    public void MediumClicked()
    {
        GameManager.Instance.Difficulty = 0.15f;
    }

    public void HardClicked()
    {
        GameManager.Instance.Difficulty = 0.2f;
    }

    public void SaveClicked()
    {
        GameManager.Instance.SaveAll();
    }

    public void PlayGameClicked()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        GameManager.Instance.SaveAll();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

}
