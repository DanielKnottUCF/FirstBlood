using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Attack : NetworkBehaviour
{
    [SerializeField]
    private GameObject AttackProjectilePrefab;

    [SerializeField]
    private float bulletSpeed;

    void Awake(){
        //GetComponent<Rigidbody2D>().AddForce(Vector3.forward * bulletSpeed);
        //Destroy(gameObject, 1.0f);
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
      
        //transform.position = (transform.position * bulletSpeed * Time.deltaTime);
        RpcFire(myPosition, myDir, rotation);
    }

    [ClientRpc]
    void RpcFire(Vector2 myPosition, Vector2 myDir, Vector3 rotation)
    {
        GameObject AttackProjectile = Instantiate(AttackProjectilePrefab, myPosition, Quaternion.Euler(rotation)) as GameObject;
        AttackProjectile.GetComponent<Bullet>().owner = transform;
        AttackProjectile.GetComponent<Rigidbody2D>().velocity = (myDir * bulletSpeed);
        NetworkServer.Spawn(AttackProjectile);
        Destroy(AttackProjectile, 2.0f);
    }
}