using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    private void Update()
    {
        if (!IsOwner) return;
        
        Vector3 movedir = new Vector3(0,0);
        if (Input.GetKey(KeyCode.W)) movedir.y = +1;
        if (Input.GetKey(KeyCode.S)) movedir.y = -1;
        if (Input.GetKey(KeyCode.D)) movedir.x = +1;
        if (Input.GetKey(KeyCode.A)) movedir.x = -1;

        float moveSpeed = 5f;
        transform.position += movedir * moveSpeed * Time.deltaTime;
    }
}
