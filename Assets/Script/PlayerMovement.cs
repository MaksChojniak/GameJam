using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    [Header("Parameters")]
    public GameObject playerObject;
    public GameObject cameraObject;

    [Space(20)]
    [Header("Ground")]
    [SerializeField] Transform _groundDetector;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] bool _isGrounded;
    float _groundDistance = 0.4f;

    float _mouseSens = 2;
    float _moveSens = 10;
    float _jumpForce = 10;

    float xRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        float xAxis = Input.GetAxisRaw("Horizontal") * _moveSens *  Time.deltaTime;
        float zAxis = Input.GetAxisRaw("Vertical") * _moveSens * Time.deltaTime;

        float xMouse = Input.GetAxisRaw("Mouse X") * _mouseSens;
        float yMouse = Input.GetAxisRaw("Mouse Y") * _mouseSens;

        float jump = Input.GetKeyDown(KeyCode.Space) ? 30 : 0;

        PlayerMove(zAxis, xAxis, Vector3.up * _jumpForce * jump);
        CameraMove(xMouse, yMouse);

        GroundDetector();
    }

    void GroundDetector()
    {
        _isGrounded = Physics.CheckSphere(_groundDetector.position, _groundDistance, _groundLayer);
    }

    void PlayerMove(float forward, float right, Vector3 up)
    {
        Vector3 forwardDir = Vector3.forward * forward;
        Vector3 rightDir = Vector3.right * right;

        playerObject.transform.Translate(forwardDir);
        playerObject.transform.Translate(rightDir);

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            playerObject.GetComponent<Rigidbody>().AddForce(up);
    }

    void CameraMove(float mouseX, float mouseY)
    {
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        cameraObject.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerObject.transform.Rotate(Vector3.up * mouseX);
    }


}
