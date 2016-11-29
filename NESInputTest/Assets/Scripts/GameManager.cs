using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Text m_scoreText;
    public Text m_timeText;

    public float m_maxTime;
    public bool m_countDown;

    private float m_score;
    private float m_playTime;

    private float m_startTime;
    private float m_pausedStartTime;
    private float m_pauseLength;

    private bool m_checkStop;

	// Use this for initialization
	void Start () {
        m_pauseLength = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Check if the state has changed
        if (StateManager.m_instance.m_dirtyFlag)
        {
            //Call state exit behaviour
            OnStateExit();

            //Call state enter behaviour
            OnStateEnter();
        }

        //State specific behaviour
        StateUpdate();
    }

    //Behaviour to perform per frame based on state
    void StateUpdate()
    {
        switch (StateManager.m_instance.m_currentState)
        {
            case StateManager.State.MENU:
                {
                    break;
                }
            case StateManager.State.PLAY:
                {
                    UpdateTime();
                    UpdateScore();

                    if (!m_checkStop)
                    {
                        CheckGameOver();
                    }
                    break;
                }
            case StateManager.State.PAUSE:
                {
                    break;
                }
            case StateManager.State.GAMEOVER:
                {
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    //Behaviour to perform on state change
    void OnStateEnter()
    {
        switch (StateManager.m_instance.m_currentState)
        {
            case StateManager.State.MENU:
                {
                    break;
                }
            case StateManager.State.PLAY:
                {
                    if (StateManager.m_instance.m_prevState != StateManager.State.PAUSE)
                    {
                        ResetData();            
                    }
                    m_checkStop = false;
                    break;
                }
            case StateManager.State.PAUSE:
                {
                    m_pausedStartTime = Time.time;
                    break;
                }
            case StateManager.State.GAMEOVER:
                {
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    void OnStateExit()
    {
        m_checkStop = true;

        switch (StateManager.m_instance.m_prevState)
        {
            case StateManager.State.MENU:
                {
                    break;
                }
            case StateManager.State.PLAY:
                {
                    break;
                }
            case StateManager.State.PAUSE:
                {
                    m_pauseLength += Time.time - m_pausedStartTime;
                    break;
                }
            case StateManager.State.GAMEOVER:
                {
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    void CheckGameOver()
    {
        if (m_playTime >= m_maxTime)
        {
            StateManager.m_instance.ChangeState(StateManager.State.GAMEOVER);
        }
    }

    void UpdateTime()
    {
        if (m_countDown)
        {
            m_playTime = (Time.time - m_startTime) - m_pauseLength;
            float timeRemaining = m_maxTime - m_playTime;
            m_timeText.text = "Time: " + timeRemaining.ToString("F1");
        }
        else
        {
            m_playTime = (Time.time - m_startTime) - m_pauseLength;
            m_timeText.text = "Time: " + m_playTime.ToString("F1");
        }
    }

    void UpdateScore()
    {
        m_scoreText.text = "Score: " + m_score;
    }

    public void AddToScore(float _value)
    {
        m_score += _value;
    }

    void ResetData()
    {
        m_startTime = Time.time;
        m_pausedStartTime = m_startTime;
        m_playTime = 0.0f;
        m_pauseLength = 0.0f;

        m_score = 0.0f;
    }
}
