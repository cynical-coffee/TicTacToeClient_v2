using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameRoomManager : MonoBehaviour
{
    [SerializeField] private GameObject gameRoomPrefab;
    private GameObject gameRoomUI;

    [SerializeField] private GameObject gamePlayPrefab;
    private GameObject gamePlayClone;

    private Button[] gameRoomButtons;
    private Button leaveRoomButton;

    private TMP_Text[] gameRoomTexts;
    private TMP_Text player1Username;
    private TMP_Text player2Username;

    private void Start()
    {
        gameRoomUI = Instantiate(gameRoomPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        gameRoomButtons = FindObjectsByType<Button>(sortMode: FindObjectsSortMode.None);

        gameRoomTexts = FindObjectsByType<TMP_Text>(sortMode: FindObjectsSortMode.None);

        foreach (Button button in gameRoomButtons)
        {
            if (button.name == "Button_LeaveRoom")
            {
                leaveRoomButton = button;
            }
        }

        foreach (TMP_Text text in gameRoomTexts)
        {
            if (text.name == "Text_Player1")
            {
                player1Username = text;
            }
            else if (text.name == "Text_Player2")
            {
                player2Username = text;
            }
        }

        leaveRoomButton.onClick.AddListener(LeaveGameRoom);
    }

    private void LeaveGameRoom()
    {
        NetworkClientProcessing.SendMessageToServer(ClientToServerSignifiers.leaveGameRoom.ToString(), TransportPipeline.ReliableAndInOrder);
    }

    private void OnDestroy()
    {
        Destroy(gameRoomUI);
    }

    public void GameRoomSetUp()
    {
        player1Username.text = $"Player 1: {StateManager.Instance.userName}";
    }

    public void StartGame()
    {
        player2Username.text = $"Player 2: {StateManager.Instance.opponentUsername}";
        gamePlayClone = Instantiate(gamePlayPrefab, gameRoomUI.transform);
    }
}