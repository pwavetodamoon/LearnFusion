using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;
using deVoid.Utils;

public class NetworkRunnerHandle : MonoBehaviour
{
    // Khởi tạo Network Runner core của Fusion
    public NetworkRunner runner;

    private NetworkRunner _runner;

    private void OnGUI()
    {
        if (_runner == null)
        {
            if (GUI.Button(new Rect(800, 200, 400, 80), "Join"))
            {
                InitNetworkRunner(GameMode.AutoHostOrClient);
            }
        }
    }

    private async void InitNetworkRunner(GameMode mode)
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
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
        Signals.Get<ShowMovementSignal>().Dispatch();

    }
}