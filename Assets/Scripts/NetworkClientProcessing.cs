using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkClientProcessing : MonoBehaviour
{
    public static NetworkClientProcessing Instance { get; private set; }

    private NetworkClient networkClient;

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

        networkClient = FindObjectOfType<NetworkClient>();
    }

    public void ProcessMessageFromServer(string msg)
    {
        Debug.Log("Msg received = " + msg);

        if (msg.StartsWith("2"))
        {
            StateManager.Instance.UpdateGameState(StateManager.GameState.LOBBY);
        }

        if (msg.StartsWith("3"))
        {
            StateManager.Instance.UpdateGameState(StateManager.GameState.GAMEROOM);
        }

        if (msg.StartsWith("4"))
        {
            StateManager.Instance.UpdateGameState(StateManager.GameState.LOGINPAGE);
        }
    }

    public void SendMessageToServer(string msg)
    {
        networkClient.SendMessageToServer(msg);
    }
}
