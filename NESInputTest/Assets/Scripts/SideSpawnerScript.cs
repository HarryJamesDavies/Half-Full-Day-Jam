using UnityEngine;
using System.Collections;

public class SideSpawnerScript : MonoBehaviour {

    [SerializeField]
    GameObject m_bulletRef;

    [SerializeField]
    int side = 0;

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
        float posY;
        float posX;
        float min = 0.0f;
        float max = 10.0f;

        if (side == 0) //top
        {
            min = 0.0f;
            max = 10.0f;

            posY = 10.0f; //up or down boundary
            posX = Random.Range(min, max); //right or left boundary boundary

            transform.position = new Vector3(posX, posY, 0.0f);

            Instantiate(m_bulletRef).GetComponent<BulletScript>().setDirectionAndPosition(gameObject, 3);
        }
        else if (side == 1) //left
        {
            min = 0.0f;
            max = -10.0f;

            posY = Random.Range(min, max); //up or down boundary
            posX = 0.0f; //right or left boundary boundary

            transform.position = new Vector3(posX, posY, 0.0f);

            Instantiate(m_bulletRef).GetComponent<BulletScript>().setDirectionAndPosition(gameObject, 0);
        }
        else if (side == 2) //down
        {
            min = 0.0f;
            max = -10.0f;

            posY = 0.0f; //up or down boundary
            posX = Random.Range(min, max); //right or left boundary boundary

            transform.position = new Vector3(posX, posY, 0.0f);

            Instantiate(m_bulletRef).GetComponent<BulletScript>().setDirectionAndPosition(gameObject, 2);
        }
        else if (side == 3) //right
        {
            min = 0.0f;
            max = 10.0f;

            posY = Random.Range(min, max); //up or down boundary
            posX = 10.0f; //right or left boundary boundary

            transform.position = new Vector3(posX, posY, 0.0f);

            Instantiate(m_bulletRef).GetComponent<BulletScript>().setDirectionAndPosition(gameObject, 1);
        }

        

    }

    public void SetSide(int sideToSet)
    {
        side = sideToSet;
    }
}
