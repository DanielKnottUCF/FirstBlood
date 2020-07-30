using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

//why add this script?
/*
I added this script because it contains the information needed to deal damage from one player to another without them 
directly communicating with one another. We use owner to know whether or not we are being hit by our own bullet. This prevents
players from damaging themselves.
*/
public class Bullet : NetworkBehaviour
{

   public int damage;
   public Transform owner;
}
