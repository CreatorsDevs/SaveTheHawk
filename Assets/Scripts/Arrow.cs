using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rb;
    bool hasHit;
    public GameObject arrow;
    private float minlaunchForce = 28f;
    private float maxlaunchForce = 32f;
    private float bottomBound = -30f;
    public float rotationValue;
    public ParticleSystem birdVanishParticleEffect;
    public ParticleSystem brokenArrows;
    public GameObject leftDeadEagle;
    public GameObject rightDeadEagle;
    static int restartCounter = 0;
    GameManager gameManager;
    // Start is called before the first frame update
    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable() 
    {
        hasHit = false;
        arrow.GetComponent<Rigidbody2D>().velocity = transform.right * Random.Range(minlaunchForce, maxlaunchForce);
    }
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        gameManager.livesLost = restartCounter;       
        if(hasHit == false)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (transform.position.y < bottomBound)
        {
            gameObject.SetActive(false);            
        }

        if(restartCounter >= 3)
        {
            gameManager.GameOver();
            restartCounter = 0;            
        }                              
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        //rb.velocity = Vector2.zero;
        //rb.isKinematic = true;
        if(other.gameObject.CompareTag("RightBird"))
        {
            hasHit = true;
            Instantiate(brokenArrows, transform.position, brokenArrows.transform.rotation);
            Instantiate(rightDeadEagle, other.transform.position, rightDeadEagle.transform.rotation);
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
            restartCounter++;
            AudioManager.instance.Play("EagleCry");
        }
        else if(other.gameObject.CompareTag("LeftBird"))
        {
            hasHit = true;
            Instantiate(brokenArrows, transform.position, brokenArrows.transform.rotation);
            Instantiate(leftDeadEagle, other.transform.position, leftDeadEagle.transform.rotation);
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
            restartCounter++;
            AudioManager.instance.Play("EagleCry");
        }
    }

    void ResetRotation()
    {
        Vector3 temp = transform.rotation.eulerAngles;
        temp.z = rotationValue;
        transform.rotation = Quaternion.Euler(temp);
    }
}
