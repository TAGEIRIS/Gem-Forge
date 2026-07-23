using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenDan :MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            StartCoroutine(Dead());
        }
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(0.05f);
        transform.position = new Vector3(50000f,50000f,0);
    }
}
