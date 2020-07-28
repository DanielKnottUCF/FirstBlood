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
            this.CmdShoot();
        }
    }

    [Command]
    void CmdShoot(){
        Vector3 moveDirection;
        moveDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        moveDirection.z = 0;
        moveDirection.Normalize();
        //transform.position = (transform.position * bulletSpeed * Time.deltaTime);
        GameObject AttackProjectile = Instantiate(AttackProjectilePrefab, transform.position + transform.forward*2, transform.rotation);
        AttackProjectile.GetComponent<Rigidbody2D>().velocity = (moveDirection * bulletSpeed);
        NetworkServer.Spawn(AttackProjectile);
        Destroy(AttackProjectile, 2.0f);
    }
}