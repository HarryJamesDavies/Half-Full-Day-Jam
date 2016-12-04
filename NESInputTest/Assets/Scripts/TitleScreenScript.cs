using UnityEngine;
using System.Collections;

public class TitleScreenScript : MonoBehaviour {

    [SerializeField]
    TextMesh m_attachedText;

    [SerializeField]
    bool growing;

	// Use this for initialization
	void Start () {
        m_attachedText = gameObject.GetComponent<TextMesh>();
        growing = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (growing == true)
        {
            m_attachedText.characterSize = m_attachedText.characterSize + 0.02f;
        }
        else if (growing == false)
        {
            m_attachedText.characterSize = m_attachedText.characterSize - 0.02f;
        }

        if(m_attachedText.characterSize > 1.6f)
        {
            growing = false;
        }
        else if(m_attachedText.characterSize < 0.8f)
        {
            growing = true;
        }
    }
}
