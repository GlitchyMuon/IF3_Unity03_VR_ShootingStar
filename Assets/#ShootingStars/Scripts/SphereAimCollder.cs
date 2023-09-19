using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereAimCollder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnColliderEnter(Collision collision)
    {
        if (collision.gameObject.tag == "StarToSky")
        {
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
