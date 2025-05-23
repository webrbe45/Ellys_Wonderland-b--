using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static bool isGameOver;
    public GameObject gameOverScreen;
    public GameObject pauseMenuScreen;
    public GameObject optionScreen;

    public static Vector2 lastCheckPointPos = new Vector2(-3, 0);
    public static int numberOfCoins;
    public TextMeshProUGUI coinsText;

    public GameObject[] playerPrefabs;
    int characterIndex;

    private PlayerManager1 playerScript;

    private void Awake()
    {
        characterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        numberOfCoins = PlayerPrefs.GetInt("NumberOfCoins", 0);
        isGameOver = false;

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            playerScript = playerObj.GetComponent<PlayerManager1>();
        }
    }

    private void Update()
    {
        if (playerScript != null && playerScript.isDead && !isGameOver)
        {
            isGameOver = true;
            gameOverScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ReplayLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenuScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenuScreen.SetActive(false);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void OpenOptions()
    {
        optionScreen.SetActive(true);

        if (gameOverScreen.activeSelf)
        {
            gameOverScreen.SetActive(false);
        }
    }

    public void CloseOptions()
    {
        optionScreen.SetActive(false);
    }
}
