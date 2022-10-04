using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Spawner : MonoBehaviour
{
    [Header("Player And Enemys")]
    [SerializeField] private PlayerMove player;
    [SerializeField] private Enemy[] _enemies;

    [Header("Spawn Data")]
    [SerializeField] private int _baseLife;
    [SerializeField] private float SpawnRate;
    private float timeAfterStart;

    [Header("UI Objects")]
    [SerializeField] private GameObject StartUI;
    [SerializeField] private GameObject GameUI;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject canvasEnd;
    [SerializeField] private TextMeshProUGUI scoreTextEnd;
    [SerializeField] private TextMeshProUGUI resultTextEnd;
    [SerializeField] private TextMeshProUGUI moneyTextEnd;
    [SerializeField] private GameObject secondLifeBtn;

    //other data
    private float minWidth;
    private float maxWidth;
    private float maxHeight;

    private ulong points;
    private int hpMultiplayer = 1;

    private int killedEnemies = 0;

    private bool isGameEnd;

    private void Start()
    {
        AddPoints(0);
        player.spawner = this;

        Vector2 screenWidth = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        minWidth = screenWidth.x;
        screenWidth = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        maxWidth = screenWidth.x;
        maxHeight = screenWidth.y +.5f;
    }

    private void Update()
    {
        if (isGameEnd || !player.isGameStarted) return;
        timeAfterStart += Time.deltaTime;
        if (timeAfterStart >= SpawnRate)
        {
            timeAfterStart -= SpawnRate;
            CreateEnemy();
        }
    }

    private void CreateEnemy()
    {
        int indexEnemy = Random.Range(0, _enemies.Length);
        Enemy spawnedEnemy = Instantiate(_enemies[indexEnemy], new Vector2(Random.Range(minWidth, maxWidth), maxHeight), Quaternion.identity, transform);
        spawnedEnemy.SetHealth(_baseLife * (indexEnemy + 1) * hpMultiplayer * YandexGame.savesData.maxLevel);
        spawnedEnemy.SetSpawner(this);
    }

    public void AddPoints(ulong points)
    {
        this.points += points;
        scoreText.text = $"Score: {this.points}";
        hpMultiplayer = (int)(points / 1000) + 1;
    }

    public void StartGame()
    {
        StartUI.SetActive(false);
        GameUI.SetActive(true);

        player.isGameStarted = true;
    }

    public void LoseGame()
    {
        if(points> YandexGame.savesData.maxScore)
            YandexGame.savesData.maxScore = points;

        canvasEnd.SetActive(true);
        player.enabled = false;
        ulong money = (ulong)(points * .2f);
        scoreTextEnd.text = $"Очков: {points}";
        moneyTextEnd.text = $"Денег: {money}";
        resultTextEnd.text = "Поражение";
        YandexGame.savesData.money += money;
    }

    public void DestroyedEnemies()
    {
        killedEnemies++;
        if(killedEnemies >= 50)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        canvasEnd.SetActive(true);
        secondLifeBtn.SetActive(false);
        player.enabled = false;
        int money = (int)(points * .2f);
        scoreTextEnd.text = $"Очков: {points}";
        moneyTextEnd.text = $"Денег: {money}";
        resultTextEnd.text = "Победа!";
        YandexGame.savesData.money += (ulong)(points * .2f);
        if (points > YandexGame.savesData.maxScore)
            YandexGame.savesData.maxScore = points;

        YandexGame.savesData.maxLevel++;
    }
}
