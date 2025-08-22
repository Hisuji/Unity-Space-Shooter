using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMangager : MonoBehaviour
{

    public TextMeshProUGUI PointsText;
    public GameObject HealthContainer;
    public GameObject HealthUIitemPrefab;
    private int point;

    public GameObject GameOverUi;
    public GameObject HighscoreUi;
    public TMP_InputField NameInputField;
    public HighscoreManager highscoreManager;

    public void Awake()
    {
        GameOverUi.SetActive(false);
        HighscoreUi.SetActive(false);
    }

    public void GameOver()
    {
        GameOverUi.SetActive(true);

        var isNewHighscore = highscoreManager.IsNewHighscore(point);
        HighscoreUi.SetActive(isNewHighscore);

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddToHighscore()
    {
        var playerName = NameInputField.text;

        if (string.IsNullOrWhiteSpace(playerName))
        {
            return;
        }

        highscoreManager.Add(playerName, point);
        HighscoreUi.SetActive(false);

    }

    public void addPoints(int points)
    {
        point += points;
        PointsText.text = point.ToString();
    }

    public void setHealth(int health)

    {
        for (int i = HealthContainer.transform.childCount - 1; i >= 0; i--)
        {
            var child = HealthContainer.transform.GetChild(i);
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < health; i++)
        {
            var item = Instantiate(HealthUIitemPrefab, HealthContainer.transform);
        }

    }
        
    

}
