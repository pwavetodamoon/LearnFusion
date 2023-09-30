using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerNetwork : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkPrefabRef[] _networkPlayer;
    [SerializeField] private List<Transform> _spawnPosList;
    UISessionListPanel uISessionListPanel;

    private CollectNetworkInputData _collectNetworkInputData;
    private NetworkObject networkPlayerObject;
    private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();
    private void Awake()
    {
        uISessionListPanel = FindObjectOfType<UISessionListPanel>(true);

    }
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
            NetworkPrefabRef selectedCharacterRandom = _networkPlayer[0];
            networkPlayerObject = runner.Spawn
            (selectedCharacterRandom, RandomSpawnpositions().position, Quaternion.identity, player);
            runner.SetPlayerObject(player, networkPlayerObject);
        }
        _spawnedCharacters.Add(player, networkPlayerObject);
        Debug.Log("Player Now: " + _spawnedCharacters.Count);
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
    {
        if (uISessionListPanel == null) return;

        if (sessionList.Count == 0)
        {
            uISessionListPanel.OnNoSessionFound();
        }
        else
        {
            uISessionListPanel.ClearListUiItem(); // xoa item list cu
            foreach (SessionInfo sessionInfo in sessionList)
            {
                uISessionListPanel.AddListUiItem(sessionInfo);
            }
        }
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    { }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    { }
}