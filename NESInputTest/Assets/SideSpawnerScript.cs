using UnityEngine;
using System.Collections;

public class SideSpawnerScript : MonoBehaviour {

    [SerializeField]
    GameObject m_bulletRef;

    int counter = 0;
    
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if(counter == 500)
        {
            counter = 0;
            SpawnBullet();
        }

        counter++;
	}

    void SpawnBullet()
    {
        float min = 0.0f;
        float max = 10.0f;
        float posY = Random.Range(min, max);

        transform.position = new Vector3(0.0f, posY, 0.0f);

        Instantiate(m_bulletRef).GetComponent<BulletScript>().setDirectionAndPosition(gameObject, 2);

    }
}
