using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private Text tapToStartText;
    [SerializeField] private Text tapToRestartText;
    [SerializeField] private Button options;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text bestScoreText;

    CharacterController character;
    ObjectPooler pooler;
    float score;
    float bestScore;

    private void Awake()
    {
        bestScore = PlayerPrefs.GetFloat("bestScoreSave");
        bestScoreText.text = "Best Score:" + bestScore.ToString("0");
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        pooler = GetComponent<ObjectPooler>();
    }

    void Start()
    {
        Time.timeScale = 0;
    }
    
    void Update()
    {
        GetScore();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pauseButton.gameObject.SetActive(true);
            tapToStartText.enabled = false;
            options.gameObject.SetActive(false);
            scoreText.enabled = true;
            Resume();
        }
        if (character.endGameControl)
        {
            gameOverText.enabled = true;
            pauseButton.gameObject.SetActive(false);
            tapToRestartText.enabled = true;
            pooler.StopAllCoroutines();
            SaveBestScore();
        }
        if (Input.GetKeyDown(KeyCode.Space) && character.endGameControl)
        {
            Restart();
        }

    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void GetScore()
    {
        score = character.transform.position.z;
        scoreText.text =score.ToString("0");
        
    }
    void SaveBestScore()
    {
        if (score > bestScore )
        {
            bestScore = score;
            PlayerPrefs.SetFloat("bestScoreSave", bestScore);
        }
    }
}
