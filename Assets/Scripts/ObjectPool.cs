using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(5)]

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> rightSideBirds;
    public List<GameObject> leftSideBirds;
    public List<GameObject> rightSideArrows;
    public List<GameObject> leftSideArrows;
    public GameObject rightSideBirdsToPool;
    public GameObject leftSideBirdsToPool;
    public GameObject rightSideArrowsToPool;
    public GameObject leftSideArrowsToPool;
    public int amountToPool;

    void Awake() 
    {
        SharedInstance = this;    
    }
    // Start is called before the first frame update
    void Start()
    {
        rightSideBirds = new List<GameObject>();
        leftSideBirds = new List<GameObject>();
        rightSideArrows = new List<GameObject>();
        leftSideArrows = new List<GameObject>();
        GameObject rightBirds;
        GameObject leftBirds;
        GameObject rightArrows;
        GameObject leftArrows;
        for(int i=0; i<amountToPool; i++)
        {
            rightBirds = Instantiate(rightSideBirdsToPool);
            rightBirds.SetActive(false);
            rightSideBirds.Add(rightBirds);
        }

        for(int i=0; i<amountToPool; i++)
        {
            leftBirds = Instantiate(leftSideBirdsToPool);
            leftBirds.SetActive(false);
            leftSideBirds.Add(leftBirds);
        }

        for(int i=0; i<amountToPool; i++)
        {
            rightArrows = Instantiate(rightSideArrowsToPool);
            rightArrows.SetActive(false);
            rightSideArrows.Add(rightArrows);
        }

        for(int i=0; i<amountToPool; i++)
        {
            leftArrows = Instantiate(leftSideArrowsToPool);
            leftArrows.SetActive(false);
            leftSideArrows.Add(leftArrows);
        }

    }

    public GameObject GetRightBirdsObject()
    {
        for(int i = 0; i<amountToPool; i++)
        {
            if(!rightSideBirds[i].activeInHierarchy)
            {
                return rightSideBirds[i];
            }
        }
        return null;
    }

    public GameObject GetLeftBirdsObject()
    {
        for(int i = 0; i<amountToPool; i++)
        {
            if(!leftSideBirds[i].activeInHierarchy)
            {
                return leftSideBirds[i];
            }
        }
        return null;
    }

    public GameObject GetRightArrowsObject()
    {
        for(int i = 0; i<amountToPool; i++)
        {
            if(!rightSideArrows[i].activeInHierarchy)
            {
                return rightSideArrows[i];
            }
        }
        return null;
    }

    public GameObject GetLeftArrowsObject()
    {
        for(int i = 0; i<amountToPool; i++)
        {
            if(!leftSideArrows[i].activeInHierarchy)
            {
                return leftSideArrows[i];
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}