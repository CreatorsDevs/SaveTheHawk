using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Slingshot : MonoBehaviour
{
    public LineRenderer[] lineRenderers;
    public Transform[] stripPositions;
    public Transform center;
    public Transform idlePosition;
    public Vector3 currentPosition;

    public float maxLength;

    public float bottomBoundary;
    bool isMouseDown;
    public GameObject rockPrefab;
    public float rockPositionOffset;
    Rigidbody2D rock;
    Collider2D rockCollider;
    public float force;
    private Camera _cameraRenderer;

    void Awake() 
    {
        _cameraRenderer = Camera.main;

    }
    
    void Start()
    {

        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);

        CreateRock();
    }

    void CreateRock()
    {
        lineRenderers[0].GetComponent<LineRenderer>().enabled = true;
        lineRenderers[1].GetComponent<LineRenderer>().enabled = true;
        rock = Instantiate(rockPrefab).GetComponent<Rigidbody2D>();
        rockCollider = rock.GetComponent<Collider2D>();
        rockCollider.enabled = false;

        rock.isKinematic = true;

        ResetStrips();
    }
    
    void Update()
    {
        if(rock == null) return;
        
        if(isMouseDown)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;

            currentPosition = _cameraRenderer.ScreenToWorldPoint(mousePosition);
            currentPosition = center.position + Vector3.ClampMagnitude(currentPosition
                - center.position, maxLength);

            currentPosition = ClampBoundary(currentPosition);

            SetStrips(currentPosition);

            if(rockCollider)
            {
                rockCollider.enabled = true;
            }
        }
        else
        {
            ResetStrips();
        }
        
    }

    private void OnMouseDown() 
    {
        AudioManager.instance.Play("Stretch");
        isMouseDown = true;
    }

    private void OnMouseUp() 
    {
        isMouseDown = false;
        Shoot();
        currentPosition = idlePosition.position;
    }

    void Shoot()
    {
        AudioManager.instance.Play("Release");

        if(rock == null) return;
        
        rock.isKinematic = false;
        Vector3 rockForce = (currentPosition - center.position) * force * -1;
        rock.velocity = rockForce;

        //rock.GetComponent<Rock>().Release();

        rock = null;
        rockCollider = null;
        lineRenderers[0].GetComponent<LineRenderer>().enabled = false;
        lineRenderers[1].GetComponent<LineRenderer>().enabled = false;
        Invoke(nameof(CreateRock), 1);
    }

    void ResetStrips()
    {
        currentPosition = idlePosition.position;
        SetStrips(currentPosition);
    }

    void SetStrips(Vector3 position)
    {
        lineRenderers[0].SetPosition(1, position);
        lineRenderers[1].SetPosition(1, position); 

        if(rock)
        {
            Vector3 dir = position - center.position;
            rock.transform.position = position + dir.normalized * rockPositionOffset;
            rock.transform.right = -dir.normalized;
        }        
    }

    Vector3 ClampBoundary(Vector3 vector)
    {
        vector.y = Mathf.Clamp(vector.y, bottomBoundary, 1000);
        return vector;
    }
}
