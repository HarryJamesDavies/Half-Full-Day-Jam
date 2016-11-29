using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        switch (gameObject.tag)
        {
            case "Player1":
                if (Input.GetButtonDown("P1-A(NES)"))
                {
                    transform.position += new Vector3(0.0f, 1.0f, 0.0f);
                    Debug.Log("Player 1 Press A");
                }
                else if (Input.GetButtonDown("P1-B(NES)"))
                {
                    transform.position += new Vector3(1.0f, 0.0f, 0.0f);
                    Debug.Log("Player 1 Press B");
                }
                else if (Input.GetButtonDown("P1-Select(NES)"))
                {
                    transform.position += new Vector3(0.0f, -1.0f, 0.0f);
                }
                else if (Input.GetButtonDown("P1-Start(NES)"))
                {
                    transform.position += new Vector3(-1.0f, 0.0f, 0.0f);
                }

                transform.position += new Vector3(Input.GetAxis("P1-Horizontal(NES)") * 0.1f, Input.GetAxis("P1-Vertical(NES)") * -0.1f, 0.0f);
                break;
            case "Player2":
                if (Input.GetButtonDown("P2-A(NES)"))
                {
                    transform.position += new Vector3(0.0f, 1.0f, 0.0f);
                    Debug.Log("Player 2 Press A");
                }
                else if (Input.GetButtonDown("P2-B(NES)"))
                {
                    transform.position += new Vector3(1.0f, 0.0f, 0.0f);
                    Debug.Log("Player 2 Press B");
                }
                else if (Input.GetButtonDown("P2-Select(NES)"))
                {
                    transform.position += new Vector3(0.0f, -1.0f, 0.0f);
                }
                else if (Input.GetButtonDown("P2-Start(NES)"))
                {
                    transform.position += new Vector3(-1.0f, 0.0f, 0.0f);
                }

                transform.position += new Vector3(Input.GetAxis("P2-Horizontal(NES)") * 0.1f, Input.GetAxis("P2-Vertical(NES)") * -0.1f, 0.0f);
                break;
            case "Player3":
                if (Input.GetButtonDown("P3-A(NES)"))
                {
                    transform.position += new Vector3(0.0f, 1.0f, 0.0f);
                }
                else if (Input.GetButtonDown("P3-B(NES)"))
                {
                    transform.position += new Vector3(1.0f, 0.0f, 0.0f);
                }
                else if (Input.GetButtonDown("P3-Select(NES)"))
                {
                    transform.position += new Vector3(0.0f, -1.0f, 0.0f);
                }
                else if (Input.GetButtonDown("P3-Start(NES)"))
                {
                    transform.position += new Vector3(-1.0f, 0.0f, 0.0f);
                }

                transform.position += new Vector3(Input.GetAxis("P3-Horizontal(NES)") * 0.1f, Input.GetAxis("P3-Vertical(NES)") * -0.1f, 0.0f);
                break;
            case "Player4":
                if (Input.GetButtonDown("P4-A(NES)"))
                {
                    transform.position += new Vector3(0.0f, 1.0f, 0.0f);
                }
                else if (Input.GetButtonDown("P4-B(NES)"))
                {
                    transform.position += new Vector3(1.0f, 0.0f, 0.0f);
                }
                else if (Input.GetButtonDown("P4-Select(NES)"))
                {
                    transform.position += new Vector3(0.0f, -1.0f, 0.0f);
                }
                else if (Input.GetButtonDown("P4-Start(NES)"))
                {
                    transform.position += new Vector3(-1.0f, 0.0f, 0.0f);
                }

                transform.position += new Vector3(Input.GetAxis("P4-Horizontal(NES)") * 0.1f, Input.GetAxis("P4-Vertical(NES)") * -0.1f, 0.0f);
                break;
            default:
                break;
        }
	}
}
