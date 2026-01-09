using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public List<GameObject> targets;
    public Button restartButton;
    private float spawnRate = 1.0f;
    private int score;
    public bool isGameActive;
    public GameObject titleScreen;

    // Start is called before the first frame update
    void Start()
    {

    }
    
    IEnumerator SpawnTarget() {
    while (isGameActive) {
        // Menunggu selama waktu spawnRate (1 detik)
        yield return new WaitForSeconds(spawnRate);
        
        // Memilih target secara acak dari daftar (list)
        int index = Random.Range(0, targets.Count);
        
        // Membuat objek target tersebut di scene
        Instantiate(targets[index]);
    } 
}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver() 
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;

    }

    public void UpdateScore(int scoreToAdd) 
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void RestartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void StartGame(int difficulty) 
    {

        isGameActive = true; 
        StartCoroutine(SpawnTarget());
        score = 0;
        spawnRate /= difficulty;
        scoreText.text = "Score: " + score;
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);        
    }
}
