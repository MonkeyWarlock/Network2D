using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkSpawner : NetworkBehaviour
{
    [SerializeField] private Transform spawnedPrefab;
    private Transform spawnedPrefabTransform;

    [SerializeField] private GameObject spawnEnemyPrefab;
    private GameObject spawnedEnemy;

    [SerializeField] private Vector3 spawnEnemyPosition;
    
    public override void OnNetworkSpawn()
    {
        if (!IsHost) return;
        
        spawnedPrefabTransform = Instantiate(spawnedPrefab);
        spawnedPrefabTransform.GetComponent<NetworkObject>().Spawn(true);

        spawnedEnemy = Instantiate(spawnEnemyPrefab);
        spawnedEnemy.GetComponent<NetworkEnemyCode>().spawninformation(spawnEnemyPosition);
        spawnedEnemy.GetComponent<NetworkObject>().Spawn(true);
    }
}
