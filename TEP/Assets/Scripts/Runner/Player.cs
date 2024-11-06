
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private string inputNameHorizontal;

    private Rigidbody rb;

    private float inputHorizontal;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        inputHorizontal = Input.GetAxisRaw(inputNameHorizontal);
        transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(inputHorizontal * speed * Time.fixedDeltaTime, rb.linearVelocity.y, 0);
    }
}
