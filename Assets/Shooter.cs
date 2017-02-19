using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

    [SerializeField]
    private GameObject m_projectileSpawner;

    [SerializeField]
    private GameObject m_projectileType;

    [SerializeField]
    private float m_shotInterval;
    
    [SerializeField]
    private int m_power;

    [SerializeField]
    private int m_cachedProjectiles;

    private List<GameObject> m_projectiles;

    private int m_currentProjectile;
    private float m_timeSinceLastShot;


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
    private int m_movementAxis;
    [SerializeField]
    private float m_movementSpeed;

    private Vector3 m_originalPosition;
    private float m_distance;
    private float m_startTime;
    private bool m_movingToGoal;
    private bool m_waiting;
    private float m_timeSinceWaitStart;

    // Use this for initialization
    void Start ()
    {
        m_projectiles = new List<GameObject>();
        for(int i = 0; i < m_cachedProjectiles; i++)
        {
            GameObject temp = Instantiate(m_projectileType, m_projectileSpawner.transform.position, Quaternion.identity);
            m_projectiles.Add(temp);
            print(m_projectiles[i]);
            m_projectiles[i].transform.position = m_projectileSpawner.transform.position;
            m_projectiles[i].GetComponent<Projectile>().SetProjectileIndex(i);
            m_projectiles[i].gameObject.SetActive(false);
            print("index: " + i);           
        }
        m_timeSinceLastShot = 0;
        m_currentProjectile = 0;

        Projectile.PROJECTILECOLLISION += ReturnProjectileToPool;

        m_movingToGoal = true;
        m_startTime = Time.time;
        m_originalPosition = m_movingObject.transform.position;
        m_distance = Vector3.Distance(m_originalPosition, m_goalPosition.position);
    }

    void Update()
    {
        if (m_timeSinceLastShot >= m_shotInterval)
        {
            Shoot();
        }
        else
        {
            m_timeSinceLastShot += Time.deltaTime;
        }
        print(m_timeSinceLastShot);

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
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		

    }

    private void Shoot()
    {
        print("shooting");
       
        m_projectiles[m_currentProjectile].gameObject.SetActive(true);
        m_projectiles[m_currentProjectile].transform.position = m_projectileSpawner.transform.position;
        m_projectiles[m_currentProjectile].GetComponent<Rigidbody>().AddForce(new Vector3(m_power,0 , 0));
        //m_currentProjectile = m_projectiles.Length - 1 ? 0 : m_currentProjectile++;
        if(m_currentProjectile == m_projectiles.Count -1)
        {
            m_currentProjectile = 0;
        }
        else
        {
            m_currentProjectile++;
        }
        m_timeSinceLastShot = 0;
    }

    private void ReturnProjectileToPool(int index)
    {       
       // m_projectiles[index].
        m_projectiles[index].gameObject.SetActive(false);
    }

    private void MoveObject()
    {
        if (m_movingToGoal)
        {
            print("moving to goal");
            MoveToGoal();
        }
        else if (!m_movingToGoal)
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

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(m_waitTime);
        m_startTime = Time.time;
        // m_movingToGoal = !m_movingToGoal ?  true : false;
        if (m_movingToGoal)
        {
            m_movingToGoal = false;
        }
        else
        {
            m_movingToGoal = true;
        }
    }
}
