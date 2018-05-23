using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FICharacterFirstPersonController{

    [Header("Character Object")]
    public GameObject characterObject;
    public Camera cam;

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

    public void Init(GameObject charObject)
    {
        characterObject = charObject;
        rb = characterObject.GetComponent<Rigidbody>();
        rotateXAccumulation = 0;
    }

    public void Move()
    {
        MoveCharacter();
        CameraRotationBasedOnMouse();
    }

    void MoveCharacter()
    {
        Vector3 currentPosition = characterObject.transform.position;
        Vector3 _moveForward = characterObject.transform.forward * Input.GetAxisRaw("Vertical");
        Vector3 _moveSide = characterObject.transform.right * Input.GetAxisRaw("Horizontal");
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
