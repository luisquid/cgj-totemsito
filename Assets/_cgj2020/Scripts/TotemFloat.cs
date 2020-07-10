using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemFloat : MonoBehaviour
{
    float newY;
    [SerializeField]
    private float speed = 1f;

    public float maxSpeed = 9f;
    public float minAmplitude = 0.001f;
    public float maxAmplitude = 0.009f;

    [SerializeField]
    private float amplitude;

    private void Start()
    {
        amplitude = Random.Range(minAmplitude, maxAmplitude);
        speed = Random.Range(4f, maxSpeed);
    }

    void Update()
    {
        // Save the y position prior to start floating (maybe in the Start function):
        newY = transform.position.y;

        // Put the floating movement in the Update function:
        transform.position = new Vector3(transform.position.x, (newY + amplitude * Mathf.Sin(speed * Time.time)), transform.position.z);
    }
}
