using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidSpawner : MonoBehaviour
{
    public int m_rarePercentage;
    public List<GameObject> m_commonAsteroids;
    public List<GameObject> m_rareAsteroids;
    public int m_spawnFrequency;

    private int m_minimumFrequency;
    private int m_currentCount;
    private bool m_spawnNow;
    private int m_spawnOffset;

    // Use this for initialization
    void Start ()
    {
        m_spawnOffset = Random.Range(-50, 50);
        m_spawnFrequency += m_spawnOffset;
        m_currentCount = 0;
        m_spawnNow = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (StateManager.m_instance.m_currentState == StateManager.State.PLAY)
        {
            UpdateTimer();
            if (m_spawnNow)
            {
                CreateAsteroid();
                m_spawnNow = false;
                //UpdateFrequency();
            }
        }
	}

    void CreateAsteroid()
    {
        Instantiate(RandomizeAsteroid(), this.transform.position, Quaternion.identity);
    }

    GameObject RandomizeAsteroid()
    {
        int randomRare = (int)Random.Range(0.0f, 101.0f);

        if (randomRare <= m_rarePercentage)
        {
            int randomAsteroid = (int)Random.Range(0.0f, m_rareAsteroids.Count);
            return m_rareAsteroids[randomAsteroid];
        }
        else
        {
            int randomAsteroid = (int)Random.Range(0.0f, m_commonAsteroids.Count);
            return m_commonAsteroids[randomAsteroid];
        }
    }

    void UpdateTimer()
    {
        m_currentCount++;
        if(m_currentCount == m_spawnFrequency)
        {
            m_spawnNow = true;
            m_currentCount = 0;
        }
    }

    void UpdateFrequency()
    {
        if (m_spawnFrequency != m_minimumFrequency)
        {
            m_spawnFrequency--;
        }
    }
}
