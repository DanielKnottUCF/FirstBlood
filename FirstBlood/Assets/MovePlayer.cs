using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MovePlayer : NetworkBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D rb;

    void FixedUpdate(){
        if (this.isLocalPlayer){
            rb = GetComponent<Rigidbody2D>();
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2 (x, y);
            rb.velocity = (movement * speed);

            Vector3 mouseScreen = Input.mousePosition;
            Vector3 mouse = Camera.main.ScreenToWorldPoint(mouseScreen);
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg - 90);
        }
    }
}
