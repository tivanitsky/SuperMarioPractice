using UnityEngine;
using System.Collections;

public class BazookaShoot : MonoBehaviour {

	private Animator m_Animator;

	// Use this for initialization
	void Awake () {
		m_Animator = transform.gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Submit")) {
						m_Animator.SetBool ("Shoot", true);
			
				}
	}
}
