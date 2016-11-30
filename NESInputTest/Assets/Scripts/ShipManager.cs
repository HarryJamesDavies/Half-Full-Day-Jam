using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class ShipManager
{
    //set the spawn points
    public Transform m_SpawnPoint;
    private Movement m_Movement;

    //what player are we looking at
    [HideInInspector]public int m_PlayerNumber;

    [HideInInspector]public GameObject m_PlayerInstance;

    //record how many wins each player has
    [HideInInspector]public int m_NumberOfWins;


	// Use this for initialization
	void SetUp ()
    {
        m_Movement = m_PlayerInstance.GetComponent<Movement>();
        m_Movement.m_PlayerNumber = m_PlayerNumber;
    }

    public void enableControl()
    {
        //enable the tank movement
        m_Movement.enabled = true;
      
    }

    public void disableControl()
    {
        //disable the tank movement
        m_Movement.enabled = false;

    }

    public void reset()
    {
        //reset the respective ships to thier spawn locations
        m_PlayerInstance.transform.position = m_SpawnPoint.position;
        m_PlayerInstance.transform.rotation = m_SpawnPoint.rotation;
    }


}
