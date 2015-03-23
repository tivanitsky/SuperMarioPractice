using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	public Rigidbody2D rocket;//Укажем рокету
	public float speed = 20f;

	private Animator m_Animator;
	private PlayerMovement plMov;//Ссылка на другой скрипт

	// Use this for initialization
	void Awake () {
		m_Animator = transform.root.gameObject.GetComponent<Animator>();
		plMov	   = transform.root.gameObject.GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetButton ("Submit")) {
			m_Animator.SetTrigger("Fire");
			//m_Animator.SetBool("Fire", true);

			//todo после инициации объекта фелс
			if (plMov.facingRight) {
				//Порождаем рокету
				Rigidbody2D RocketInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
				RocketInstance.velocity = new Vector2(speed, 0);
				m_Animator.SetBool("Fire", false);			
			}
			else { 
				//Порождаем рокету в обратном направлении
				Rigidbody2D RocketInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
				RocketInstance.velocity = new Vector2(-speed, 0);
				m_Animator.SetBool("FIre", false);
			}
			//m_Animator.SetBool("Fire", false);
			//m_Animator.SetBool("Shoot", false);

		}
	}}

