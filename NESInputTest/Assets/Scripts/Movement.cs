using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public int m_PlayerNumber;

    public Rigidbody2D m_rb;

    ShipManager m_currentShip;

    [SerializeField]
    AudioClip m_bash;

    AudioSource m_audio;

    float m_thrust;
    float m_boostSpeed;
    float m_rotationSpeed;

    public bool m_died = false;


    void Start()
    {
        m_audio = GetComponent<AudioSource>();
        m_audio.clip = m_bash;
        m_rb = GetComponent<Rigidbody2D>();
        m_thrust = 5.0f;
        m_boostSpeed = 400.0f;
        m_rotationSpeed = 50.0f;

        foreach (ShipManager ships in GameManager.m_instance.m_ships)
        {
            if (this.gameObject == ships.m_PlayerInstance)
            {
                m_currentShip = ships;
            }
        }
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

            Vector2 position = new Vector2(transform.position.x, transform.position.y);
            if (!m_currentShip.m_died)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(BlackHole.m_instance.GetGravityForce(position, true), ForceMode2D.Force);
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(BlackHole.m_instance.GetGravityForce(position, true) * 50.0f, ForceMode2D.Force);
            }
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
                if (!m_currentShip.m_died)
                {
                    m_audio.Play();
                }
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
                switch (_controller)
                {
                    case 1:
                        if (GameManager.m_instance.m_p1Boost >= 30.0f)
                        {
                            Vector2 m_boostForce = (Vector2)transform.right * m_boostSpeed * Time.deltaTime;
                            gameObject.GetComponent<Rigidbody2D>().AddForce(m_boostForce, ForceMode2D.Impulse);
                            GameManager.m_instance.m_p1Boost -= 30.0f;
                        }
                        break;
                    case 2:
                        if (GameManager.m_instance.m_p2Boost >= 30.0f)
                        {
                            Vector2 m_boostForce = (Vector2)transform.right * m_boostSpeed * Time.deltaTime;
                            gameObject.GetComponent<Rigidbody2D>().AddForce(m_boostForce, ForceMode2D.Impulse);
                            GameManager.m_instance.m_p2Boost -= 30.0f;
                        }
                        break;
                    case 3:
                        if (GameManager.m_instance.m_p3Boost >= 30.0f)
                        {
                            Vector2 m_boostForce = (Vector2)transform.right * m_boostSpeed * Time.deltaTime;
                            gameObject.GetComponent<Rigidbody2D>().AddForce(m_boostForce, ForceMode2D.Impulse);
                            GameManager.m_instance.m_p3Boost -= 20.0f;
                        }
                        break;
                    case 4:
                        if (GameManager.m_instance.m_p4Boost >= 30.0f)
                        {
                            Vector2 m_boostForce = (Vector2)transform.right * m_boostSpeed * Time.deltaTime;
                            gameObject.GetComponent<Rigidbody2D>().AddForce(m_boostForce, ForceMode2D.Impulse);
                            GameManager.m_instance.m_p4Boost -= 30.0f;
                        }
                        break;
                    default:
                        break;
                }
            }
            //else if (Input.GetButtonDown("P" + _controller + "-Select(NES)"))
            //{
            //    transform.position += new Vector3(0.0f, -1.0f, 0.0f);
            //}
            //else if (Input.GetButtonDown("P" + _controller + "-Start(NES)"))
            //{
            //    transform.position += new Vector3(-1.0f, 0.0f, 0.0f);
            //}

            float m_rotation = -Input.GetAxis("P" + _controller + "-Horizontal(NES)") * m_rotationSpeed * Time.deltaTime;
            transform.Rotate(0, 0, m_rotation);
        }
    }
}
