using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableEnemy : MonoBehaviour
{
    public int hp;

    public float dmgCD;
    private float whenDamaged;

    private bool canBeDamaged;

    private void Update()
    {
        if (!canBeDamaged)
        {
            if (Time.time > whenDamaged + dmgCD)
            {
                canBeDamaged = true;
            }
        }
        if (hp <= 0)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void TakeDamage()
    {
        TakeDamage(1);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        canBeDamaged = false;
        whenDamaged = Time.time;
    }
}
