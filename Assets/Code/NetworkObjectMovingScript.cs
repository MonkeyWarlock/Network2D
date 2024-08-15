using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;

public class NetworkObjectMovingScript : NetworkBehaviour
{

    private float moveSpeed = 3.5f;
    private Vector3 networkPosition;
    private NetworkVariable<Vector3> moveDir = 
        new NetworkVariable<Vector3>(new Vector3(0,1,0), NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    [SerializeField] private Vector3 spawnPosition = new Vector3(-5f, 0, 0);
    public override void OnNetworkSpawn()
    {
        networkPosition = spawnPosition;
        transform.position = spawnPosition;
    }

    void Update()
    {

        // if (!IsOwner)
        // {
        //     transform.position = networkPosition;
        //     return;
        // }
        // CurrentPositionToServerRpc(transform.position);
        
        Move();
    }

    private void Move()
    {
        if (math.abs(transform.position.y) >= 2.5)
        {
            moveDir.Value =  new (moveDir.Value.x, moveDir.Value.y * -1 , moveDir.Value.z);
        }
        
        transform.position += moveDir.Value * moveSpeed * Time.deltaTime;

    }
    
    
    
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
