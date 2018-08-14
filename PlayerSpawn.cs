using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour {

    public GameObject PlayerPrefabs;

	// Use this for initialization
	void Start () {
        Instantiate(PlayerPrefabs, transform.position, transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
