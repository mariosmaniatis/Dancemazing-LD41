using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMonster : MonoBehaviour {

    public GameObject monster;
	// Update is called once per frame
	void FixedUpdate () {
        gameObject.transform.position = monster.transform.position;
	}
}
