using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FICraftBuilding{

    public Camera cam;
    public GameObject building;

    GameObject ghostBuilding = null;

    public void Update()
    {
        Vector3? survacePosition = getSurvacePosition();
        if(survacePosition != null )
        {
            if (ghostBuilding == null)
                ghostBuilding = MonoBehaviour.Instantiate(building, (Vector3)survacePosition, Quaternion.identity);
            else
                ghostBuilding.transform.position = (Vector3)survacePosition;
        }
        else
        {
            MonoBehaviour.Destroy(ghostBuilding);
        }
    }

    Vector3? getSurvacePosition()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100, layerMask))
        {
            Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            return hit.point;
        }
        else
        {
            Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            return null;
        }

        
    }

}
