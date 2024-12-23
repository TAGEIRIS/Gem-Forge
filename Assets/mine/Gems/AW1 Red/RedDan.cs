using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class RedDan : MonoBehaviour
{
    public Vector2 Speed;

    private void Start()
    {
        Speed = GetComponent<Rigidbody2D>().velocity;
        StartCoroutine(Dead());
    }
    private void Update()
    {
        Speed-=SpeedDown(Speed);
        GetComponent<Rigidbody2D>().velocity = Speed;
    }
    IEnumerator Dead()
    {
        yield return new WaitForSeconds(0.5f);
        this.transform.position = new Vector3(5000,5000,0);
    }
    private Vector2 SpeedDown(Vector2 vector2)
    {
        return new(vector2.x*0.01f,vector2.y*0.01f);
    }

}
