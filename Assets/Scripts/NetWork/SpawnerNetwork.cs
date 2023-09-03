using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerNetwork : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkPrefabRef[] _networkPlayer;
    [SerializeField] private List<Transform> _spawnPosList;

    private CollectNetworkInputData _collectNetworkInputData;
    private NetworkObject networkPlayerObject;
    private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();

    private Transform RandomSpawnpositions()
    {
        int Position = UnityEngine.Random.Range(0, _spawnPosList.Count);
        Transform spawnPos = _spawnPosList[Position];
        Debug.Log($"spawnPos: {spawnPos.position}");
        return spawnPos;
    }
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {

        if (runner.IsServer)
        {
            int randomCharacter = UnityEngine.Random.Range(0, _networkPlayer.Count());
            NetworkPrefabRef selectedCharacterRandom = _networkPlayer[randomCharacter];
            networkPlayerObject = runner.Spawn(selectedCharacterRandom, RandomSpawnpositions().position, Quaternion.identity, player);
        }
        _spawnedCharacters.Add(player, networkPlayerObject);
        Debug.Log("Player Now: " + _spawnedCharacters.Count);

        // Theo dõi Avatars của người chơi để chúng ta có thể xóa nó khi chúng ngắt kết nối

    }


    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
        {
            runner.Despawn(networkObject);
            _spawnedCharacters.Remove(player);
            Debug.Log("On Remove Player");
            Debug.Log("Despawn");
        }
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        if (_collectNetworkInputData == null && NetworkPlayer.local != null)
        {
            _collectNetworkInputData = NetworkPlayer.local.GetComponent<CollectNetworkInputData>();
        }
        if (_collectNetworkInputData != null)
        {
            input.Set(_collectNetworkInputData.GetNetworkInputData());
        }
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log($"Connected to Sever1");
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        Debug.Log($"On Disconnected From Server1");
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    { }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    { }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    { }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    { }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    { }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    { }

    public void OnSceneLoadDone(NetworkRunner runner)
    { }

    public void OnSceneLoadStart(NetworkRunner runner)
    { }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    { }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    { }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    { }
}