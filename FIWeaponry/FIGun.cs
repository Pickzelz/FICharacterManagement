﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FIGun : FIWeapon{

    public enum StateMachine { STATE_IDLE, STATE_SHOOTING, STATE_WAITING_NEXT_SHOOT, STATE_FINISH_SHOOT, STATE_COUNT }

    [Header("Gun Object")]
    public ParticleSystem gunParticle;
    public bool isUseTrajectory = false;
    public GameObject bulletPrefabs;
    public GameObject bulletPlaceholder;
    public Camera camera;

    [Header("Gun Spec")]
    public float damage = 10f;
    public float gunSpeedRate = 5f;
    public float bulletSpeed;
    public float maxRange;
    public bool isAutoGun;
    public Vector3 recoilRatePosition;

    bool m_isReadyForShoot = true;
    bool m_isShooting = false;
    StateMachine currentState = StateMachine.STATE_IDLE;
    Vector3 currentPosition = Vector3.zero;

    float t_time = 0;

    void Start()
    {
        currentPosition = transform.localPosition;
    }

	public void Attack()
    {
        Debug.Log("attack");
        if(currentState == StateMachine.STATE_IDLE)
        {
            currentState = StateMachine.STATE_SHOOTING;
        }
    }

    public void PrepareForNewShoot()
    {
        currentState = StateMachine.STATE_IDLE;
    }

    private void FixedUpdate()
    {
        StateMachineUpdate();
        if(Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
        else if(Input.GetButtonUp("Fire1"))
        {
            PrepareForNewShoot();
        }
    }

    public void StateMachineUpdate()
    {
        if(transform.localPosition != currentPosition)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, currentPosition, 0.1f);
        }
        switch(currentState)
        {
            case StateMachine.STATE_IDLE:
                break;
            case StateMachine.STATE_SHOOTING:
                if (isUseTrajectory)
                    ShootWithTrajectory();
                else
                    ShootWithRaycast();

                if(currentState != StateMachine.STATE_IDLE)
                    currentState = StateMachine.STATE_WAITING_NEXT_SHOOT;

                break;
            case StateMachine.STATE_WAITING_NEXT_SHOOT:
                
                if(isAutoGun)
                {
                    if(t_time >= (gunSpeedRate/1000))
                    {
                        t_time = 0;

                        if (currentState != StateMachine.STATE_IDLE)
                            currentState = StateMachine.STATE_SHOOTING;
                    }
                    else
                    {
                        t_time += Time.deltaTime;
                    }
                }
                else
                {
                    if (currentState != StateMachine.STATE_IDLE)
                        currentState = StateMachine.STATE_FINISH_SHOOT;
                }
                break;
        }

    }

    private void Recoil()
    {
        Vector3 recoilPos = transform.localPosition - recoilRatePosition;
        transform.localPosition = recoilPos;
    }

    private void ShootWithTrajectory()
    {
        gunParticle.Play();

        GameObject bulletO = MonoBehaviour.Instantiate(bulletPrefabs, bulletPlaceholder.transform.position, bulletPlaceholder.transform.rotation);
        bulletO.GetComponent<FIbullet>().cam = camera;
        
    }

    private void ShootWithRaycast()
    {
        gunParticle.Play();
        Recoil();
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, maxRange))
        {
            Debug.DrawRay(camera.transform.position, camera.transform.forward * hit.distance, Color.red);
            GameObject bulletO = MonoBehaviour.Instantiate(bulletPrefabs, bulletPlaceholder.transform.position, bulletPlaceholder.transform.rotation);
            bulletO.SetActive(true);
        }

    }
    

}
