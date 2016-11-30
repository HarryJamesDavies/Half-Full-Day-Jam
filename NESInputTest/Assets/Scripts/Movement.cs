using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public int m_PlayerNumber;

    public Rigidbody2D m_rb;

    float m_thrust;
    float m_boostSpeed;
    float m_boostTotal;
    float m_rotationSpeed;


    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_thrust = 5.0f;
        m_boostSpeed = 400.0f;
        m_boostTotal = 100.0f;
        m_rotationSpeed = 50.0f;
    }

    void Update()
    {
        if (StateManager.m_instance.m_currentState == StateManager.State.PLAY)
        {
            switch (gameObject.tag)
            {
                case "Player1":
                    PlayerMovement(1);
                    break;
                case "Player2":
                    PlayerMovement(2);
                    break;
                default:
                    break;
            }

            if (m_boostTotal < 100.0f)
            {
                m_boostTotal += 0.2f;
            }
            else
            {
                m_boostTotal = 100.0f;
            }

            Vector2 position = new Vector2(transform.position.x, transform.position.y);
            gameObject.GetComponent<Rigidbody2D>().AddForce(BlackHole.m_instance.GetGravityForce(position, true), ForceMode2D.Force);
        }

    }

    void PlayerMovement(int _controller)
    {
        if (StateManager.m_instance.m_currentState == StateManager.State.PLAY)
        {
            Vector2 m_force = (Vector2)transform.right * m_thrust * Time.deltaTime; 

            if (Input.GetButton("P" + _controller + "-A(NES)"))
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(m_force, ForceMode2D.Impulse);
            }
            else if (Input.GetButtonDown("P" + _controller + "-B(NES)"))
            {
                if (m_boostTotal >= 40.0f)
                {
                    Vector2 m_boostForce = (Vector2)transform.right * m_boostSpeed * Time.deltaTime;
                    gameObject.GetComponent<Rigidbody2D>().AddForce(m_boostForce, ForceMode2D.Impulse);
                    m_boostTotal -= 40.0f;
                    Debug.Log(m_boostTotal);
                }
            }
            else if (Input.GetButtonDown("P" + _controller + "-Select(NES)"))
            {
                transform.position += new Vector3(0.0f, -1.0f, 0.0f);
            }
            else if (Input.GetButtonDown("P" + _controller + "-Start(NES)"))
            {
                transform.position += new Vector3(-1.0f, 0.0f, 0.0f);
            }

            float m_rotation = -Input.GetAxis("P" + _controller + "-Horizontal(NES)") * m_rotationSpeed * Time.deltaTime;
            transform.Rotate(0, 0, m_rotation);
        }
    }

    public void Reset()
    {
        m_rb.velocity = Vector2.zero;
    }
}
