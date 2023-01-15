using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject victory;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        IDestroyable destroyable = other.gameObject.GetComponent<IDestroyable>();
        if(destroyable != null)
        {
            Debug.Log("Found a script");
            destroyable.TakeDamage();
        }

        MenuBack menu = other.gameObject.GetComponent<MenuBack>();
        if (menu)
        {
            menu.TakeDamage();
            victory.SetActive(true);
        }
    }
}
