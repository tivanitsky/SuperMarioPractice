using UnityEngine;
using System.Collections;

public class CoinMovement : MonoBehaviour {
	public float CoinSpeed = 5f;
	//You can play a single audio clip using Play, Pause and Stop. You can also adjust its volume while playing using the volume property, or seek using time. Multiple sounds can be played on one AudioSource using PlayOneShot. You can play a clip at a static position in 3D space using PlayClipAtPoint.
	private AudioSource m_SoundSource;
	public AudioClip ClickSoud;//Клик типа аудиоклипа A container for audio data.

	void Awake(){
		// У камеры должен быть tag MainCamera
		m_SoundSource = Camera.main.transform.FindChild("Sound").GetComponent<AudioSource>();

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Монета бежит
		//rigidbody2D.velocity = new Vector2(CoinSpeed, rigidbody2D.velocity.y);	
	}

	//Sent when an incoming collider makes contact with this object's collider (2D physics only)
	//Событие столкновения с другим коллайдером (очень удобно использовать теги объектов)
	void OnCollisionEnter2D(Collision2D other){
		//Проверка тега
		if (other.gameObject.tag == "Player") {
			m_SoundSource.PlayOneShot(ClickSoud);
			Destroy(this.gameObject);//Дестрой монеты

		}
	}
}
