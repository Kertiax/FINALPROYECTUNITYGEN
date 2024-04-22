using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Threading.Tasks;
public class ActiveEnemy : NetworkBehaviour
{
    public GameObject enemy;
    public List<GameObject> players;
    public override void OnNetworkSpawn()
    {
        if(!IsServer)
        {
            enabled=false;
            return;
        }
        NetworkManager.Singleton.OnClientConnectedCallback += ClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += ClientDisconnected;
    }

    void ClientConnected(ulong u)
    {
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
    }
    async void ClientDisconnected(ulong u)
    {
        await Task.Yield();
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            enemy.SetActive(true);
        }
    }
}