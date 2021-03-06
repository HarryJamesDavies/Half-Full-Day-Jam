﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager m_instance;

    public ShipManager[] m_ships;
    public GameObject m_p1Prefab;
    public GameObject m_p2Prefab;
    public GameObject m_p3Prefab;
    public GameObject m_p4Prefab;

    public int m_NumberOfRoundsToWin = 5;
    public int m_RoundNumber;

    //used for delays in between rounds
    private WaitForSeconds m_roundStart;
    private WaitForSeconds m_roundEnd;

    //who wins what and when?
    private ShipManager m_Roundwinner;
    private ShipManager m_GameWinner;


    public Text m_p1ScoreText;
    public Text m_p2ScoreText;
    public Text m_p3ScoreText;
    public Text m_p4ScoreText;
    public Text m_timeText;

    public Text m_p1BoostText;
    public Text m_p2BoostText;
    public Text m_p3BoostText;
    public Text m_p4BoostText;

    public float m_maxTime;
    public bool m_countDown;

    //floats for player scores
    private float m_p1Score;
    private float m_p2Score;
    private float m_p3Score;
    private float m_p4Score;
    private float m_playTime;

    private float m_startTime;
    private float m_pausedStartTime;
    private float m_pauseLength;

    private bool m_checkStop;

    public bool m_twoPlayers = true;
    public bool m_fourPlayers = false;

    public int m_dead = 0;
    public float m_p1Boost = 100.0f;
    public float m_p2Boost = 100.0f;
    public float m_p3Boost = 100.0f;
    public float m_p4Boost = 100.0f;

    public bool m_timeOut = false;


    // Use this for initialization
    void Start()
    {
        m_roundStart = new WaitForSeconds(3.0f);
        m_roundEnd = new WaitForSeconds(3.0f);

        //Check here to see if we should create a game for 2 or 4 players
        //At the moment we are defaulting to 2 players
        //...

        //SpawnShipsForTwoPlayers();
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
        if (m_playTime >= m_maxTime && !m_timeOut)
        {
            m_timeOut = true;
            foreach (ShipManager ships in m_ships)
            {
                if (!ships.m_died)
                {
                    ships.m_NumberOfWins++;
                }
            }

            m_p1Score = m_ships[0].m_NumberOfWins;
            m_p2Score = m_ships[1].m_NumberOfWins;
            m_p3Score = m_ships[2].m_NumberOfWins;
            m_p4Score = m_ships[3].m_NumberOfWins;
            StateManager.m_instance.ChangeState(StateManager.State.RESET);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

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
        if (m_twoPlayers == true)
        {
            m_p1Score = m_ships[0].m_NumberOfWins;
            m_p2Score = m_ships[1].m_NumberOfWins;
        }
        if (m_fourPlayers == true)
        {
            m_p1Score = m_ships[0].m_NumberOfWins;
            m_p2Score = m_ships[1].m_NumberOfWins;
            m_p3Score = m_ships[2].m_NumberOfWins;
            m_p4Score = m_ships[3].m_NumberOfWins;
        }

        m_p1BoostText.text = "Boost: " + Mathf.Round(m_p1Boost);
        m_p2BoostText.text = "Boost: " + Mathf.Round(m_p2Boost);
        m_p3BoostText.text = "Boost: " + Mathf.Round(m_p3Boost);
        m_p4BoostText.text = "Boost: " + Mathf.Round(m_p4Boost);

        if (m_p1Boost < 100.0f)
        {
            m_p1Boost += 0.2f;
        }
        else
        {
            m_p1Boost = 100.0f;
        }

        if (m_p2Boost < 100.0f)
        {
            m_p2Boost += 0.2f;
        }
        else
        {
            m_p2Boost = 100.0f;
        }

        if (m_p3Boost < 100.0f)
        {
            m_p3Boost += 0.2f;
        }
        else
        {
            m_p3Boost = 100.0f;
        }

        if (m_p4Boost < 100.0f)
        {
            m_p4Boost += 0.2f;
        }
        else
        {
            m_p4Boost = 100.0f;
        }

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
                    CheckDead();
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
                    m_timeOut = false;
                    m_playTime = 0.0f;
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
                    UpdateScore();
                    for (int i = 0; i < m_ships.Length; i++)
                    {
                        m_ships[i].m_NumberOfWins = 0;
                    }
                    break;
                }
            case StateManager.State.RESET:
                {
                    if (GetGameWinner() != null)
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
                    if (m_twoPlayers)
                    {
                        SpawnShipsForTwoPlayers();
                    }
                    else if (m_fourPlayers)
                    {
                        SpawnShipsForFourPlayers();
                    }
                    break;
                }
            case StateManager.State.PLAY:
                {
                    if (!m_timeOut)
                    {
                        m_Roundwinner = null;

                        m_Roundwinner = GetRoundWinner();

                        if (m_Roundwinner != null)
                        {
                            m_Roundwinner.m_NumberOfWins++;
                            Debug.Log(m_Roundwinner.m_NumberOfWins);
                        }

                        m_GameWinner = GetGameWinner();
                    }
                    else
                    {
                        //m_timeOut = false;
                    }
                    break;
                }
            case StateManager.State.PAUSE:
                {
                    m_pauseLength += Time.time - m_pausedStartTime;
                    break;
                }
            case StateManager.State.GAMEOVER:
                {
                    for (int i = 0; i < m_ships.Length; i++)
                    {
                        Destroy(m_ships[i].m_PlayerInstance);
                    }
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
        if (m_twoPlayers == true)
        {
            m_p1ScoreText.text = "Tardis: " + m_p1Score;
            m_p2ScoreText.text = "Enterprise: " + m_p2Score;
        }

        if(m_fourPlayers == true)
        {
            m_p1ScoreText.text = "Tardis: " + m_p1Score;
            m_p2ScoreText.text = "Enterprise: " + m_p2Score;
            m_p3ScoreText.text = "Falcon: " + m_p3Score;
            m_p4ScoreText.text = "Shuttle: " + m_p4Score;
        }
    }

    void ResetData()
    {
        m_startTime = Time.time;
        m_pausedStartTime = m_startTime;
        m_playTime = 0.0f;
        m_pauseLength = 0.0f;
        m_dead = 0;

        if (m_fourPlayers == true)
        {
            m_p1Score = 0.0f;
            m_p2Score = 0.0f;
            m_p3Score = 0.0f;
            m_p4Score = 0.0f;
        }
        if (m_twoPlayers == true)
        {
            m_p1Score = 0.0f;
            m_p2Score = 0.0f;
        }

       m_p1Boost = 100.0f;
       m_p2Boost = 100.0f;
       m_p3Boost = 100.0f;
       m_p4Boost = 100.0f;

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

    //set the game for two players
    public void SpawnShipsForTwoPlayers()
    {
        m_ships[0].m_PlayerInstance =
        (GameObject)Instantiate(m_p1Prefab, m_ships[0].m_SpawnPoint.position, m_ships[0].m_SpawnPoint.rotation);

        m_ships[1].m_PlayerInstance =
        (GameObject)Instantiate(m_p2Prefab, m_ships[1].m_SpawnPoint.position, m_ships[1].m_SpawnPoint.rotation);

        //globals to use for checking the game state
        m_twoPlayers = true;
        m_fourPlayers = false;
    }

    //set the game for four players
    public void SpawnShipsForFourPlayers()
    {
         m_ships[0].m_PlayerInstance =
         (GameObject)Instantiate(m_p1Prefab, m_ships[0].m_SpawnPoint.position, m_ships[0].m_SpawnPoint.rotation);

         m_ships[1].m_PlayerInstance =
         (GameObject)Instantiate(m_p2Prefab, m_ships[1].m_SpawnPoint.position, m_ships[1].m_SpawnPoint.rotation);

         m_ships[2].m_PlayerInstance =
         (GameObject)Instantiate(m_p3Prefab, m_ships[2].m_SpawnPoint.position, m_ships[2].m_SpawnPoint.rotation);

         m_ships[3].m_PlayerInstance =
         (GameObject)Instantiate(m_p4Prefab, m_ships[3].m_SpawnPoint.position, m_ships[3].m_SpawnPoint.rotation);

        //globals to use for checking the game state
        m_twoPlayers = false;
        m_fourPlayers = true;  
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
        int numShipsinLevel = 0;

        for (int i = 0; i < m_ships.Length; i++)
        {
            //if there are enough rounds to win return that ship
            if (m_ships[i].m_PlayerInstance.activeSelf)
                numShipsinLevel++;
        }

        if (numShipsinLevel <= 1)
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

    private void CheckDead()
    {
        if (m_fourPlayers)
        {
            if (m_dead >= 3)
            {
                StateManager.m_instance.ChangeState(StateManager.State.RESET);
                Debug.Log("Four player reset");
            }
        }
        else
        {
            if (m_dead >= 1)
            {
                StateManager.m_instance.ChangeState(StateManager.State.RESET);
                Debug.Log("Two player reset");
            }
        }
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
        m_Roundwinner = null;

        //set the round winner
        m_Roundwinner = GetRoundWinner();

        if (m_Roundwinner != null)
        {
            m_Roundwinner.m_NumberOfWins++;
        }

        //set the game winner 
        m_GameWinner = GetGameWinner();

        yield return m_roundEnd;

    }


}

