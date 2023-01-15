using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBig : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        WallThick script = other.gameObject.GetComponent<WallThick>();

        if(script)
            script.TakeDamage();
    }
}
