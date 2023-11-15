using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(50)]

public class GameManager : MonoBehaviour
{
    private Vector2 rightBirdsRandomSpawnPos;
    private Vector2 leftBirdsRandomSpawnPos;
    private Vector2 rightArrowsRandomSpawnPos;
    private Vector2 leftArrowsRandomSpawnPos;

    public GameObject[] birds;
    public GameObject[] arrows;
    public GameObject playButtonUi;
    public GameObject restartButtonUi;
    public GameObject CreditsPanelUi;
    public TextMeshProUGUI scoreValue;
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;

    GameObject rightBirds;
    GameObject leftBirds;
    GameObject rightArrows;
    GameObject leftArrows;

    public bool gameOver;
    private int score;
    public int livesLost;

    void Start()
    {
        score = 0;
        UpdateScore(0);
    }

    void Update()
    {
        float xBirdPosition = 70;
        float minYArrowPosition = -20;
        float maxYArrowPosition = -14;
        float minYBirdPosition = 12;
        float maxYBirdPosition = 26;
        float maxXArrowPosition = 35;
        float minXArrowPosition = 25;

        rightBirdsRandomSpawnPos = new Vector2(xBirdPosition,Random.Range(minYBirdPosition, maxYBirdPosition));
        leftBirdsRandomSpawnPos = new Vector2(-xBirdPosition,Random.Range(minYBirdPosition, maxYBirdPosition));
        rightArrowsRandomSpawnPos = new Vector2(Random.Range(minXArrowPosition, maxXArrowPosition), Random.Range(minYArrowPosition, maxYArrowPosition));
        leftArrowsRandomSpawnPos = new Vector2(Random.Range(-minXArrowPosition, -maxXArrowPosition), Random.Range(minYArrowPosition, maxYArrowPosition));

        LivesCounter();

        if(gameOver)
        {
            GameOver();
        }
    }

    IEnumerator SpawnBirds()
    {
        while(!gameOver)
        {
            yield  return new WaitForSeconds(2);
            if(gameOver) break;
            rightBirds = ObjectPool.SharedInstance.GetRightBirdsObject();
            leftBirds = ObjectPool.SharedInstance.GetLeftBirdsObject();
            if(rightBirds != null)
            {
                rightBirds.transform.position = rightBirdsRandomSpawnPos;
                rightBirds.transform.rotation = birds[0].transform.rotation;
                rightBirds.SetActive(true);
            }
            if(leftBirds != null)
            {
                leftBirds.transform.position = leftBirdsRandomSpawnPos;
                leftBirds.transform.rotation = birds[1].transform.rotation;
                leftBirds.SetActive(true);
            }
        }        
    }

    IEnumerator SpawnArrows()
    {
        while(!gameOver)
        {
            yield return new WaitForSeconds(5f);
            if(gameOver) break;
            leftArrows = ObjectPool.SharedInstance.GetLeftArrowsObject(); 
            rightArrows = ObjectPool.SharedInstance.GetRightArrowsObject();        
            if(leftArrows != null)
            {
                leftArrows.transform.position = leftArrowsRandomSpawnPos;
                leftArrows.transform.rotation = arrows[1].transform.rotation;
                leftArrows.SetActive(true);
            }
            yield return new WaitForSeconds(2.5f);
            if(gameOver) break;
            if(rightArrows != null)
            {
                rightArrows.transform.position = rightArrowsRandomSpawnPos;
                rightArrows.transform.rotation = arrows[0].transform.rotation;
                rightArrows.SetActive(true);
            }
        }       
    }

    public void PlayGame()
    {
        playButtonUi.SetActive(false);
        //Debug.Log("PlayGame");
        StartCoroutine(SpawnBirds());
        StartCoroutine(SpawnArrows());
        gameOver = false;
    }

    public void GameOver()
    {
        gameOver = true;
        restartButtonUi.SetActive(true);
        StopCoroutine(SpawnBirds());
        StopCoroutine(SpawnArrows());
        //StopAllCoroutines();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateScore(int scoreToAdd)
    {
        if(!gameOver)
        {
            score += scoreToAdd;
            scoreValue.text = score.ToString();
        }
    }

    void LivesCounter()
    {
        if(livesLost == 1)
        {
            life3.gameObject.SetActive(false);
        }
        else if(livesLost == 2)
        {
            life2.gameObject.SetActive(false);
        }
        else if(livesLost >= 3)
        {
            life1.gameObject.SetActive(false);  
        }
    }

    public void CreditsOpen()
    {
        CreditsPanelUi.gameObject.SetActive(true);
    }

    public void CreditsClose()
    {
        CreditsPanelUi.gameObject.SetActive(false);
    }

    public void PlayButtonSound()
    {
        AudioManager.instance.Play("Button");
    }
}
