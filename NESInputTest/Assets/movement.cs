using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	
	void Update ()
    {
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
	}

    void PlayerMovement(int _controller)
    {
        if (Input.GetButtonDown("P" + _controller + "-A(NES)"))
        {
            transform.position += new Vector3(0.0f, 1.0f, 0.0f);
            Debug.Log("Player 1 Press A");
        }
        else if (Input.GetButtonDown("P" + _controller + "-B(NES)"))
        {
            transform.position += new Vector3(1.0f, 0.0f, 0.0f);
            Debug.Log("Player 1 Press B");
        }
        else if (Input.GetButtonDown("P" + _controller + "-Select(NES)"))
        {
            transform.position += new Vector3(0.0f, -1.0f, 0.0f);
        }
        else if (Input.GetButtonDown("P" + _controller + "-Start(NES)"))
        {
            transform.position += new Vector3(-1.0f, 0.0f, 0.0f);
        }

        transform.position += new Vector3(Input.GetAxis("P" + _controller + "-Horizontal(NES)") * 0.1f, Input.GetAxis("P" + _controller + "-Vertical(NES)") * -0.1f, 0.0f);
    }
}
