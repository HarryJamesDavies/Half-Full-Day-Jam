using UnityEngine;
using System.Collections;

public class BlackHoleArm : MonoBehaviour {

    public float m_gravityCoefficient;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay2D(Collider2D collision)
    {
        Vector2 position = new Vector2(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y);
        collision.gameObject.transform.position += BlackHole.m_instance.GetGravityForce(gameObject.transform.position, m_gravityCoefficient);
        //collision.GetComponent<Rigidbody2D>().AddForce(BlackHole.m_instance.GetGravityForce(position, true) * m_gravityCoefficient, ForceMode2D.Impulse);
    }
}
