using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [Header("GameOver Related")]
    public static GameController instance;
    public GameObject GameOver;
    

    [Header("Quest Related")]
    private int enemiesAmount;
    [SerializeField] private TMP_Text totalEnemies;
    [SerializeField] private TMP_Text totalEnemiesKilled;
    [SerializeField] private GameObject QuestText;
    [SerializeField] private GameObject afterQuestText;
    public int enemiesKilled;
    public GameObject exit;

    [Header("Enemy Spawn")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject enemyPrefab;
    private int randomSpawn;

    void Start()
    {
        instance = this;
        enemiesAmount = 10;
        totalEnemies.text = enemiesAmount.ToString();
        int randomSpawn = Random.Range(0, spawnPoints.Length-1);
        Instantiate(enemyPrefab, spawnPoints[randomSpawn].position, transform.rotation);
        exit.SetActive(false);
    }

    void Update()
    {
        int randomSpawn = Random.Range(0, spawnPoints.Length);
        totalEnemiesKilled.text = enemiesKilled.ToString();

        if (enemiesKilled >= enemiesAmount)
        {
            QuestText.SetActive(false);
            afterQuestText.SetActive(true);
            exit.SetActive(true);

            var enemies = GameObject.FindGameObjectsWithTag("enemy");

            foreach (GameObject en in GameObject.FindGameObjectsWithTag("enemy"))
            {
                Destroy(en);
            }
        }

    }

    public void ShowGameOver()
    {
        GameOver.SetActive(true);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().volume = 0;
        GameObject.FindGameObjectWithTag("Owl").GetComponent<AudioSource>().volume = 0;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void spawnEnemy()
    {
        int randomSpawn = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[randomSpawn].position, transform.rotation);
        
    }
    public void enemyKilled()
    {
        if(enemiesKilled < enemiesAmount)
        {
            enemiesKilled++;
            spawnEnemy();
        } 
    }
}
