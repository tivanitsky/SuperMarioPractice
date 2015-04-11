using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	//Переменные
	public float repeatDamagePeriod = 1.0f; //Период повторения дамажа (после дамажа марио некоторое время неуязвим) в секундах
	public float hurthForce = 2f; //Отталкивается во время коцки марио
	public float damageAmount = 5f;//Отнимается за одну коцку
	public float health = 100f;//Ждоровье

	private Animator anim;
	private bool alive 					= true;
	private float lastHitTime = 0f;
	//private SpriteRenderer healthBar;
	private GameObject healthBar;
	private Renderer healthBarRenderer;

	//Инициализация дефолтов
	void Start () {
		anim 		= GetComponent<Animator> ();
		healthBarRenderer   = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<SpriteRenderer> ();
		healthBar   = GameObject.FindGameObjectWithTag("HealthBar");
		lastHitTime = Time.time;

	}

	// Пересечение с коллайдером каким нибудь
	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Monster") {
			//Что бы коцало только через определенный период, а не непрерывно
			if (Time.time > (lastHitTime + repeatDamagePeriod)) {
				if (health > 0f) {
					//Причиним боль
					TakeDamage(col.transform);
					lastHitTime = Time.time;//Последний раз когда коцнули
				} else {
					//Шмерть
					Die();				
				}
			}
		}
	}
	
	// Причинить боль игроку
	void TakeDamage (Transform enemy) {
		//Нельзя прыгать когда тебя коцает
		anim.SetBool("IsGrounded", false);

		//Оттолкнем марио от врага
		Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 5f;
		rigidbody2D.AddForce (hurtVector * hurthForce);

		//Отнимем жисть на 10 единиц
		health -= damageAmount;

		//Обновим шкалу жизни у марио
		UpdateHealthBar();

	}

	//Обновим шкалу жизни у марио
	void UpdateHealthBar(){
		//Пропорции красного и зеленого цвета хелсбара в зависимости от здоровья марио
		healthBarRenderer.material.color = Color.Lerp (Color.green, Color.red, 1 - health * 0.01f);

		//Ресайзним хелсбар пропорционально здоровью
		healthBar.transform.localScale += new Vector3 (healthBar.transform.localScale.x * -0.1F, 0, 0);
		//healthBar.transform.localScale = new Vector3 (5 * health * 0.01f, 1, 1);
	}

	//Шмерть
	public void Die(){
		//Если живчик
		if (alive) {
			alive = false;

			//Сделаем все коллайдеры игрока триггерами что бы он падал
			Collider2D[] cols = GetComponents<Collider2D>();
			foreach(Collider2D c in cols ){
				c.isTrigger = true;
			}

			//Анимируем его немного
			anim.SetBool("IsDead", true);

			//Сделаем хелс бар неактивным
			GameObject.FindGameObjectWithTag("HealthBar").SetActive(false);

			//Дизейбл скрипта плеер контрола что бы он не бегал там
			GetComponent<PlayerMovement>().enabled = false;

			//Отключим и ган скрипт что бы не стрелял там
			GetComponentInChildren<Gun>().enabled = false;
		}
	}
}
