using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CineCutChange : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Cine/CineSmooth")
        {
            collision.transform.GetChild(0).GetComponent<CinemachineVirtualCamera>().Priority = 100;
            Camera.main.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = 2;
        }
        if (collision.gameObject.tag == "Cine/CineCut")
        {
            //collision.transform.GetChild(0).GetComponent<CinemachineVirtualCamera>().Priority = 100;
            Camera.main.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cine/CineSmooth")
        {
            collision.transform.GetChild(0).GetComponent<CinemachineVirtualCamera>().Priority = 0;
        }
        if (collision.gameObject.tag == "Cine/CineCut")
        {
            collision.transform.GetChild(0).GetComponent<CinemachineVirtualCamera>().Priority = 0;
        }
    }
}
