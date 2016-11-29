using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    int m_bulletDirection;
    bool m_inAir;
    Rigidbody2D m_rigidBody;
    Vector2 bulletForce;
    float drag;

	// Use this for initialization
	void Start () {
        m_rigidBody = gameObject.GetComponent<Rigidbody2D>();
        drag = 50.0f;
        StartCoroutine(countdownTillDeath(5.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (m_inAir == false)
        {
            if (m_bulletDirection == 0)
            {
                bulletForce = new Vector2(0.0f, 100.0f);
                m_rigidBody.AddForce(bulletForce);
                //m_rigidBody.drag = drag;
                m_inAir = true;
            }
            else if (m_bulletDirection == 1)
            {
                bulletForce = new Vector2(-100.0f, 0.0f);
                m_rigidBody.AddForce(bulletForce);
                //m_rigidBody.drag = drag;
                m_inAir = true;
            }
            else if (m_bulletDirection == 2)
            {
                bulletForce = new Vector2(100.0f, 0.0f);
                m_rigidBody.AddForce(bulletForce);
                //m_rigidBody.drag = drag;
                m_inAir = true;
            }
            else
            {
                //m_rigidBody.drag = drag;
            }

        }
    }

    

    public void setDirectionAndPosition(GameObject parent, int _direction)
    {
        transform.position = parent.transform.position;
        m_bulletDirection = _direction;

        
    }

    IEnumerator countdownTillDeath(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
