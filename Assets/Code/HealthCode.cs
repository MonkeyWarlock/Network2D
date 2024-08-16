using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class HealthCode : NetworkBehaviour
{
    [SerializeField] int Health;

    private void Update()
    {
        
        if (Health <= 0)
        {
            //DestroyServerRpc();
            Destroy(gameObject);
        }
    }

    public void TakeDamage()
    {
        Health -= 1;
        //Debug.Log("currenthealth: " + Health);
    }
    

    [ServerRpc]
    private void DestroyServerRpc()
    {
        //DestroyClientRpc();
        Destroy(gameObject);
    }
    
    [ClientRpc]
    private void DestroyClientRpc()
    {
        Destroy(gameObject);
    }
}
