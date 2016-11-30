using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public int m_PlayerNumber;

    public Rigidbody2D m_rb;

    Vector2 m_velocity = Vector2.zero;
    float m_thrust;
    float m_maxVelocity;
    float m_boostSpeed;
    float m_boostTotal;
    float m_rotationSpeed;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_thrust = 2.0f;
        m_maxVelocity = 20.0f;
        m_boostSpeed = 250.0f;
        m_boostTotal = 100.0f;
        m_rotationSpeed = 25.0f;
    }

    void Update()
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
            m_boostTotal += 1.0f;
        }
    }

    void PlayerMovement(int _controller)
    {
        if (StateManager.m_instance.m_currentState == StateManager.State.PLAY)
        {
            Vector2 m_force = (Vector2)transform.right * m_thrust * Time.deltaTime; 

            m_velocity = Vector2.ClampMagnitude(m_velocity, m_maxVelocity);

            if (Input.GetButton("P" + _controller + "-A(NES)"))
            {
                m_velocity += m_force;
            }
            else if (Input.GetButtonDown("P" + _controller + "-B(NES)"))
            {
                if (m_boostTotal >= 40.0f)
                {
                    Vector2 m_boostForce = (Vector2)transform.right * m_boostSpeed * Time.deltaTime;
                    m_velocity += m_boostForce;
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

            m_rb.MovePosition(m_rb.position + -m_velocity * Time.deltaTime);
        }
    }
}
