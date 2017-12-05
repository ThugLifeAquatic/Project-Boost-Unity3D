using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    [SerializeField]
    float rcsThrust = 100f;
    [SerializeField]
    float mainThrust = 1000f;


    Rigidbody rigidBody;
    AudioSource audioSource;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        AudioSwitcher(Thrust(), Rotate());
	}

    private bool Rotate()
    {
        rigidBody.freezeRotation = true;

        float rotationMultiplier = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.back * rotationMultiplier);
            return true;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.forward * rotationMultiplier);
            return true;
        }

        rigidBody.freezeRotation = false;
        return false;
    }

    private bool Thrust()
    {
        float thrustMultiplier = mainThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustMultiplier);
            return true;
        }
        return false;
    }

    private void AudioSwitcher(bool thrusting, bool rotating)
    {
        if (thrusting == true && rotating == true)
        {
            audioSource.pitch = 2;
        }
        else if (thrusting == true && rotating == false)
        {
            audioSource.pitch = 1;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
