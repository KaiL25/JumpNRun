using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    [SerializeField]
    private GameObject m_movingObject;

    [SerializeField]
    private bool m_moving;

    [SerializeField]
    private Transform m_goalPosition;

    //Determines how long the platform waits at a specific point before moving again.
    [SerializeField]
    private float m_waitTime;

    [SerializeField]
    private bool m_rotating;

    [SerializeField]
    private int m_movementAxis;

    [SerializeField]
    private int m_rotationAxis;

    [SerializeField]
    private float m_movementSpeed;

    [SerializeField]
    private float m_rotationSpeed;


    private Vector3 m_originalPosition;
    private float m_distance;
    private float m_startTime;
    private bool m_movingToGoal;
    private bool m_waiting;
    private float m_timeSinceWaitStart;
    // Use this for initialization
    void Start()
    {
        m_movingToGoal = true;
        m_startTime = Time.time;
        m_originalPosition = m_movingObject.transform.position;
        m_distance = Vector3.Distance(m_originalPosition, m_goalPosition.position);
        
    }
    void Update()
    {
        if (m_waiting)
        {
            m_timeSinceWaitStart += Time.deltaTime;
            if (m_timeSinceWaitStart >= m_waitTime)
            {
                m_waiting = false;
                m_timeSinceWaitStart = 0;
                m_startTime = Time.time;
            }
        }

        if (!m_waiting && m_moving)
        {
            MoveObject();
        }

        if (!m_waiting && m_rotating)
        {
            RotateObject();
        }
    }

    void FixedUpdate()
    {
        

      
    }


    private void MoveObject()
    {
        if (m_movingToGoal)
        {
            print("moving to goal");
            MoveToGoal();
        }
        else if(!m_movingToGoal)
        {
            print("moving to origin");
            MoveToOrigin();
        }      
    }

    private void MoveToGoal()
    {
        
        float distanceCovered = (Time.time - m_startTime) * m_movementSpeed;
        float fracJourney = distanceCovered / m_distance;
        transform.position = Vector3.Lerp(m_originalPosition, m_goalPosition.position, fracJourney);
        print("moving to goal: " + m_movingToGoal);
        if (transform.position == m_goalPosition.position)
        {           
            m_movingToGoal = false;
            m_waiting = true;
           // StartCoroutine("WaitTime");
        }
    }

    private void MoveToOrigin()
    {
        float distanceCovered = (Time.time - m_startTime) * m_movementSpeed;
        float fracJourney = distanceCovered / m_distance;
        transform.position = Vector3.Lerp(m_goalPosition.position, m_originalPosition, fracJourney);
        print("moving to origin: " + m_movingToGoal + "time passed: " + distanceCovered);
        if (transform.position == m_originalPosition)
        {
            m_waiting = true;
            m_startTime = Time.time;
            m_movingToGoal = true;
            // StartCoroutine("WaitTime");
        }
    }

    private void RotateObject()
    {
        if(m_rotationAxis == 0)
        {
            m_movingObject.transform.Rotate(Vector3.right * m_rotationSpeed * Time.deltaTime);            
        }

        else if(m_rotationAxis == 1)
        {
            m_movingObject.transform.Rotate(Vector3.up * m_rotationSpeed * Time.deltaTime);
        }

        else if(m_rotationAxis == 2)
        {
            m_movingObject.transform.Rotate(Vector3.back * m_rotationSpeed * Time.deltaTime);
        }
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(m_waitTime);
        m_startTime = Time.time;
       // m_movingToGoal = !m_movingToGoal ?  true : false;
        if(m_movingToGoal)
        {
            m_movingToGoal = false;
        }
        else
        {
            m_movingToGoal = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        other.transform.parent = gameObject.transform;
        print("parented");
    }

    void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }
}
