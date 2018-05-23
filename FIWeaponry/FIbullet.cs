using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIbullet : MonoBehaviour {

    public float bulletSpeed;
    public float stepPerFrame;

    private Vector3 bulletVelocity = Vector3.zero;

    public GameObject bulletObject;
    public ParticleSystem blow;
    [HideInInspector] public Camera cam;
    [HideInInspector] public bool IsTrajectoryBullet;

    private void Start()
    {
        bulletVelocity = transform.forward * bulletSpeed;
    }

    private void Update()
    {
        if(IsTrajectoryBullet)
        {
            TrajectoryShoot();
        }
        if(!blow.IsAlive())
        {
            Destroy(gameObject);
        }
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
        bulletObject.SetActive(false);
        blow.transform.position = hit.point;
        blow.gameObject.SetActive(true);
        blow.Play();
    }

    private void OnDrawGizmos()
    {
    }
}
