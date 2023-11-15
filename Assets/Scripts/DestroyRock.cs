using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRock : MonoBehaviour
{
    private float bottomBound = -15f;
    public ParticleSystem explosionParticle;
    public ParticleSystem brokenArrows;
    GameManager gameManager;
    int pointValue = 5;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (transform.position.y < bottomBound)
        {
            DestroyGameObject();
        }
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        AudioManager.instance.Play("Rock");
        if(other.gameObject.CompareTag("MidGround"))
        {
            Invoke(nameof(DestroyGameObject), 2);
        }
        else if(other.gameObject.CompareTag(nameof(Arrow)))
        {
            Instantiate(brokenArrows, transform.position, brokenArrows.transform.rotation);
            DestroyGameObject();
            other.gameObject.SetActive(false);
            gameManager.UpdateScore(pointValue);
        }
    }
    void DestroyGameObject()
    {
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        if(transform.position.y > bottomBound)
            AudioManager.instance.Play("Rock");  
        Destroy(gameObject);
    }
}
