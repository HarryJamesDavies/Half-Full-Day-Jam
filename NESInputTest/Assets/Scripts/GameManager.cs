using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager m_instance;

    public ShipManager[] m_ships;
    public GameObject m_p1Prefab;
    public GameObject m_p2Prefab;

    public int m_NumberOfRoundsToWin = 5;
    public int m_RoundNumber;

    //used for delays in between rounds
    private WaitForSeconds m_roundStart;
    private WaitForSeconds m_roundEnd;

    //who wins what and when?
    private ShipManager m_Roundwinner;
    private ShipManager m_GameWinner;


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
    void Start()
    {
        m_roundStart = new WaitForSeconds(3.0f);
        m_roundEnd = new WaitForSeconds(3.0f);

        SpawnShips();
        m_pauseLength = 0.0f;

        if (m_instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_instance = this;
        }
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
                    break;
                }
            case StateManager.State.PLAY:
                {
                    UpdateTime();
                    UpdateScore();

                    //EnableShips();

                    //if (CheckShipCount())
                    //{
                    //    StateManager.m_instance.ChangeState(StateManager.State.RESET);
                    //}

                    //if (!m_checkStop)
                    //{
                    //    CheckGameOver();
                    //}
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
                    StateManager.m_instance.ChangeState(StateManager.State.PLAY);
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
                        ResetShips();
                        //DisableShips();

                        m_RoundNumber++;
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
                    m_Roundwinner.m_NumberOfWins = 0;
                    break;
                }
            case StateManager.State.RESET:
                {
                    //hello
                    if(GetGameWinner() != null)
                    {
                        StateManager.m_instance.ChangeState(StateManager.State.GAMEOVER);
                    }
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
                    m_Roundwinner = null;

                    m_Roundwinner = GetRoundWinner();

                    if (m_Roundwinner != null)
                    {
                        m_Roundwinner.m_NumberOfWins++;
                        Debug.Log(m_Roundwinner.m_NumberOfWins);
                    }
                    
                    m_GameWinner = GetGameWinner();
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

    //============================================== MATTS WORK ==============================================//
    //========================================================================================================//

    ////disable ship control
    //private void DisableShips()
    //{
    //    for (int i = 0; i < m_ships.Length; i++)
    //    {
    //        m_ships[i].disableControl();
    //    }
    //}
    
    ////enable ship control
    //private void EnableShips()
    //{
    //    for (int i = 0; i < m_ships.Length; i++)
    //    {
    //        m_ships[i].enableControl();
    //    }
    //}

    //spawn ships at the spawn locations
    private void SpawnShips()
    { 
        m_ships[0].m_PlayerInstance =
            (GameObject)Instantiate(m_p1Prefab, m_ships[0].m_SpawnPoint.position, m_ships[0].m_SpawnPoint.rotation);

        m_ships[1].m_PlayerInstance =
        (GameObject)Instantiate(m_p2Prefab, m_ships[1].m_SpawnPoint.position, m_ships[1].m_SpawnPoint.rotation);
    }

    private void ResetShips()
    {
        for (int i = 0; i < m_ships.Length; i++)
        {
            m_ships[i].reset();
        }
    }

    //checks to see if there is one player left
    private bool CheckShipCount()
    {
        int numTanksinLevel = 0;

        for (int i = 0; i < m_ships.Length; i++)
        {
            //if there are enough rounds to win return that ship
            if (m_ships[i].m_PlayerInstance.activeSelf)
                numTanksinLevel++;
        }

        if (numTanksinLevel <= 1)
        {
            return true;
        }

        return false;

       // return numTanksinLevel <= 1;
    }

    //find the round winner
    private ShipManager GetRoundWinner()
    {
        for (int i = 0; i < m_ships.Length; i++)
        {
            //if there are enough rounds to win return that ship
            if (!m_ships[i].m_died)
            {
                return m_ships[i];
            }
        }

        return null;
    }

    //find the game winner
    private ShipManager GetGameWinner()
    {
        // Go through all the ships
        for (int i = 0; i < m_ships.Length; i++)
        {
            //if there are enough rounds to win return that ship
            if (m_ships[i].m_NumberOfWins == m_NumberOfRoundsToWin)
            {
                return m_ships[i];
            }
        }

        return null;
    }

    private IEnumerator StartNewRound()
    {
        ResetShips();
        //DisableShips();

        m_RoundNumber++;

        yield return m_roundStart;
    }

    private IEnumerator NewRoundPlaying()
    {
        //EnableShips();

        if(!CheckShipCount())
        {
            yield return null;
        }

    }

    private IEnumerator NewRoundEnded()
    {
        //DisableShips();
        m_Roundwinner = null;

        m_Roundwinner = GetRoundWinner();

        if (m_Roundwinner != null)
        {
            m_Roundwinner.m_NumberOfWins++;
        }

        m_GameWinner = GetGameWinner();

        yield return m_roundEnd;

    }


}

