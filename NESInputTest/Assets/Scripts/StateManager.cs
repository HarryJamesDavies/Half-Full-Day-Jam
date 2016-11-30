using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateManager : MonoBehaviour
{
	public AudioClip Clip1;
	public AudioClip Clip2;
	public AudioClip Clip3;

    public enum State
    {
        MENU = 0,
        PLAY = 1,
        PAUSE = 2,
        GAMEOVER = 3,
        RESET = 4,
        Count
    };

    public static StateManager m_instance = null;

    public bool m_dirtyFlag;
    public State m_currentState;
    public State m_prevState;
    private State m_checkState;

    // Use this for initialization
    void Start()
    {
        if (m_instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_instance = this;
        }

        m_currentState = State.MENU;
        m_checkState = State.MENU;
        m_prevState = State.MENU;
        m_dirtyFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        m_dirtyFlag = false;

        if (m_checkState != m_currentState)
        {
            m_checkState = m_currentState;
            m_dirtyFlag = true;
        }

        //   COPY BETWEEN THESE TWO LINES FOR STATE STUFF   \\
        //==================================================\\

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
            case StateManager.State.RESET:
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
					AudioSource audio = GetComponent<AudioSource>();
					audio.clip = Clip1;
					audio.Play();
                    break;
                }
            case StateManager.State.PLAY:
                {
					AudioSource audio = GetComponent<AudioSource>();
					audio.clip = Clip2;
					audio.Play();
                    break;
                }
            case StateManager.State.PAUSE:
                {
                    break;
                }
            case StateManager.State.GAMEOVER:
                {
					AudioSource audio = GetComponent<AudioSource>();
					audio.clip = Clip3;
					audio.Play();
                    break;
                }
            case StateManager.State.RESET:
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
                    break;
                }
            case StateManager.State.GAMEOVER:
                {
                    break;
                }
            case StateManager.State.RESET:
                {
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    //==================================================\\

    public void ChangeState(State _state)
    {
        m_checkState = m_currentState;
        m_prevState = m_currentState;
        m_currentState = _state;
    }
}


