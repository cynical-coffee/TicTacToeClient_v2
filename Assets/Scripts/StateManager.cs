using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance { get; private set; }
    public enum GameState
    {
        LOGINPAGE = 0,
        LOBBY = 1,
        GAMEROOM = 2,
        GAMESTART = 3
    }
    public GameState state;
    public string userName { get; set; }
    public string opponentUsername { get; set; }

    [SerializeField] private GameObject accountManagerPrefab;
    [SerializeField] private GameObject lobbyManagerPrefab;
    [SerializeField] private GameObject gameRoomManagerPrefab;
    
    private GameObject accountManagerClone;
    private GameObject lobbyManagerClone;
    private GameObject gameRoomManagerClone;
    
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

    private void Start()
    {
        UpdateGameState(GameState.LOGINPAGE);
    }

    private void LoginState()
    {
        if (accountManagerClone != null)
        {
            Destroy(accountManagerClone);
            accountManagerClone = null;
        }

        if (lobbyManagerClone != null)
        {
            Destroy(lobbyManagerClone);
            lobbyManagerClone = null;
        }
        accountManagerClone = Instantiate(accountManagerPrefab);
    }

    private void LobbySate()
    {
        if (gameRoomManagerClone != null)
        {
            Destroy(gameRoomManagerClone);
            gameRoomManagerClone = null;
        }

        if (accountManagerClone != null)
        {
            Destroy(accountManagerClone);
            accountManagerClone = null;
        }
        lobbyManagerClone = Instantiate(lobbyManagerPrefab);
    }

    private void GameRoomState()
    {
        if (lobbyManagerClone != null)
        {
            Destroy(lobbyManagerClone);
            lobbyManagerClone = null;
        }
        gameRoomManagerClone = Instantiate(gameRoomManagerPrefab);
       // gameRoomManagerClone.GetComponent<GameRoomManager>().GameRoomSetUp();
    }

    private void GameStartState()
    {
        gameRoomManagerClone.GetComponent<GameRoomManager>().StartGame();
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.LOGINPAGE:
                LoginState();
                break;
            case GameState.LOBBY:
                LobbySate();
                break;
            case GameState.GAMEROOM:
                GameRoomState();
                break;
            case GameState.GAMESTART:
                GameStartState();
                break;
        }
    }
}
