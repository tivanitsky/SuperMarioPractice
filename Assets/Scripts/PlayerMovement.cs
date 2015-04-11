using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private Animator m_Animator;
	public float JumpSpeed = 0.1f;
	private Transform m_GroundCheck;
	//Позволяет выбрать из слоев преднастроенных
	public LayerMask GroundLayer;//LayerMask allow you to display the LayerMask popup menu in the inspector.
	private bool IsDead;
	public int HighScore;
	public bool facingRight;

	// Use this for initialization
	void Start () {
		m_Animator 		= GetComponent<Animator> ();//Обращение к компоненту объекта к котрому прикреплен скрипт в качестве компонента
		m_GroundCheck	= transform.FindChild("GroundCheck");
		IsDead 			= false;
		facingRight 	= true;	
	}

	void OnTriggerEnter2D(Collider2D other){
		//Проверка тега
		if (other.gameObject.tag == "Fall" || other.gameObject.tag == "Falling") {
			//Изменим направление монстра
			IsDead	= true;
			
			//Анимация смэрти
			m_Animator.SetBool("IsDead", IsDead);
			Application.LoadLevel (Application.loadedLevelName);//Последний загруженный уровень
		} else if (other.gameObject.tag == "LevelEnd") {
			//Загрузка след уровня
			Application.LoadLevel ("scene0");
		}
	}

	//Столкновение с монетой
	void OnCollisionEnter2D(Collision2D other){
				//Проверка тега
				if (other.gameObject.tag == "Coin") {
				HighScore = PlayerPrefs.GetInt ("HighScore", 0);
				PlayerPrefs.SetInt("HighScore", ++HighScore);
				}
			else if (other.gameObject.tag == "Monster") {

			//IsDead	= true;

			//Анимация смэрти
			//m_Animator.SetBool("IsDead", IsDead);

		}
	}

	void FixedUpdate () {
		float hSpeed = Input.GetAxis ("Horizontal");//Получение горизонтальной скорости при обновлении физики
		
		m_Animator.SetFloat ("Speed", Mathf.Abs (hSpeed));//Назначим параметр (по модулю) аниматора (который мы задавали для транзишнов)	


		//m_Animator.SetBool ("Fire", false);


		//на земле ли мы (используем объект под марио)
		bool IsGrounded = Physics2D.OverlapPoint(m_GroundCheck.position, GroundLayer);//Пересекаются ли

		if (IsDead) {
			//Прыжочек
			this.rigidbody2D.AddForce(Vector2.up * JumpSpeed, ForceMode2D.Impulse);
			
			//yield return new WaitForSeconds(5);//Перед переключение уровня задержечка

			IsDead = false;

			Application.LoadLevel (Application.loadedLevel);

		}

		if (Input.GetButton ("Jump")) {
			m_Animator.SetTrigger ("Jumped");//Слой анимации базуки
						//Прыгать нужно только тогда когда марио на земле а не каждый раз при обновлении кадра (см событие fixed update)
						//Иначе его будет дрючь этим импульсом до посинения (пока 24 кадра не отрисуется)
						if (IsGrounded) {
								this.rigidbody2D.AddForce (Vector2.up * JumpSpeed, ForceMode2D.Impulse);
								IsGrounded = false;			
						}

				} 


		m_Animator.SetBool("IsGrounded", IsGrounded);//Для перехода от одной анимации к другой см аниматор

		//Соответственно это переключает анимацию с одного клипа на другой	
		//Скорость смены кадров анимации настроивается соотвественно в анимации поле Sample

		//*********************Направление движения**************************
		if (hSpeed > 0) {
			//Вправо бежит
			// Widen the object by 0.1
			//transform.localScale += new Vector3(0.1F, 0, 0);
			transform.localScale = new Vector3 (1, 1, 1);
			facingRight			 = true;	
//				Vector3
//					Description
//					
//					Representation of 3D vectors and points.
//					
//					This structure is used throughout Unity to pass 3D positions and directions around. It also contains functions for doing common vector operations.

		} else if (hSpeed < 0) {
			transform.localScale = new Vector3 (-1, 1, 1);
			facingRight			 = false;	
			//			
		}

		//*********************Движение*************************************
		//Скорость твердого тела, префикс 2 у вектора говорит о количестве измерений
		this.rigidbody2D.velocity = new Vector2(hSpeed, this.rigidbody2D.velocity.y);

	}
}
