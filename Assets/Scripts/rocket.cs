using UnityEngine;
using System.Collections;

public class rocket : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter2D(Collider2D other){
		//Проверка тега
		if (other.gameObject.tag == "DestroyRocket") {
			//Изменим направление монстра
			Destroy(this.gameObject);//Дестрой рокеты при вылете за пределы
		}
	}
}
