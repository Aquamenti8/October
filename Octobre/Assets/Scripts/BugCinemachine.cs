using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BugCinemachine : MonoBehaviour {

    public float orthographicSize;

	
	// Update is called once per frame
	void Start() {
        GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = orthographicSize; //orthographicSize

	}
}
