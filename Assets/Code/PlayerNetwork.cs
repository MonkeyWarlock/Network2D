using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    [SerializeField] private GameObject networkBullet;
    private Vector3 networkPosition;
    private Vector3 shootDirection;
    private void Update()
    {
        // if (!IsOwner)
        // {
        //     transform.position = networkPosition;
        //     return;
        // }
        // CurrentPositionToServerRpc(transform.position);
        
        Moving();
        Shoot();
    }

    private void Moving()
    {
        Vector3 movedir = new Vector3(0,0);
        if (Input.GetKey(KeyCode.W)) movedir.y = +1;
        if (Input.GetKey(KeyCode.S)) movedir.y = -1;
        if (Input.GetKey(KeyCode.D)) movedir.x = +1;
        if (Input.GetKey(KeyCode.A)) movedir.x = -1;

        float moveSpeed = 5f;
        transform.position += movedir * moveSpeed * Time.deltaTime;
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            shootDirection = Input.mousePosition;
            shootDirection.z = 0.0f;
            shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
            shootDirection -= transform.position;

            InititateBulletServerRpc(transform.position, shootDirection);
        }
    }

    [ServerRpc]
    private void InititateBulletServerRpc(Vector3 spawnposition, Vector3 bulletdirection)
    {
        GameObject newBullet = Instantiate(networkBullet);
        newBullet.GetComponent<NetworkBullets>().spawninformation(spawnposition, bulletdirection);
        newBullet.GetComponent<NetworkObject>().Spawn(true);
    }

    // [ClientRpc]
    // private void InititateBulletClientRpc(Vector3 spawnposition, Vector3 bulletdirection)
    // {
    //     
    // }
    
    
    // [ServerRpc]
    // private void CurrentPositionToServerRpc(Vector3 position)
    // {
    //     CurrentPositionFromClientRpc(position);
    // }
    //
    // [ClientRpc]
    // private void CurrentPositionFromClientRpc(Vector3 position)
    // {
    //     if (IsOwner) return;
    //     networkPosition = position;
    // }
}
