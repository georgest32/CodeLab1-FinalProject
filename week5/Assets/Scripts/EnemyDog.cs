using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDog : BaseEnemy
{
    public override void ShootProjectile()
    {
        GetComponent<Rigidbody2D>().velocity = FindObjectOfType<PlayerControl>().transform.position - transform.transform.position;
    }
}
