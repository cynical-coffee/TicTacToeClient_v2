using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private GameObject LobbyPrefab;

    private GameObject lobbyUI;

    private TMP_Text[] informationalText;
    private TMP_Text username;

    private TMP_InputField gameRoomInputField;

    private Button[] buttons;
    private Button createGameRoomButton;
    private Button logOutButton;

    private const string createGameRoomSignifier = "3";
    private const string logoutSignifier = "4";

    private void Start()
    {
        lobbyUI = Instantiate(LobbyPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        informationalText = FindObjectsOfType<TMP_Text>();
        gameRoomInputField = FindObjectOfType<TMP_InputField>();
        buttons = FindObjectsOfType<Button>();

        foreach (TMP_Text text in informationalText)
        {
            if (text.name == "Text_WelcomeUsername")
            {
                username = text;
            }
        }

        foreach (Button button in buttons)
        {
            if (button.name == "Button_CreateRoom")
            {
                createGameRoomButton = button;
            }
            else if (button.name == "Button_LogOut")
            {
                logOutButton = button;
            }
        }

        username.text = $"Welcome Back, {StateManager.Instance.userName}!";
        createGameRoomButton.onClick.AddListener(CreateNewGameRoom);
        logOutButton.onClick.AddListener(LogOut);
    }

    private void CreateNewGameRoom()
    {
        NetworkClientProcessing.Instance.SendMessageToServer(createGameRoomSignifier + "," + gameRoomInputField.text);
    }

    private void LogOut()
    {
        NetworkClientProcessing.Instance.SendMessageToServer(logoutSignifier);
    }

    private void OnDestroy()
    {
        Destroy(lobbyUI);
    }
}
