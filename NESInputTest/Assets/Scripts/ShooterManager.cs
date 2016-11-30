using UnityEngine;
using System.Collections;

public class ShooterManager : MonoBehaviour
{

    [SerializeField]
    GameObject sideShooterRef;

    [SerializeField]
    GameObject m_rightShooter;
    [SerializeField]
    GameObject m_leftShooter;
    [SerializeField]
    GameObject m_upShooter;
    [SerializeField]
    GameObject m_bottomShooter;
    [SerializeField]
    bool shootersInGame;

    // Use this for initialization
    void Start()
    {
        shootersInGame = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (StateManager.m_instance.m_currentState)
        {
            case StateManager.State.MENU:
                {
                    break;
                }
            case StateManager.State.PAUSE:
                {
                    break;
                }
            case StateManager.State.PLAY:
                {
                    if(shootersInGame == false)
                    {
                        SpawnShooters();
                    }
                    break;
                }
            case StateManager.State.RESET:
                {
                    if(shootersInGame == true)
                    {
                        DespawnShooters();
                    }

                    break;
                }
        }
    }

    void SpawnShooters()
    {
        m_rightShooter = Instantiate(sideShooterRef);
        m_leftShooter = Instantiate(sideShooterRef);
        m_upShooter = Instantiate(sideShooterRef);
        m_bottomShooter = Instantiate(sideShooterRef);

        m_rightShooter.GetComponent<SideSpawnerScript>().SetSide(0);
        m_leftShooter.GetComponent<SideSpawnerScript>().SetSide(2);
        m_bottomShooter.GetComponent<SideSpawnerScript>().SetSide(1);
        m_upShooter.GetComponent<SideSpawnerScript>().SetSide(3);

        shootersInGame = true;
    }

    void DespawnShooters()
    {
        Destroy(m_rightShooter);
        Destroy(m_leftShooter);
        Destroy(m_bottomShooter);
        Destroy(m_upShooter);

        shootersInGame = false;
    }
}
