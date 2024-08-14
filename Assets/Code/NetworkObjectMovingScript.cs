using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;

public class NetworkObjectMovingScript : NetworkBehaviour
{

    private NetworkVariable<Vector3> moveDir = 
        new NetworkVariable<Vector3>(new Vector3(0,1,0), NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    [SerializeField] private Vector3 spawnPosition = new Vector3(-5f, 0, 0);
    public override void OnNetworkSpawn()
    {
        transform.position = spawnPosition;
    }

    void Update()
    {
        float moveSpeed = 5f;
        
        if (math.abs(transform.position.y) >= 2.5)
        {
            moveDir.Value =  new (moveDir.Value.x, moveDir.Value.y * -1 , moveDir.Value.z);
        }
        
        transform.position += moveDir.Value * moveSpeed * Time.deltaTime;
        

    }
}
