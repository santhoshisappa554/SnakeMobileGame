using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerInput : MonoBehaviour
{
    public enum Direction
    {
        up, right, down, left
    }
    public static Direction dir;
    public static Action PausePressed;
    public static Action AnyKeyPressed;
    private void Update()
    {
        switch (GameState.GetGameState())
        {
            case State.Playing:
                HandleInputPlaying();
                break;
            case State.Pause:
                HandleInputPaused();
                break;
            case State.Gameover:
                HandleInputGameOver();
                break;
            default:
                break;
        }
    }
    void HandleInputPlaying()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(v) > Mathf.Abs(h))
        {
            dir = v > 0 ? Direction.up : Direction.down;
        }
        else
        {
            dir = h > 0 ? Direction.right : Direction.left;
        }
        if (Input.GetButtonDown("Pause"))
        {
            PausePressed();
        }
    }
    void HandleInputPaused()
    {
        if (Input.GetButtonDown("Pause"))
        {
            PausePressed();
        }
    }
    void HandleInputGameOver()
    {
        if (Input.anyKeyDown)
        {
            AnyKeyPressed();
        }
    }
}
