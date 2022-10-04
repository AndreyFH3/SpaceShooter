using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float speed;

    void Update()
    {
        transform.position = transform.position + (Vector3.down * speed * Time.deltaTime);
        if (transform.position.y <= -10)
        {
            transform.position = new Vector2(0,20);
        }
    }
}
