using UnityEngine;
using System.Collections;

//Перчисление типов меню
public enum MenuTypes : byte {
	MainMain  		= 0,
	OptionMenu  	= 1,
	PauseMenu  		= 2,
	GameOverMenu	= 3,
} 

public class NewBehaviourScript : MonoBehaviour {
    //Передается в качестве параметра в аудиосурс
    //Заполнять палик свойство нужно у объекта к которому прикреплен скрипт а не у самого скрипта
	public AudioClip ClickSoud;//Клик типа аудиоклипа A container for audio data.
    //Тк как он паблик то доступен для редактирования в юнити в инспекторе скрипта и можно загрузить туда аудио клип
	public bool IsMenuActive { get; set;}// Определяет активно меню или нет при старте системы = тру
	// Use this for initialization

	public MenuTypes ActiveMenu { get; set;}// Переменная типа перечисления

	private readonly GUI.WindowFunction[] MenuFunction = null;//Массив из функций меню (см параметр GUILayout.Window)

	private readonly string[] MenuNames = new string[]{ "Main menu", "Options menu", "Pause menu", "Game over menu",};
    
	private Settings m_settings = new Settings();//Класс который мы сами описали как скрипт отдельный, для сохранения настроек в котором между играми
	//используется PlayerPrefs см в нем описание
	//desc of player prefs
	//		Description
	//			
	//			Stores and accesses player preferences between game sessions.

	//An AudioSource is attached to a GameObject for playing back sounds in a 3D environment. In order to play 3D sounds you also need to have a AudioListener. The audio listener is normally attached to the camera you want to use. Whether sounds are played in 3D or 2D is determined by AudioImporter settings.
    //
    //You can play a single audio clip using Play, Pause and Stop. You can also adjust its volume while playing using the volume property, or seek using time. Multiple sounds can be played on one AudioSource using PlayOneShot. You can play a clip at a static position in 3D space using PlayClipAtPoint.
	private AudioSource m_SoundSource;

	//Конструктор
	public NewBehaviourScript()
	{
		//Инициируем массив возможных функций меню (функции с именами описанными в массиве должны быть определены в тексте скрипта)
		//На то это и специальный тип массивфункций окна
		MenuFunction = new GUI.WindowFunction[]{
			MainMenu  	,
			OptionsMenu  ,
			PauseMenu  	,
			GameOverMenu ,
		
		};

	}

	void Awake () {
		ActiveMenu = MenuTypes.MainMain;//При старте меню назначается дефолтный тип

		IsMenuActive = false;

		Application.runInBackground = true;

		//DontDestroyOnLoad(gameObject);

        //Ищет в объекте чайлда камеры(камера гейм обджектс - адд емпти чайлд обджект наименование Sound)
        //Адд компонент в инспекторе аудио -- аудио сурс
        // У камеры должен быть tag MainCamera
		m_SoundSource = Camera.main.transform.FindChild("Sound").GetComponent<AudioSource>();
		//Первыми параметрами передаются наши источники музыки и звука на камере (с компонентами соответствующими)
		//Music child можно копировать в Unity Ctrl + d в аудиосурсе проставить сразу песню и галку playonwake b loop для цикла
		m_settings.Load(Camera.main.transform.FindChild("Music").GetComponent<AudioSource>(), m_SoundSource);	
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Cancel")) {
			IsMenuActive = true;
		}
	}

	// Вызывается постоянно то же см дебаг сообщение после закрытия меню
	void OnGUI () {
		//Debug.Log ("I am on gui");
		const int Width = 400;
		const int Height = 300;

		m_settings.Load(Camera.main.transform.FindChild("Music").GetComponent<AudioSource>(), m_SoundSource);	

		//GUILayout.Label ("Score", m_settings.HighScore.ToString());
		GUILayout.Label ("Score " + PlayerPrefs.GetInt ("HighScore", 0).ToString());

		if (IsMenuActive) {
			Rect windowRect = new Rect(
				(Screen.width - Width) / 2,
			(Screen.height - Height) / 2,
			Width, Height);
		//Интерфейс со всякими штуками гуевыми типа кнопок окошек всплывающих 1 параметр ид второй прямоугольник из которого будет состоять меню
		// третий - метод который будет что то делать в самом меню как правило GUILayout.Label("test");
			GUILayout.Window(0, windowRect, MenuFunction[(byte)ActiveMenu], MenuNames[(byte)ActiveMenu]);//Вызвается преобразованная к байту функция соответствующая порядку перечисления
		} else {//Debug.Log ("Menu is unactive but it is onGUI");
		}
	}

	//********************Функции меню всевозможные*************************
	void OptionsMenu (int id)
	{
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Music vol", GUILayout.Width (90));
		m_settings.MusicVolume = GUILayout.HorizontalSlider (m_settings.MusicVolume, 0.0f, 1.0f);
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Sound vol", GUILayout.Width (90));
		m_settings.SoundVolume = GUILayout.HorizontalSlider (m_settings.SoundVolume, 0.0f, 1.0f);
		GUILayout.EndHorizontal ();

		if(GUILayout.Button("Reset hight srore")){
			m_SoundSource.PlayOneShot(ClickSoud);
			//m_settings.HighScore = 0;        
			PlayerPrefs.SetInt("HighScore", 0);
		}

		if(GUILayout.Button("Back")){
			m_SoundSource.PlayOneShot(ClickSoud);
			m_settings.Save();

			ActiveMenu = MenuTypes.MainMain;        
		}
	}
	void PauseMenu (int id)
	{
	}
	void GameOverMenu (int id)
	{
	}
	//@MainMenu
	void MainMenu (int id)
	{   //Нажатие кнопки
		if(GUILayout.Button("Start Game")){
            m_SoundSource.PlayOneShot(ClickSoud);
            //Деактивируем меню
            IsMenuActive = false;
		}

		if(GUILayout.Button("Options")){
                      m_SoundSource.PlayOneShot(ClickSoud);
                    //Деактивируем меню
                    //IsMenuActive = false;
			ActiveMenu = MenuTypes.OptionMenu;        
		}

        //application.quit не работает для веб плееера и редактора
        if(!Application.isWebPlayer && !Application.isEditor){
        if(GUILayout.Button("Exit")){
                            //Деактивируем меню
                            //IsMenuActive = false;
                m_SoundSource.PlayOneShot(ClickSoud);
                Application.Quit();
                }

        }
	}//********************Функции меню всевозможные*************************

}
