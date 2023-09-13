using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;
using deVoid.Utils;

public class NetworkRunnerHandle : MonoBehaviour
{
    // Khởi tạo Network Runner core của Fusion
    public NetworkRunner runner;

    private NetworkRunner _runner;
    private void Awake()
    {
        InitNetworkRunner(GameMode.AutoHostOrClient, SceneManager.GetActiveScene().buildIndex);
    }
    private async void InitNetworkRunner(GameMode mode, SceneRef sceneRef)
    {
        // Create the Fusion runner and let it know that we will be providing user input
        _runner = Instantiate(runner);
        _runner.name = "Network Runner";
        _runner.ProvideInput = true;

        // Start or join (depends on gamemode) a session with a specific name
        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = "TestRoom",
            Scene = sceneRef,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
        Signals.Get<ShowMovementSignal>().Dispatch();

    }
}