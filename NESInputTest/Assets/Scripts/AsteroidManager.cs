using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidManager : MonoBehaviour
{
    public GameObject m_asteroidSpawner;
    public List<Vector3> m_spawnerPosition;
    public float m_distance;
    public List<GameObject> m_spawners;
        
	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i <= m_spawnerPosition.Count - 1; i++)
        {
            CreateSpawner(m_spawnerPosition[i]);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    void CreateSpawner(Vector3 _direction)
    {
        Vector3 dpos = _direction * m_distance;
        Vector3 position = transform.position + dpos;
        GameObject spawner = (GameObject)Instantiate(m_asteroidSpawner, position, Quaternion.identity);
        spawner.transform.SetParent(this.transform);
        m_spawners.Add(spawner);
    }
}
