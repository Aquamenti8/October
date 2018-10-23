using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritRadar : MonoBehaviour {

    public GameObject spirit;
    public SpiritBehaviour behaviour;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && behaviour.state == 1)
        {
            behaviour.MoveBush();
        }

        if(collision.CompareTag("Collectable") && behaviour.state == 1)
        {
            behaviour.OutBush(collision.gameObject);
            
        }

        if (collision.CompareTag("Collectable") && behaviour.state == 5)
        {
            behaviour.state = 2;
            behaviour.target = collision.gameObject;
            behaviour.gameObject.GetComponent<Animator>().SetBool("run", true);
            behaviour.gameObject.GetComponent<Animator>().SetBool("lost", false);
        }
    }
}
