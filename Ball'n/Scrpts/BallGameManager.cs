using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallGameManager : Singleton<BallGameManager>
{
    [SerializeField] Slider healthMeter;
    [SerializeField] TMP_Text scoreUI;
    [SerializeField] GameObject gameOverUI;

    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform playerStart;

    private void Start()
    {
        Instantiate(playerPrefab, playerStart.position, playerStart.rotation);
    }

    public void SetHealth(int health) {
        healthMeter.value = Mathf.Clamp(health, 0, 100);
    }

    public void SetScore(int score)
    {
        scoreUI.text = score.ToString();
    }

    public void SetGameOver() {
        gameOverUI.SetActive(true);
    }
}