using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Camera cam;
    CharacterController controller;

    [SerializeField] float speed = 1;
    float refSmooth;
    [SerializeField] float smoothValue = 2;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        Debug.Log(isGrounded); 

        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            }
            else
            {
                velocity.y = -9.81f;
            }


        }

        Debug.Log(velocity.y);


        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");


        Vector3 direction = new Vector3(horizontal, 0, vertical);

        if (direction.magnitude > 0)
        {
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref refSmooth, smoothValue);

            Vector3 moveDirection = Quaternion.Euler(0, angle, 0) * Vector3.forward;

            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);

            controller.Move(moveDirection * Time.deltaTime * speed);


        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
}
