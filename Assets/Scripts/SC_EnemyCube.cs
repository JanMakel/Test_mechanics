using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script controls enemy cube AI
public class SC_EnemyCube : MonoBehaviour
{
    //Private variables
    Camera mainCamera;
    float movementTime = 0;
    Vector3 startPoint;
    Vector3 endPoint;

    // Start is called before the first frame update
    void Start()
    {
        //Get camera tagged "MainCamera"
        mainCamera = Camera.main;
        GenerateStartEndPoint();
    }

    //Assign start and end points slightly outside the Camera view
    void GenerateStartEndPoint()
    {
        Vector3 relativeStart;
        Vector3 relativeEnd;

        //Randomly pick whether to go Left <-> Right or Up <-> Down
        if (Random.Range(-10, 10) > 0)
        {
            relativeStart = new Vector3(Random.Range(-10, 10) > 0 ? 1.1f : -0.1f, Random.Range(0.00f, 1.00f), mainCamera.transform.position.y);
            if (relativeStart.y > 0.4f && relativeStart.y < 0.6f)
            {
                if (relativeStart.y >= 0.5f)
                {
                    relativeStart.y = 0.6f;
                }
                else
                {
                    relativeStart.y = 0.4f;
                }
            }
            relativeEnd = relativeStart;
            relativeEnd.x = relativeEnd.x > 1 ? -0.1f : 1.1f;
        }
        else
        {
            relativeStart = new Vector3(Random.Range(0.00f, 1.00f), Random.Range(-10, 10) > 0 ? 1.1f : -0.1f, mainCamera.transform.position.y);
            if (relativeStart.x > 0.4f && relativeStart.x < 0.6f)
            {
                if (relativeStart.x >= 0.5f)
                {
                    relativeStart.x = 0.6f;
                }
                else
                {
                    relativeStart.x = 0.4f;
                }
            }
            relativeEnd = relativeStart;
            relativeEnd.y = relativeEnd.y > 1 ? -0.1f : 1.1f;
        }

        //Convert screen points to world points
        startPoint = mainCamera.ViewportToWorldPoint(relativeStart);
        endPoint = mainCamera.ViewportToWorldPoint(relativeEnd);

        //Reset movement time
        movementTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Game over, wait for click
        if (SC_PlayerCube.GameOver)
        {
            //Click to resume
            if (Input.GetMouseButtonDown(0))
            {
                SC_PlayerCube.GameOver = false;
                GenerateStartEndPoint();
            }
            else
            {
                return;
            }
        }

        //Move enemy from one side to the other
        if (movementTime < 1)
        {
            movementTime += Time.deltaTime * 0.5f;

            transform.position = Vector3.Lerp(startPoint, endPoint, movementTime);
        }
        else
        {
            //Re-generate start / end point
            GenerateStartEndPoint();
        }
    }
}
