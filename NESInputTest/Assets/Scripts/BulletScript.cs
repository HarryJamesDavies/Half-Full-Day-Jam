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
        //StartCoroutine(countdownTillDeath(5.0f));
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = new Vector2(gameObject.transform.position.x, 
            gameObject.transform.position.y);

        if (m_inAir == false)
        { 
            bulletForce = BlackHole.m_instance.GetGravityForce(position, true) * 300.0f;
            //bulletForce = new Vector2(0.0f, 100.0f);
            m_rigidBody.AddForce(bulletForce);
            //m_rigidBody.drag = drag;
            m_inAir = true;
        }
    }

    void OnCollisionEnter2D(Collision2D _collider)
    {
        if(_collider.gameObject.tag == "Player1" || _collider.gameObject.tag == "Player2")
        {
            Vector2 position = new Vector2(gameObject.transform.position.x,
           gameObject.transform.position.y);
            _collider.gameObject.GetComponent<Rigidbody2D>().AddForce(BlackHole.m_instance.GetGravityForce(position, true) * 10, ForceMode2D.Impulse);
            Destroy(gameObject);
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
