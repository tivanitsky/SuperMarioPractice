using UnityEngine;
using System.Collections;

//Класс настроек не наследуется от монобехавиоур
public class Settings{
	private AudioSource m_MusicSource;
	private AudioSource m_SoundSource;

	public float MusicVolume{
		get{return m_MusicSource.volume;}
		set{m_MusicSource.volume = value;}
	}
	public float SoundVolume{
		get{return m_SoundSource.volume;}
		set{m_SoundSource.volume = value;}
	}//переменная с предопределенными геттерами и сеттерами

	public int HighScore { get; set;}

	//Загрузить настройки
	public void Load(AudioSource music, AudioSource sound){
				m_MusicSource = music;
				m_SoundSource = sound;

				SoundVolume = PlayerPrefs.GetFloat ("SoundVolume", 1.0f);	
				MusicVolume	= PlayerPrefs.GetFloat ("MusicVolume", 0.5f);
				HighScore	= PlayerPrefs.GetInt ("HighScore", 0);

		//desc of player prefs
//		Description
//			
//			Stores and accesses player preferences between game sessions.
	}//метод

	//Сохранить настройки
	public void Save(){
		PlayerPrefs.SetFloat ("SoundVolume", SoundVolume);
		PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
		PlayerPrefs.SetInt("HighScore", HighScore);
		//desc of player prefs
		//		Description
		//			
		//			Stores and accesses player preferences between game sessions.
	}//метод

}
