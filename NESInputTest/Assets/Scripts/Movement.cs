using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public int m_PlayerNumber;

    public Rigidbody2D m_rb;

    [SerializeField]
    AudioClip m_bash;

    AudioSource m_audio;

    float m_thrust;
    float m_boostSpeed;
    float m_boostTotal;
    float m_rotationSpeed;


    void Start()
    {
        m_audio = GetComponent<AudioSource>();
        m_audio.clip = m_bash;
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
            m_rb.constraints = RigidbodyConstraints2D.None;
            switch (gameObject.tag)
            {
                case "Player1":
                    PlayerMovement(1);
                    break;
                case "Player2":
                    PlayerMovement(2);
                    break;
                case "Player3":
                    PlayerMovement(3);
                    break;
                case "Player4":
                    PlayerMovement(4);
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
        else if (StateManager.m_instance.m_currentState == StateManager.State.PAUSE || StateManager.m_instance.m_currentState == StateManager.State.MENU)
        {
            m_rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

    }

    void OnCollisionEnter2D(Collision2D _collider)
    {
        if (StateManager.m_instance.m_currentState == StateManager.State.PLAY)
        {
            if (_collider.gameObject.tag == "Player1" || _collider.gameObject.tag == "Player2"
                || _collider.gameObject.tag == "Player3" || _collider.gameObject.tag == "Player4"
                || _collider.gameObject.tag == "Bullet")
            {
                m_audio.Play();
            }
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
}
