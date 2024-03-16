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
    private Button joinGameRoomButton;
    private Button logOutButton;

    private void Start()
    {
        lobbyUI = Instantiate(LobbyPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        informationalText = FindObjectsByType<TMP_Text>(sortMode: FindObjectsSortMode.None);
        gameRoomInputField = FindAnyObjectByType<TMP_InputField>();
        buttons = FindObjectsByType<Button>(sortMode: FindObjectsSortMode.None);

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
            else if (button.name == "Button_JoinRoom")
            {
                joinGameRoomButton = button;
            }
            else if (button.name == "Button_LogOut")
            {
                logOutButton = button;
            }
        }

        username.text = $"Welcome Back, {StateManager.Instance.userName}!";
        createGameRoomButton.onClick.AddListener(CreateNewGameRoom);
        joinGameRoomButton.onClick.AddListener(JoinGameRoom);
        logOutButton.onClick.AddListener(LogOut);
    }

    private void CreateNewGameRoom()
    {
        NetworkClientProcessing.SendMessageToServer(ClientToServerSignifiers.createGameRoom + "," + gameRoomInputField.text, TransportPipeline.ReliableAndInOrder);
    }

    private void JoinGameRoom()
    {
        NetworkClientProcessing.SendMessageToServer(ClientToServerSignifiers.joinExistingRoom + "," + gameRoomInputField.text, TransportPipeline.ReliableAndInOrder);
    }

    private void LogOut()
    {
        NetworkClientProcessing.SendMessageToServer(ClientToServerSignifiers.logout.ToString(), TransportPipeline.ReliableAndInOrder);
    }

    private void OnDestroy()
    {
        Destroy(lobbyUI);
    }
}
