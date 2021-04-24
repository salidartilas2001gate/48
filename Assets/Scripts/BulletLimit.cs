using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLimit : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Bullet>() != null)
        {
            collision.gameObject.SetActive(false);
        }
    }
}
