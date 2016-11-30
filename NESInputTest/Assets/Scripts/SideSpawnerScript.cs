using UnityEngine;
using System.Collections;

public class SideSpawnerScript : MonoBehaviour {

    [SerializeField]
    GameObject m_bulletRef;

    [SerializeField]
    int side = 0;

    [SerializeField]
    int counter = 0;

    [SerializeField]
    int timeToShoot;
    
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
        float posY;
        float posX;
        float min = 0.0f;
        float max = 10.0f;

        if (side == 0) //top
        {
            min = -20.0f;
            max = 20.0f;

            posY = 15.0f; //up or down boundary
            posX = Random.Range(min, max); //right or left boundary boundary

            transform.position = new Vector3(posX, posY, 0.0f);

            Instantiate(m_bulletRef).GetComponent<BulletScript>().setDirectionAndPosition(gameObject, 3);
        }
        else if (side == 1) //left
        {
            min = -10.0f;
            max = 10.0f;

            posY = Random.Range(min, max); //up or down boundary
            posX = -50.0f; //right or left boundary boundary

            transform.position = new Vector3(posX, posY, 0.0f);

            Instantiate(m_bulletRef).GetComponent<BulletScript>().setDirectionAndPosition(gameObject, 0);
        }
        else if (side == 2) //down
        {
            min = -20.0f;
            max = 20.0f;

            posY = -15.0f; //up or down boundary
            posX = Random.Range(min, max); //right or left boundary boundary

            transform.position = new Vector3(posX, posY, 0.0f);

            Instantiate(m_bulletRef).GetComponent<BulletScript>().setDirectionAndPosition(gameObject, 2);
        }
        else if (side == 3) //right
        {
            min = -10.0f;
            max = 10.0f;

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
}
