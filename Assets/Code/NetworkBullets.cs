using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkBullets : NetworkBehaviour
{
    private float speed = 8f;
    private float lifetime = 3f;
    private Vector3 bulletSpawnPosition;
    private Vector3 bulletShotDirection;
    public override void OnNetworkSpawn()
    {
        transform.position = bulletSpawnPosition;
        Vector2 bulletvelocity = new Vector2(bulletShotDirection.x, bulletShotDirection.y);
        bulletvelocity = bulletvelocity.normalized;
        GetComponent<Rigidbody2D>().velocity = bulletvelocity * speed;
    }

    public void spawninformation(Vector3 spawnposition, Vector3 bulletdirection)
    {
        bulletSpawnPosition = spawnposition;
        bulletShotDirection = bulletdirection;
    }

    private void Update()
    {
        lifetime -= 2.5f * Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
