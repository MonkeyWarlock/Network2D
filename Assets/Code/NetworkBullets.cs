using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkBullets : NetworkBehaviour
{
    private float speed = 8f;
    private float lifetime = 4f;
    private Vector3 bulletSpawnPosition;
    private Vector3 bulletShotDirection;
    private String bulletOwner;
    public override void OnNetworkSpawn()
    {
        transform.position = bulletSpawnPosition;
        Vector2 bulletvelocity = new Vector2(bulletShotDirection.x, bulletShotDirection.y);
        bulletvelocity = bulletvelocity.normalized;
        GetComponent<Rigidbody2D>().velocity = bulletvelocity * speed;
    }

    public void spawninformation(Vector3 spawnposition, Vector3 bulletdirection, String owner)
    {
        bulletSpawnPosition = spawnposition;
        bulletShotDirection = bulletdirection;
        bulletOwner = owner;
    }

    private void Update()
    {
        if (!IsOwner) return;
        
        lifetime -= 2.5f * Time.deltaTime;

        if (lifetime <= 0)
        {
            DestroyBulletServerRpc();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsOwner) return;
        
        if (other.tag != bulletOwner)
        {
            other.GetComponent<HealthCode>().TakeDamage();
            DestroyBulletServerRpc();
        }
    }

    [ServerRpc]
    private void DestroyBulletServerRpc()
    {
        DestroyBulletClientRpc();
    }
    
    [ClientRpc]
    private void DestroyBulletClientRpc()
    {
        Destroy(gameObject);
    }
}
