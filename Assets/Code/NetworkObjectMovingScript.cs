using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;

public class NetworkObjectMovingScript : NetworkBehaviour
{

    private float moveSpeed = 3.5f;
    private Vector3 networkPosition;
    private Vector3 moveDir = new Vector3(0,1,0);

    [SerializeField] private Vector3 spawnPosition = new Vector3(-5f, 0, 0);
    public override void OnNetworkSpawn()
    {
        networkPosition = spawnPosition;
        transform.position = spawnPosition;
    }

    void Update()
    {
        
        Move();
    }

    private void Move()
    {
        if (math.abs(transform.position.y) >= 2.5)
        {
            moveDir =  new (moveDir.x, moveDir.y * -1 , moveDir.z);
        }
        
        transform.position += moveDir * moveSpeed * Time.deltaTime;

    }
    
}
