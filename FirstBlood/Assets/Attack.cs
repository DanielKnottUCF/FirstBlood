using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
//Lots of changes here. Why and what did you change?
/*
    We call the fire function to run through a command as well as clientrpc. The issue before was that the code was only running on the server. Command takes care of that. But if we call the same function through clientrpc as well,
    it will be called and display properly on the client too.
*/
public class Attack : NetworkBehaviour
{
    [SerializeField]
    private GameObject AttackProjectilePrefab;

    [SerializeField]
    private float bulletSpeed;

    void Awake(){
    }
    
    void Update(){
        if (this.isLocalPlayer && Input.GetKeyDown(KeyCode.Space)){
            this.Fire();
        }
    }

    void Fire()
    {
        Vector3 moveDirection;
        moveDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        moveDirection.z = 0;
        moveDirection.Normalize();
        CmdShoot(transform.position, moveDirection ,transform.rotation.eulerAngles);
    }

    [Command]
    void CmdShoot(Vector2 myPosition, Vector2 myDir, Vector3 rotation)
    {
      
        
        RpcFire(myPosition, myDir, rotation);
    }
    
    [ClientRpc]
    void RpcFire(Vector2 myPosition, Vector2 myDir, Vector3 rotation)
    {
        GameObject AttackProjectile = Instantiate(AttackProjectilePrefab, myPosition, Quaternion.Euler(rotation)) as GameObject;
        AttackProjectile.GetComponent<Bullet>().owner = transform; // we set the owner to ourselves so we do not deal ourself damage when this collider passes through us
        AttackProjectile.GetComponent<Rigidbody2D>().velocity = (myDir * bulletSpeed);
        NetworkServer.Spawn(AttackProjectile);
        Destroy(AttackProjectile, 1.0f);
    }
}