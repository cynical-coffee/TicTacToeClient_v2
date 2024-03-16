using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameRoomManager : MonoBehaviour
{
    [SerializeField] private GameObject gameRoomPrefab;
    private GameObject gameRoomUI;

    private Button[] gameRoomButtons;
    private Button leaveRoomButton;

    private void Start()
    {
        gameRoomUI = Instantiate(gameRoomPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        gameRoomButtons = FindObjectsByType<Button>(findObjectsInactive: FindObjectsInactive.Include,
            sortMode: FindObjectsSortMode.None);

        foreach (Button button in gameRoomButtons)
        {
            if (button.name == "Button_LeaveRoom")
            {
                leaveRoomButton = button;
            }
        }

        leaveRoomButton.onClick.AddListener(LeaveGameRoom);
    }

    private void LeaveGameRoom()
    {
        
    }
}
