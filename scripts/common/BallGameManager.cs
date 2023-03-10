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
    [SerializeField] GameObject gameWinUI;
    [SerializeField] GameObject titleUI;

    [SerializeField] AudioSource gameMusic;

    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform playerStart;

    public enum State { 
        TITLE,
        START_GAME,
        PLAY_GAME,
        GAME_OVER,
        GAME_WIN
    }

    State state = State.TITLE;
    float stateTimer = 0;

    private void Start()
    {
    }

    private void Update()
    {
        switch (state){
            case State.TITLE:
                titleUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case State.START_GAME:
                titleUI.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Instantiate(playerPrefab, playerStart.position, playerStart.rotation);
                gameMusic.Play();
                state = State.PLAY_GAME;
                break;
            case State.PLAY_GAME:

                break;
            case State.GAME_OVER:
                stateTimer -= Time.deltaTime;
                if (stateTimer <= 0) {
                    gameOverUI.SetActive(false);
                    state = State.TITLE;
                }
                break;
            case State.GAME_WIN:
                stateTimer -= Time.deltaTime;
                if (stateTimer <= 0){
                    gameWinUI.SetActive(false);
                    state = State.TITLE;
                }
                break;
            default:

                break;
        }


    }

    public void SetHealth(int health) {
        healthMeter.value = Mathf.Clamp(health, 0, 100);
    }

    public void SetScore(int score)
    {
        scoreUI.text = score.ToString();
        if (score == 2000) {
            SetGameWin();
        }
    }

    public void SetGameOver() {
        gameOverUI.SetActive(true);
        gameMusic.Stop();
        state = State.GAME_OVER;
        stateTimer = 3;
        
    }

    public void SetGameWin()
    {
        gameWinUI.SetActive(true);
        gameMusic.Stop();
        state = State.GAME_WIN;
        stateTimer = 5;

    }

    public void OnStartGame() {
        state = State.START_GAME;
    }
}
