using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSide : MonoBehaviour
{
    public float speed = 8.5f;
    private float leftBound = -90f;
    private float rightBound = 90f;

    // Update is called once per frame
    void Update()
    {
        //if (PlayerController.gameOver == false && PlayerController.isGameActive == true)
        //{
            //transform.Translate(Vector3.down * Time.fixedDeltaTime * speed);
            transform.position += Vector3.left * Time.deltaTime * speed;
        //}
        if (transform.position.x < leftBound)
        {
            gameObject.SetActive(false);
        }
        else if (transform.position.x > rightBound)
        {
            gameObject.SetActive(false);
        }        
    }
}
