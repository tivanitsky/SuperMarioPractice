using UnityEngine;
using System.Collections;

//Скрипт который заставляет хелс бар следовать за плеером
public class FallOfPlayer : MonoBehaviour {
	public Vector3 offset; //Отступ от плеера

	private Transform player;//Ссылка на игрока за которым надо следовать

	// Use this for initialization
	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = player.position + offset;
	}
}
