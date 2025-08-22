using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUi : MonoBehaviour
{

    public HighscoreManager highscoreManager;
    public GameObject HighscoreEntries;
    public GameObject HighscoreEntryPrefab;

    private void Start()
    {
        
    }

    private void ShowHighscores()
    {
        for (var i = HighscoreEntries.transform.childCount - 1; i >= 0; i--)
        {
            var child = HighscoreEntries.transform.GetChild(i);
            Destroy(child.gameObject);
        }
        var highscores = highscoreManager.List();
        foreach (var highscore in highscores)
        {
            var highscoreEntry = Instantiate(HighscoreEntryPrefab, HighscoreEntries.transform);
            var textMeshPro = highscoreEntry.GetComponent<TextMeshProUGUI>();
            textMeshPro.text = $"{highscore.Score} - {highscore.Name}";
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    
    public void CloseGame()
    {
        if (Application.isEditor)
        {
            EditorApplication.isPlaying = false;
        } else
        {
            Application.Quit();
        }
    

        
    }

    


}
