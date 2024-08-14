using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkSpawner : NetworkBehaviour
{
    [SerializeField] private Transform spawnedPrefab;
    private Transform spawnedPrefabTransform;
    
    public override void OnNetworkSpawn()
    {
        if (!IsHost) return;
        
        spawnedPrefabTransform = Instantiate(spawnedPrefab);
        spawnedPrefabTransform.GetComponent<NetworkObject>().Spawn(true);
    }
}
