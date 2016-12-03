using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    public Vector3 m_rotationVector;
    public bool m_rotate = true;
	
	// Update is called once per frame
	void Update ()
    {
	    if(m_rotate)
        {
            transform.Rotate(m_rotationVector);
        }
	}
}
