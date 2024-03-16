using UnityEngine;

public static class NetworkClientProcessing
{
    #region Send and Receive Data Functions
    public static void ReceivedMessageFromServer(string msg, TransportPipeline pipeline)
    {
        Debug.Log("Network msg received =  " + msg + ", from pipeline = " + pipeline);

        string[] csv = msg.Split(',');
        int signifier = int.Parse(csv[0]);

        if (signifier == ServerToClientSignifiers.successfulLogin)
        {
            SystemMessages.LoginSuccessful(StateManager.Instance.userName);
            StateManager.Instance.UpdateGameState(StateManager.GameState.LOBBY);
        }
        else if (signifier == ServerToClientSignifiers.createdGameRoom)
        {
            StateManager.Instance.UpdateGameState(StateManager.GameState.GAMEROOM);
        }
        else if (signifier == ServerToClientSignifiers.successfulLogout)
        {
            StateManager.Instance.UpdateGameState(StateManager.GameState.LOGINPAGE);
        }
        else if (signifier == ServerToClientSignifiers.failedLogin)
        {
            Debug.Log(SystemMessages.loginFailed);
        }
        else if (signifier == ServerToClientSignifiers.failedToCreateRoom)
        {
            Debug.Log(SystemMessages.createRoomFailed);
        }
        else if (signifier == ServerToClientSignifiers.usernameTaken)
        {
            Debug.Log(SystemMessages.createUsernameFailed);
        }

        //gameLogic.DoSomething();
    }

    public static void SendMessageToServer(string msg, TransportPipeline pipeline)
    {
        networkClient.SendMessageToServer(msg, pipeline);
    }

    #endregion

    #region Connection Related Functions and Events
    public static void ConnectionEvent()
    {
        Debug.Log("Network Connection Event!");
    }
    public static void DisconnectionEvent()
    {
        Debug.Log("Network Disconnection Event!");
    }
    public static bool IsConnectedToServer()
    {
        return networkClient.IsConnected();
    }
    public static void ConnectToServer()
    {
        networkClient.Connect();
    }
    public static void DisconnectFromServer()
    {
        networkClient.Disconnect();
    }

    #endregion

    #region Setup
    static NetworkClient networkClient;
    //static GameLogic gameLogic;

    public static void SetNetworkedClient(NetworkClient NetworkClient)
    {
        networkClient = NetworkClient;
    }
    public static NetworkClient GetNetworkedClient()
    {
        return networkClient;
    }
    //static public void SetGameLogic(GameLogic GameLogic)
    //{
    //    gameLogic = GameLogic;
    //}

    #endregion

}

#region Protocol Signifiers
public static class ClientToServerSignifiers
{
    public const int newAccount = 0;
    public const int returningAccount = 1;
    public const int createGameRoom= 3;
    public const int logout = 4;
    public const int joinExistingRoom = 5;
    public const int leaveGameRoom = 6;
}

public static class ServerToClientSignifiers
{
    public const int successfulLogin = 2;
    public const int createdGameRoom = 3;
    public const int successfulLogout = 4;
    public const int failedLogin = 5;
    public const int failedToCreateRoom = 6;
    public const int usernameTaken = 7;
}
#endregion


#region System Messages
public static class SystemMessages
{
    public const string loginFailed = "Username/Password is incorrect or does not exist";
    public const string createRoomFailed = "Room Already Exists";
    public const string createUsernameFailed = "Username is taken.";

    public static void LoginSuccessful(string username)
    {
        Debug.Log($"Welcome Back, {username}!");
    }
}
#endregion