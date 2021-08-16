using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float height;
    [SerializeField] private float sensitivity = 1.0f;
    private Transform cameraTransform;
    private Camera cam;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        cameraTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        currentX += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        currentY -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        Quaternion camRotation = Quaternion.Euler(currentY, currentX, 0);

        Vector3 cameraPos = new Vector3(0, height, -12);

        cameraTransform.position = player.position + camRotation * cameraPos;
        currentY = Mathf.Clamp(currentY, -20, 75);

        Vector3 heightAdjustedPos = player.position;
        heightAdjustedPos.y += 2.5f;

        cameraTransform.LookAt(heightAdjustedPos);

    }
}
