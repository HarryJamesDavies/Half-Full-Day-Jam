using UnityEngine;
using System.Collections;

public class PulseScale : MonoBehaviour
{
    public float m_scaleDifference;
    public float m_pulseLength;
    public float m_scaleChange;
    public int m_scaleIncreaseFrequency;

    private Vector3 m_orignalScale;
    private Vector3 m_minimumScale;
    private Vector3 m_maximumScale;
    private float m_scaleLength;
    private float m_startTime;
    private bool m_grow = true;

    private Vector3 m_minimumRescale;
    private Vector3 m_maximumRescale;
    private float m_rescaleLength;
    private float m_rescaleStartTime;
    private bool m_rescalling = false;

    private int m_currentCount;
    private bool m_scaleNow;

    // Use this for initialization
    void Start()
    {
        m_orignalScale = this.transform.localScale;
        Reset();
    }

    // Update is called once per frame
    void Update()
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
                    if (!m_rescalling)
                    {
                        Pulse();
                        UpdateDirection();
                    }
                    else
                    {
                        ConstantScale(2.0f);

                        if (m_minimumScale == this.transform.localScale)
                        {
                            m_rescalling = false;
                            m_startTime = Time.time;
                        }
                    }
                    break;
                }
            case StateManager.State.PLAY:
                {
                    if (!m_rescalling)
                    {
                        Pulse();
                        UpdateDirection();

                        UpdateTimer();
                        if (m_scaleNow)
                        {
                            UpdateScale();
                            m_scaleNow = false;
                            SetRescale();
                        }
                    }
                    else
                    {
                        ConstantScale(2.0f);

                        if(m_minimumScale == this.transform.localScale)
                        {
                            m_rescalling = false;
                            m_startTime = Time.time;
                        }
                    }
                    break;
                }
            case StateManager.State.PAUSE:
                {
                    break;
                }
            case StateManager.State.GAMEOVER:
                {
                    if (m_rescalling)
                    {
                        ConstantScale(5.0f);

                        if (m_minimumScale == this.transform.localScale)
                        {
                            m_rescalling = false;
                            m_startTime = Time.time;
                        }
                    }
                    break;
                }
            case StateManager.State.RESET:
                {
                    Reset();
                    //this.transform.localScale = m_orignalScale;
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
                    Reset();
                    SetRescale();
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
                    Reset();
                    SetRescale();
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

    void ConstantScale(float _rescalRate)
    {
        float distCovered = (Time.time - m_rescaleStartTime) * _rescalRate;
        float fracJourney = (distCovered / m_rescaleLength);

        this.transform.localScale = Vector3.Lerp(m_minimumRescale, m_maximumRescale, fracJourney);
    }

    void SetRescale()
    {
        m_minimumRescale = this.transform.localScale;
        m_maximumRescale = m_minimumScale;
        m_rescaleLength = Vector3.Distance(m_minimumRescale, m_maximumRescale);
        m_rescaleStartTime = Time.time;
        m_grow = true;
        m_rescalling = true;
    }

    void Pulse()
    {
        float distCovered = (Time.time - m_startTime) * m_pulseLength;
        float fracJourney = (distCovered / m_scaleLength);

        if (m_grow)
        {
            this.transform.localScale = Vector3.Lerp(m_minimumScale, m_maximumScale, fracJourney);
        }
        else
        {
            this.transform.localScale = Vector3.Lerp(m_maximumScale, m_minimumScale, fracJourney);
        }
    }

    void UpdateDirection()
    {
        if (this.transform.localScale == m_minimumScale)
        {
            m_startTime = Time.time;
            m_grow = true;
        }
        else if (this.transform.localScale == m_maximumScale)
        {
            m_startTime = Time.time;
            m_grow = false;
        }
    }

    void UpdateScale()
    {
        m_minimumScale.x += m_scaleChange;
        m_minimumScale.y += m_scaleChange;
        m_maximumScale.x = m_minimumScale.x + m_scaleDifference;
        m_maximumScale.y = m_minimumScale.y + m_scaleDifference;

        m_startTime = Time.time;
    }

    void UpdateTimer()
    {
        m_currentCount++;
        if (m_currentCount == m_scaleIncreaseFrequency)
        {
            m_scaleNow = true;
            m_currentCount = 0;
        }
    }

    void Reset()
    {
        m_currentCount = 0;
        m_scaleNow = false;

        m_minimumScale = m_orignalScale;
        m_maximumScale.x = m_minimumScale.x + m_scaleDifference;
        m_maximumScale.y = m_minimumScale.y + m_scaleDifference;
        m_maximumScale.z = 1.0f;

        m_scaleLength = Vector3.Distance(m_minimumScale, m_maximumScale);
        m_startTime = Time.time;
    }
}
