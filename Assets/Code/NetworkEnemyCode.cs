using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkEnemyCode : NetworkBehaviour
{
    [SerializeField] private GameObject networkBullet;
    private Vector3 enemySpawnPosition;
    private Vector3 enemyShotDirection;
    private int Health;
    [SerializeField] private float bulletInterval;
    private float bulletTimer;
    
    public override void OnNetworkSpawn()
    {
        transform.position = enemySpawnPosition;
        bulletTimer = bulletInterval;
    }
    
    public void spawninformation(Vector3 spawnposition)
    {
        enemySpawnPosition = spawnposition;
    }

    private void Update()
    {
        if(!IsHost) return;
        
        ShootBullet();
    }

    private void ShootBullet()
    {
        if (bulletTimer <= 0f)
        {
            InititateBulletServerRpc(transform.position, transform.up);
            
            
            bulletTimer = bulletInterval;
        }
        else
        {
            bulletTimer -= Time.deltaTime;
        }
    }

    [ServerRpc]
    private void InititateBulletServerRpc(Vector3 spawnposition, Vector3 bulletdirection)
    {
        InititateBulletClientRpc(spawnposition, bulletdirection);
    }
    
    [ClientRpc]
    private void InititateBulletClientRpc(Vector3 spawnposition, Vector3 bulletdirection)
    {
        if (!IsOwner)
            return;
        
        GameObject newBullet = Instantiate(networkBullet);
        newBullet.GetComponent<NetworkBullets>().spawninformation(spawnposition, bulletdirection, gameObject.tag);
        newBullet.GetComponent<NetworkObject>().Spawn(true);
    }
    
}
