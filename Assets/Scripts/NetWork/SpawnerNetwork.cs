using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerNetwork : MonoBehaviour, INetworkRunnerCallbacks
{
    public NetworkPrefabRef playerPrefab;
    public Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            Vector3 spawnPosition = new Vector3((player.RawEncoded % runner.Config.Simulation.DefaultPlayers) * 3, 1, 0);
            NetworkObject networkPlayerObject = runner.Spawn(playerPrefab, spawnPosition, Quaternion.identity, player);
            // Theo dõi Avatars của người chơi để chúng ta có thể xóa nó khi chúng ngắt kết nối
            _spawnedCharacters.Add(player, networkPlayerObject);
            Debug.Log("Player Now: " + _spawnedCharacters.Count);
        }
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
        //collect input
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