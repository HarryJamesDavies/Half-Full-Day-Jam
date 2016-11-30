using UnityEngine;
using System.Collections;

public class ShooterManager : MonoBehaviour {

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

    // Use this for initialization
    void Start () {

        m_rightShooter = Instantiate(sideShooterRef);
        m_leftShooter = Instantiate(sideShooterRef);
        m_upShooter = Instantiate(sideShooterRef);
        m_bottomShooter = Instantiate(sideShooterRef);

        m_rightShooter.GetComponent<SideSpawnerScript>().SetSide(0);
        m_leftShooter.GetComponent<SideSpawnerScript>().SetSide(2);
        m_bottomShooter.GetComponent<SideSpawnerScript>().SetSide(1);
        m_upShooter.GetComponent<SideSpawnerScript>().SetSide(3);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
