using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIbullet : MonoBehaviour {

    private enum E_STATE { IDLE, MOVING, HIT, E_STATE_COUNT}

    public float bulletSpeed;
    public ParticleSystem blow;

    private E_STATE state;
    private E_STATE previousState;
    private Vector3 bulletVelocity = Vector3.zero;
    [HideInInspector] public Camera cam;
    [HideInInspector] public bool IsTrajectoryBullet;

    private void Start()
    {
        bulletVelocity = transform.forward * bulletSpeed;
        state = E_STATE.IDLE;
    }

    private void FixedUpdate()
    {
        StateMachine();
        if(IsTrajectoryBullet)
        {
            TrajectoryShoot();
        }
        if(state == E_STATE.HIT && !blow.IsAlive())
        {
            Destroy(gameObject);
        }
    }

    private void StateMachine()
    {
        if (previousState == state)
            return;

        switch(state)
        {
            case E_STATE.IDLE:
                break;
            case E_STATE.MOVING:
                break;
            case E_STATE.HIT:
                break;
        }
        previousState = state;
    }

    private void TrajectoryShoot()
    {
        Vector3 firstPosition = transform.position;
        float stepSize = 1.0f / 20.0f;
        for (float step = 0; step < 1; step += stepSize)
        {
            bulletVelocity += Physics.gravity * stepSize * Time.deltaTime;
            Vector3 nextPosition = firstPosition + bulletVelocity * stepSize * Time.deltaTime;

            Ray ray = new Ray(firstPosition, nextPosition - firstPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, (nextPosition - firstPosition).magnitude))
            {
                Blow(hit);
                //if(!blow.isPlaying)
                //{
                //    Destroy(gameObject);
                //}
                break;
            }

            firstPosition = nextPosition;
        }
        transform.position = firstPosition;
    }

    public void Blow(RaycastHit hit)
    {
        gameObject.SetActive(false);
        blow.transform.position = hit.point;
        blow.gameObject.SetActive(true);
        blow.Play();
    }

    private void OnDrawGizmos()
    {
    }
}
