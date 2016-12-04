using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SideSpawnerScript : MonoBehaviour {

    [SerializeField]
    GameObject m_bulletRef;

    //[SerializeField]
    //GameObject m_bulletRef1;
    //[SerializeField]
    //GameObject m_bulletRef2;
    //[SerializeField]
    //GameObject m_bulletRef3;
    public List<GameObject> m_commonAsteriods;
    public List<GameObject> m_rareAsteriods;

    [SerializeField]
    int side = 0;

    [SerializeField]
    int counter = 0;

    [SerializeField]
    int timeToShoot;

    public int m_rarePercentage;
    
    // Use this for initialization
    void Start () {
        timeToShoot = 200;
	}
	
	// Update is called once per frame
	void Update () {

        if (StateManager.m_instance.m_currentState == StateManager.State.PAUSE)
        {

        }
        else
        {
            if (counter == timeToShoot)
            {
                counter = 0;

                if (timeToShoot > 70)
                {
                    SpawnBullet();
                    timeToShoot = timeToShoot - 10;
                }
                else if (timeToShoot == 70)
                {
                    timeToShoot = 60;
                    SpawnBullet();
                    SpawnBullet();
                    SpawnBullet();
                    SpawnBullet();
                }
                else if (timeToShoot == 60)
                {
                    SpawnBullet();
                    //SpawnBullet();
                }

            }

            counter++;
        }
	}

    void SpawnBullet()
    {
        //int randomAsteroid = (int)Random.Range(1.0f, 3.0f);

        float posY;
        float posX;
        float min = 0.0f;
        float max = 10.0f;

        RandomizeAsteroid();

        //if(randomAsteroid == 1)
        //{
        //    m_bulletRef = m_bulletRef1;
        //}
        //else if(randomAsteroid == 2)
        //{
        //    m_bulletRef = m_bulletRef2;
        //}
        //else if(randomAsteroid == 3)
        //{
        //    m_bulletRef = m_bulletRef3;
        //}

        if (side == 0) //top
        {
            min = -25.0f;
            max = 25.0f;

            posY = 25.0f; //up or down boundary
            posX = Random.Range(min, max); //right or left boundary boundary

            transform.position = new Vector3(posX, posY, 0.0f);

            Instantiate(m_bulletRef).GetComponent<BulletScript>().setDirectionAndPosition(gameObject, 3);
        }
        else if (side == 1) //left
        {
            min = -15.0f;
            max = 15.0f;

            posY = Random.Range(min, max); //up or down boundary
            posX = -50.0f; //right or left boundary boundary

            transform.position = new Vector3(posX, posY, 0.0f);

            Instantiate(m_bulletRef).GetComponent<BulletScript>().setDirectionAndPosition(gameObject, 0);
        }
        else if (side == 2) //down
        {
            min = -25.0f;
            max = 25.0f;

            posY = -25.0f; //up or down boundary
            posX = Random.Range(min, max); //right or left boundary boundary

            transform.position = new Vector3(posX, posY, 0.0f);

            Instantiate(m_bulletRef).GetComponent<BulletScript>().setDirectionAndPosition(gameObject, 2);
        }
        else if (side == 3) //right
        {
            min = -15.0f;
            max = 15.0f;

            posY = Random.Range(min, max); //up or down boundary
            posX = 50.0f; //right or left boundary boundary

            transform.position = new Vector3(posX, posY, 0.0f);

            Instantiate(m_bulletRef).GetComponent<BulletScript>().setDirectionAndPosition(gameObject, 1);
        }

        

    }

    public void SetSide(int sideToSet)
    {
        side = sideToSet;
    }

    void RandomizeAsteroid()
    {
        int randomRare = (int)Random.Range(0.0f, 101.0f);

        if(randomRare <= m_rarePercentage)
        {
            int randomAsteroid = (int)Random.Range(0.0f, m_rareAsteriods.Count);
            m_bulletRef = m_rareAsteriods[randomAsteroid];
        }
        else
        {
            int randomAsteroid = (int)Random.Range(0.0f, m_commonAsteriods.Count);
            m_bulletRef = m_commonAsteriods[randomAsteroid];
        }
    }
}
