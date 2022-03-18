using UnityEngine;
using UnityEngine.SceneManagement; // Allows scene manipulation (for Game Over)
using TMPro; //

// POINTERS
// * Singleton: A class that is (1) easily accesible from other scripts and (2) can only be instanced once at a time.
// * This script uses singleton logic.

public class GameManager : MonoBehaviour
{
    // Inspector field for the Game Over Text.
    [SerializeField] private GameObject gameOverText;

    // Inspector field for the Score Text.
    [SerializeField] private TMP_Text scoreText;

    // Creates a singleton GameManager instance and its getter.
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public bool isGameOver;
    private int score;


    // Awake is called before Start (the first frame update).
    void Awake()
    {
        // Singleton logic that ensures that only one instance exists at a time.
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    // Update is called every frame.
    private void Update()
    {
        // Restarts the game when (1) the Left Mouse Button is clicked and (2) the game is over.
        if(Input.GetMouseButtonDown(0) && isGameOver)
            RestartGame();
    }

    // GameOver displays the Game Over text and allows the player to restart.
    // It is declared as a public function so that it can be called from any other script.
    public void GameOver()
    {
        isGameOver = true;
        gameOverText.SetActive(true);
    }

    // RestartGame resets the scene and allows the game to start anew.
    private void RestartGame()
    {
        // Reloads the active scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // IncreaseScore increases the score and displays the new value.
    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
