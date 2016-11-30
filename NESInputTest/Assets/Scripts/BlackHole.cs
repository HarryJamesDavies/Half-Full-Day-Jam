using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour {

    public Vector3 m_rotationVector;

	// Use this for initialization
	void Start () {
	
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
}
