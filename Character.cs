using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public FICharacterFirstPersonController control;
    public FIGun gun;

    //private
    private Vector3 bulletVelocity;

	// Use this for initialization
	void Start () {
        control.Init(gameObject);
        gun.Init();
	}
	
	// Update is called once per frame
	void Update () {
        control.Move();
        //crafter.Update();
        gun.Update();

        if(Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Shoot");
            gun.Attack();
        }
        else if(Input.GetButtonUp("Fire1"))
        {
            gun.PrepareForNewShoot();
        }
        //bulletVelocity = gun.bulletPlaceholder.transform.forward * 100f;
        //Vector3 firstPosition = gun.bulletPlaceholder.transform.position;
        //Vector3 pBulletVelocity = bulletVelocity;

        //float stepSize = 1.0f / 20.0f;
        //for (float step = 0; step < 1; step += stepSize)
        //{
        //    pBulletVelocity += Physics.gravity * stepSize;
        //    Vector3 nextPosition = firstPosition + pBulletVelocity * stepSize;
        //    Debug.DrawLine(firstPosition, nextPosition, Color.red);
        //    firstPosition = nextPosition;
        //}
    }

    private void OnDrawGizmos()
    {

        

    }
}
