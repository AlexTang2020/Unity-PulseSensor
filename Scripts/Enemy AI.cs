using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    float speed = 5f;
    bool isTracking = false;
    float yaw = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Move()
    {
        if (!isTracking)
        {

            this.GetComponent<Rigidbody>().velocity = this.transform.forward * speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
