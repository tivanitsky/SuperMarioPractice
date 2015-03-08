using UnityEngine;
using System.Collections;

public class Monster_0_move : MonoBehaviour {
	public float Speed = 10.0f;//Настраиваемая скорость
	private float Direction = 1.0f;//Направление монстра

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
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
	}
}
