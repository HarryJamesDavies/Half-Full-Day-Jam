using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    public GameObject m_pause;
    public GameObject m_mainMenu;
    public GameObject m_controls;
    public GameObject m_HUD;
    public GameObject m_p3Score;
    public GameObject m_p4Score;
    public GameObject m_gameOver;
    public GameObject m_titleRef;
    public GameObject m_title;

    public Text m_numberOfPlayers;

	// Use this for initialization
	void Start () {

        m_title = Instantiate(m_titleRef);
    }
	
	// Update is called once per frame
	void Update () {

        //Check if the state has changed
        if (StateManager.m_instance.m_dirtyFlag)
        {
            //Call state exit behaviour
            OnStateExit();

            //Call state change behaviour
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
                    if (m_controls.activeSelf)
                    {
                        if ((Input.GetKeyDown(KeyCode.Return)) || (Input.GetButtonDown("P1-B(NES)")))
                        {
                            m_controls.SetActive(false);
                            m_mainMenu.SetActive(true);
                        }
                    }
                    else
                    {
                        if ((Input.GetKeyDown(KeyCode.Return)) || (Input.GetButtonDown("P1-A(NES)")))
                        {
                            StateManager.m_instance.ChangeState(StateManager.State.PLAY);
                        }

                        if ((Input.GetKeyDown(KeyCode.Space)) || (Input.GetButtonDown("P1-B(NES)")))
                        {
                            m_mainMenu.SetActive(false);
                            m_controls.SetActive(true);
                        }

                        if (Input.GetButtonDown("P1-Start(NES)"))
                        {
                            if (GameManager.m_instance.m_twoPlayers)
                            {
                                GameManager.m_instance.m_twoPlayers = false;
                                GameManager.m_instance.m_fourPlayers = true;
                            }
                            else if (GameManager.m_instance.m_fourPlayers)
                            {
                                GameManager.m_instance.m_twoPlayers = true;
                                GameManager.m_instance.m_fourPlayers = false;
                            }
                        }
                    }

                    if (GameManager.m_instance.m_fourPlayers == true)
                    {
                        m_numberOfPlayers.text = "Four Player";
                    }
                    else
                    {
                        m_numberOfPlayers.text = "Two Player";
                    }
                        break;
                }
            case StateManager.State.PLAY:
                {
                    //if ((Input.GetKeyDown(KeyCode.Space)) || (Input.GetButtonDown("P1-Start(NES)")) || (Input.GetButtonDown("P2-Start(NES)")))
                    //{
                    //    if (StateManager.m_instance.m_currentState != StateManager.State.MENU)
                    //    {
                    //        StateManager.m_instance.ChangeState(StateManager.State.MENU);
                    //    }
                    //}

                    if ((Input.GetKeyDown(KeyCode.P)) || (Input.GetButtonDown("P1-Select(NES)")) || (Input.GetButtonDown("P2-Select(NES)")))
                    {
                        StateManager.m_instance.ChangeState(StateManager.State.PAUSE);
                    }

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        StateManager.m_instance.ChangeState(StateManager.State.GAMEOVER);
                    }
                    break;
                }
            case StateManager.State.PAUSE:
                {
                    if ((Input.GetKeyDown(KeyCode.P)) || (Input.GetButtonDown("P1-Select(NES)")) || (Input.GetButtonDown("P2-Select(NES)")))
                    {
                        StateManager.m_instance.ChangeState(StateManager.State.PLAY);
                    }
                        break;
                }
            case StateManager.State.GAMEOVER:
                {
                    if ((Input.GetKeyDown(KeyCode.Return)) || (Input.GetButtonDown("P1-A(NES)")))
                    {
                        StateManager.m_instance.ChangeState(StateManager.State.MENU);
                    }
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
                    m_HUD.SetActive(false);
                    m_title = Instantiate(m_titleRef);
                    m_mainMenu.SetActive(true);
                    break;
                }
            case StateManager.State.PLAY:
                {
                    m_HUD.SetActive(true);
                    if (GameManager.m_instance.m_fourPlayers == true)
                    {
                        m_p3Score.SetActive(true);
                        m_p4Score.SetActive(true);
                    }
                    else
                    {
                        m_p3Score.SetActive(false);
                        m_p4Score.SetActive(false);
                    }
                    break;
                }
            case StateManager.State.PAUSE:
                {
                    m_HUD.SetActive(true);
                    m_pause.SetActive(true);
                    break;
                }
            case StateManager.State.GAMEOVER:
                {
                    m_gameOver.SetActive(true);
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
                    m_mainMenu.SetActive(false);
                    Destroy(m_title);
                    break;
                }
            case StateManager.State.PLAY:
                {
                    m_HUD.SetActive(false);
                    break;
                }
            case StateManager.State.PAUSE:
                {
                    m_pause.SetActive(false);
                    break;
                }
            case StateManager.State.GAMEOVER:
                {
                    m_gameOver.SetActive(false);
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}
