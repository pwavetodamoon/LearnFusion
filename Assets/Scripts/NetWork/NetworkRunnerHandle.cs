using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;
using deVoid.Utils;
using Fusion.Sockets;
using System;
using System.Threading.Tasks;
using System.Linq;

public class NetworkRunnerHandle : MonoBehaviour
{
    public NetworkRunner runner;

    private NetworkRunner _runner;

    private void Awake()
    {
        NetworkRunner networkRunnerInScene = FindObjectOfType<NetworkRunner>();
        if (networkRunnerInScene != null)
        {
            _runner = networkRunnerInScene;
            Debug.Log("networkRunnerInScene finded");
        }
    }
    private void Start()
    {
        if (_runner == null)
        {
            // Create the Fusion runner and let it know that we will be providing user input
            _runner = Instantiate(runner);
            _runner.name = "Network Runner";
            if (SceneManager.GetActiveScene().name != "MenuLobby")
            {
                var clienTask = InitNetworkRunner(_runner, GameMode.AutoHostOrClient, NetAddress.Any(), "TestSession", SceneManager.GetActiveScene().buildIndex, null);
            }
            Debug.Log("SV NetworkRunner Started");
        }
        else
        {
            Debug.Log("SV NetworkRunner not null");
        }
    }
    INetworkSceneManager GetSceneManager(NetworkRunner runner)
    {
        var SceneManagement = runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>().FirstOrDefault();
        if (SceneManagement == null)
        {
            SceneManagement = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }
        return SceneManagement;
    }
    private async Task InitNetworkRunner(NetworkRunner runner, GameMode mode, NetAddress netAddress, string sessionname, SceneRef sceneRef, Action<NetworkRunner> initlized)
    {
        var sceneManagement = GetSceneManager(runner);
        _runner.ProvideInput = true;
        Debug.Log($"Initializing NetworkRunner with mode: {mode}");
        // Start or join (depends on gamemode) a session with a specific name
        var task = await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            Address = netAddress,
            SessionName = sessionname,
            CustomLobbyName = "OurLobbyId",
            Scene = sceneRef,
            Initialized = initlized,
            SceneManager = sceneManagement
        });
        Signals.Get<ShowMovementSignal>().Dispatch();
        Signals.Get<ShowUIChat>().Dispatch();

    }
    public void OnJoinLobby()
    {
        var clientTask = JoinLobby();

    }
    private async Task JoinLobby()
    {
        Debug.Log("JoinLobby Started");

        string IdLobby = "OurLobbyId";

        var taskResult = await _runner.JoinSessionLobby(SessionLobby.Custom, IdLobby);

        if (taskResult.Ok)
        {
            Debug.Log("JoinLobby Ok");
            Signals.Get<ShowUISessionListPanel>().Dispatch();
        }
        else
        {
            Debug.LogError($"Unalbe to join lobby {IdLobby}");
        }
    }

    public void CreateGame(string sessionName, string sceneName)
    {
        var clienTask = InitNetworkRunner(_runner, GameMode.Host, NetAddress.Any(), sessionName, SceneUtility.GetBuildIndexByScenePath($"Scenes/{sceneName}"), null);
    }

    public void JoinGame(SessionInfo sessionInfo)
    {
        var clienTask = InitNetworkRunner(_runner, GameMode.Client, NetAddress.Any(), sessionInfo.Name, SceneManager.GetActiveScene().buildIndex, null);
    }
}