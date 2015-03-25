using UnityEngine;
using System.Collections;

public class Monster_0_move : MonoBehaviour {
	public float Speed = 10.0f;//Настраиваемая скорость
	private float Direction = 1.0f;//Направление монстра
	public LayerMask PlayerLayer;//LayerMask allow you to display the LayerMask popup menu in the inspector.
	private Transform m_Boom;
	private bool IsDead;
	private Animator m_Animator;

	// Use this for initialization
	void Start () {
		m_Animator 		= GetComponent<Animator> ();//Обращение к компоненту объекта к котрому прикреплен скрипт в качестве компонента
		m_Boom			= transform.FindChild("Boom");
		IsDead 			= false;

	}

	// Update is called once per frame
	void FixedUpdate () {
		//Дали ли нам по шапке
		IsDead = Physics2D.OverlapPoint(m_Boom.position, PlayerLayer);//Пересекаются ли
		if (IsDead) {
			//Debug.Log("Is dead " + IsDead.ToString());
			//Анимация смэрти
			m_Animator.SetBool("IsDead", IsDead);		
			//System.Threading.Thread.Sleep(500000);
			Destroy(this.gameObject);
			//Destroy(this.gameObject, 3);//Дестрой монстра через 3.5 секунд
		}

		//Движение монстра с учетом направления
		rigidbody2D.velocity = new Vector2(Direction * Speed, rigidbody2D.velocity.y);
		transform.localScale = new Vector3 (Direction, 1, 1);// зеркально отобразим когда идет назад
	}

	void OnTriggerEnter2D(Collider2D other){
		//Проверка тега
		if (other.gameObject.tag == "GoBackMonster") {
			//Изменим направление монстра
			Direction = Direction * -1;
		}

		if (other.gameObject.tag == "rocket") {
			
			Destroy(this.gameObject);//Дестрой рокеты
			
		}
	}

	void OnCollisionEnter2D(Collision2D other){

	}


}
