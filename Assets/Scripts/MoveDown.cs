using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public float speed = 5f;
    private float bottomBound = -30f;

    void Update()
    {
        transform.position += Vector3.down * Time.deltaTime * speed;
        if (transform.position.y < bottomBound)
        {
            Destroy(gameObject);
        }
    }
}
