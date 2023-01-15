using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallThick : MonoBehaviour
{
    public void TakeDamage()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
    }
}
