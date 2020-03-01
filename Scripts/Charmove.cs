using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charmove : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 10f;
    //public float yaw = 0f;
    Vector3 velocity;

    public AudioSource aSource;
    public AudioClip[] clipArray;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        aSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //yaw += 2.0f * Input.GetAxis("Mouse X");
        //transform.eulerAngles = new Vector3(0f, yaw, 0f);
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (controller.velocity.magnitude > 2f && aSource.isPlaying == false)
        {
            aSource.clip = clipArray[Random.Range(0, clipArray.Length)];
            aSource.PlayOneShot(aSource.clip);
        }
      
    }
}
