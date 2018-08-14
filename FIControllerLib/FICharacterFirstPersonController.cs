using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FICharacterFirstPersonController:FICharacter{

    [Header("Character Object")]
    public Camera cam;

    [Header("Character Status")]
    public float JumpPower;
    public float GroundDistance;

    [Header("Options")]
    public float speed = 5f;
    public float sensitivityX;
    public float sensitivityY;
    public float MinAngle;
    public float MaxAngle;

    //private
    float rotateXAccumulation;
    IAttack attack;
    object method;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rotateXAccumulation = 0;
    }

    private void FixedUpdate()
    {
        Move();
        if(Input.GetButton("Jump"))
        {
            Jump();
        }
    }

    void Move()
    {
        MoveCharacter();
        CameraRotationBasedOnMouse();
    }

    void Jump()
    {
        Debug.Log("Jump");
        Vector3 checkGround = transform.TransformDirection(Vector3.down);
        if (Physics.Raycast(transform.position, checkGround, 0.5f))
        {
            rb.AddForce(transform.up * JumpPower, ForceMode.Impulse);
        }
    }

    void MoveCharacter()
    {
        Vector3 currentPosition = transform.position;
        Vector3 _moveForward = transform.forward * Input.GetAxisRaw("Vertical");
        Vector3 _moveSide = transform.right * Input.GetAxisRaw("Horizontal");
        Vector3 _move = (_moveForward.normalized + _moveSide).normalized * speed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + _move);
    }

    void CameraRotationBasedOnMouse()
    {
        float rotateX = Input.GetAxisRaw("Mouse Y");
        float nextRotationAccumulation = rotateXAccumulation + rotateX * -1 * sensitivityX;
        if (nextRotationAccumulation < MinAngle && nextRotationAccumulation > MaxAngle * -1)
        {
            rotateXAccumulation = nextRotationAccumulation ;
            cam.transform.Rotate(new Vector3(rotateX * -1, 0, 0) * sensitivityX);
        }
        float rotateY = Input.GetAxisRaw("Mouse X");
        rb.MoveRotation(rb.rotation * Quaternion.Euler(new Vector3(0, rotateY, 0) * sensitivityY));
    }

    
}
