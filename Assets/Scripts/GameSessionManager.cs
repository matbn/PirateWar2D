using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSessionManager : MonoBehaviour
{
    public static GameSessionManager instance;
    [HideInInspector]
    public int playerScore = 0;
    public float sessionTime = 60;
    private float timeBetweenSpawns = 5;
    private float timeLastSpawn = 5;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] enemiesPrefabs;
    private bool sessionRunning = true;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        sessionTime = PlayerPrefs.GetFloat("GameSessionTime", 60);
        timeBetweenSpawns = PlayerPrefs.GetFloat("EnemySpawnTime", 5);
        timeLastSpawn = timeBetweenSpawns;
    }

    void Update()
    {
        if (sessionRunning)
        {
            sessionTime -= Time.deltaTime;
            if (sessionTime <= 0) EndSession();
            timeLastSpawn += Time.deltaTime;
            if (timeLastSpawn >= timeBetweenSpawns) SpawnEnemy();
        }
    }

    public void AddScore(int value)
    {
        playerScore+= value;
        OverlayUIManager.Instance.UpdateScore(playerScore);
    }

    private void SpawnEnemy()
    {
        int enemyTipe = Random.Range(0, enemiesPrefabs.Length);
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemiesPrefabs[enemyTipe], spawnPoints[spawnPointIndex].position, Quaternion.identity);
        timeLastSpawn = 0;
    }

    public void EndSession()
    {
        sessionRunning = false;
        OverlayUIManager.Instance.EndSessionUI();
    }
}
