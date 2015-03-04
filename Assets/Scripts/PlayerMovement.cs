using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private Animator m_Animator;

	// Use this for initialization
	void Start () {
		m_Animator = GetComponent<Animator> ();//Обращение к компоненту объекта к котрому прикреплен скрипт в качестве компонента
	}
	
	void FixedUpdate () {
		float hSpeed = Input.GetAxis ("Horizontal");//Получение горизонтальной скорости при обновлении физики

		m_Animator.SetFloat ("Speed", Mathf.Abs (hSpeed));//Назначим параметр (по модулю) аниматора (который мы задавали для транзишнов)	
		//Соответственно это переключает анимацию с одного клипа на другой	
		//Скорость смены кадров анимации настроивается соотвественно в анимации поле Sample

		if (hSpeed > 0) {
			//Вправо бежит
			// Widen the object by 0.1
			//transform.localScale += new Vector3(0.1F, 0, 0);
			transform.localScale = new Vector3 (1, 1, 1);
				
//				Vector3
//					Description
//					
//					Representation of 3D vectors and points.
//					
//					This structure is used throughout Unity to pass 3D positions and directions around. It also contains functions for doing common vector operations.

		} else if (hSpeed < 0) {
			transform.localScale = new Vector3 (-1, 1, 1);
		}
	}
}
