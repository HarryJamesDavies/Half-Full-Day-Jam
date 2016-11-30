using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour {

    public static BlackHole m_instance = null;

    public Vector3 m_rotationVector;
    public float m_gravityCoefficient;

	// Use this for initialization
	void Start ()
    {
	    if(m_instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_instance = this;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(m_rotationVector);
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            StateManager.m_instance.ChangeState(StateManager.State.GAMEOVER);           
        }

        Destroy(collision.gameObject);
    }

    public Vector3 GetGravityForce(Vector3 _objectPosition, float _delta = 0.0f)
    {
        if (_delta == 0.0f)
        {
            return -Vector2.MoveTowards(gameObject.transform.position, _objectPosition, m_gravityCoefficient);
        }

        return -Vector2.MoveTowards(gameObject.transform.position, _objectPosition, _delta);
    }

    public Vector2 GetGravityForce(Vector2 _objectPosition, float _delta = 0.0f)
    { 
        Vector2 Position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

        if (_delta == 0.0f)
        {
            return -Vector2.MoveTowards(Position, _objectPosition, m_gravityCoefficient);
        }
  
        return -Vector2.MoveTowards(Position, _objectPosition, _delta);
    }
}
