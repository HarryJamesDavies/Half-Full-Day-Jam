using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    Vector2 m_velocity = Vector2.zero;
    float m_thrust;
    float m_maxVelocity;
    float m_boostSpeed;
    float m_boostTotal;
    float m_rotationSpeed;

    void Start()
    {
        m_thrust = 2.0f;
        m_maxVelocity = 20.0f;
        m_boostSpeed = m_thrust * 2;
        m_boostTotal = 100.0f;
        m_rotationSpeed = 5.0f;
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
    }

    void PlayerMovement(int _controller)
    {
        if (StateManager.m_instance.m_currentState == StateManager.State.PLAY)
        {
            if (Input.GetButtonDown("P" + _controller + "-A(NES)"))
            {
                transform.position += new Vector3(0.0f, 1.0f, 0.0f);
            }
            else if (Input.GetButtonDown("P" + _controller + "-B(NES)"))
            {
                transform.position += new Vector3(1.0f, 0.0f, 0.0f);
            }
            else if (Input.GetButtonDown("P" + _controller + "-Select(NES)"))
            {
                transform.position += new Vector3(0.0f, -1.0f, 0.0f);
            }
            else if (Input.GetButtonDown("P" + _controller + "-Start(NES)"))
            {
                transform.position += new Vector3(-1.0f, 0.0f, 0.0f);
            }

            float m_rotation = Input.GetAxis("P" + _controller + "-Horizontal(NES)") * m_rotationSpeed * Time.deltaTime;
            transform.Rotate(0, 0, m_rotation);

            Vector2 m_force = (Vector2)transform.right * Input.GetAxis("P" + _controller +"Vertical") * m_thrust * Time.deltaTime;

            m_velocity += m_force;
            m_velocity = Vector2.ClampMagnitude(m_velocity, m_maxVelocity);

            transform.Translate(m_velocity * Time.deltaTime);

            // transform.position += new Vector3(Input.GetAxis("P" + _controller + "-Horizontal(NES)") * 0.1f, Input.GetAxis("P" + _controller + "-Vertical(NES)") * -0.1f, 0.0f);
        }
    }
}
