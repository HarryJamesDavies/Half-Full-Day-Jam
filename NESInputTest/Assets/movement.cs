using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetButtonDown("A(NES)"))
        {
            this.transform.position += new Vector3(0.0f, 1.0f, 0.0f);
        }
        else if (Input.GetButtonDown("B(NES)"))
        {
            this.transform.position += new Vector3(1.0f, 0.0f, 0.0f);
        }
        else if (Input.GetButtonDown("Select(NES)"))
        {
            this.transform.position += new Vector3(0.0f, -1.0f, 0.0f);
        }
        else if (Input.GetButtonDown("Start(NES)"))
        {
            this.transform.position += new Vector3(-1.0f, 0.0f, 0.0f);
        }

        transform.position += new Vector3(Input.GetAxis("Horizontal(NES)") * 0.1f, Input.GetAxis("Vertical(NES)") * -0.1f, 0.0f);
	}
}
