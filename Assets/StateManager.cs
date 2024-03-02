using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public enum GameState
    {
        LOGINPAGE = 0,
        LOBBY = 1,
    }

    public GameState state;

    private void Start()
    {
        state = GameState.LOGINPAGE;
    }
}
