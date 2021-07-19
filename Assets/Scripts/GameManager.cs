using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Snake snake;
    private bool snakeIsOutOfBounds = false;
    public static Action onApplePickup;
    public static Action<int> OnAddScore;
    [SerializeField] GameObject gameoverText;
    [SerializeField] GameObject pauseText;

    void Start()
    {
        GridManager.Instance.IntializeGrid();
        snake = FindObjectOfType<Snake>();
        snake.Intializesnake();
        GameState.OnGameOver += GameOver;
        GameState.OnPlaying += Playing;
        GameState.OnPaused += Paused;
        PlayerInput.AnyKeyPressed += AnyKeyPressed;
        PlayerInput.PausePressed += PausePressed;
        GameState.SetGameState(State.Playing);
    }

    private void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    private IEnumerator GameOverRoutine()
    {
        gameoverText.SetActive(true);
        while (GameState.GetGameState() == State.Gameover)
        {
            yield return new WaitForEndOfFrame();
        }
        gameoverText.SetActive(false);
    }

    private void PausePressed()
    {
        if (GameState.GetGameState() == State.Pause)
        {
            GameState.SetGameState(State.Playing);
        }
        else if (GameState.GetGameState() == State.Playing)
        {
            GameState.SetGameState(State.Pause);
        }
    }

    private void AnyKeyPressed()
    {
        if (GameState.GetGameState() == State.Gameover)
        {
            RestartGame();
        }
    }

    private void RestartGame()
    {

    }

    private void Paused()
    {
        StartCoroutine(PausedRoutine());
    }

    private IEnumerator PausedRoutine()
    {
        pauseText.SetActive(true);
        while (GameState.GetGameState() == State.Pause)
        {
            yield return new WaitForSeconds(0.2f);
        }
        pauseText.SetActive(false);
    }

    private void Playing()
    {
        StartCoroutine(PlayingRoutine());
    }

    private IEnumerator PlayingRoutine()
    {
        while (GameState.GetGameState() == State.Playing)
        {
            snake.Tick();
            if (snakeIsOutOfBounds)
            {
                GameState.SetGameState(State.Gameover);
            }
            else
            {
                checkIfSnakeOnApple();
                checkIfSnakeOnSnake();
            }
        }
        OnAddScore(1);
        yield return new WaitForSeconds(0.2f);
    }

    private void checkIfSnakeOnSnake()
    {
        Vector2Int snakeHeadGridPos = snake.GetHeadTile().GridPos;
        foreach (SnakeTile snakeTile in snake.SnakePieces.Skip(1))
        {
            if (snakeTile.GridPos == snakeHeadGridPos)
            {
                GameState.SetGameState(State.Gameover);
            }
        }
    }

    private void checkIfSnakeOnApple()
    {
        SnakeTile tileAtHead = snake.GetHeadTile();
        GridTile gridTile = GridManager.Instance.GetTileAt(tileAtHead.GridPos.x, tileAtHead.GridPos.y);
        if (gridTile.HasApple())
        {
            SnakeOnApple(gridTile);
            //add to score and notify apple pickup
            //Spawn a new apple
            SpawnApple();
            OnAddScore(10);
        }
    }

    private void SpawnApple()
    {
        GridTile tile = GridManager.Instance.GetRandomTile();
        tile.SetApple();
    }

    private void SnakeOnApple(GridTile gridTile)
    {
        gridTile.TakeApple();
        onApplePickup();
    }

    // Update is called once per frame
    void Update()
    {

    }

}
