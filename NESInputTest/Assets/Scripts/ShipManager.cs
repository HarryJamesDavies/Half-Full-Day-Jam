using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class ShipManager
{
    //set the spawn points
    public Transform m_SpawnPoint;
    public bool m_died = false;
    private Movement m_Movement;

    //what player are we looking at
    [HideInInspector]public int m_PlayerNumber;

    [HideInInspector]public GameObject m_PlayerInstance;

    //record how many wins each player has
    [HideInInspector]public int m_NumberOfWins = 0;


	// Use this for initialization
	void SetUp ()
    {
        m_Movement = m_PlayerInstance.GetComponent<Movement>();
        //m_Movement.m_PlayerNumber = m_PlayerNumber;
    }

    public void reset()
    {
        //reset the respective ships to thier spawn locations
        m_PlayerInstance.transform.position = m_SpawnPoint.position;
        m_PlayerInstance.transform.rotation = m_SpawnPoint.rotation;

        m_PlayerInstance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        m_died = false;

        Debug.Log(m_NumberOfWins);
    }
}
