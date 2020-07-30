using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
//Lots of changes here. Why and what did you change?
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
        AttackProjectile.GetComponent<Bullet>().owner = transform;
        AttackProjectile.GetComponent<Rigidbody2D>().velocity = (myDir * bulletSpeed);
        NetworkServer.Spawn(AttackProjectile);
        Destroy(AttackProjectile, 1.0f);
    }
}