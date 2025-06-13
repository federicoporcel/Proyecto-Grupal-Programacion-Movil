using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    public GameObject volumenPanel;
    public GameObject jugarPanel;
    public GameObject criatura;
    public GameObject volverAMenuButton;
    [SerializeField] GameObject cargarAFBButton;
    public GameObject playScene;
    [SerializeField] GameObject configurationPanel;

    public UnityEngine.UI.Button keyboardButton;
    [SerializeField] List<TextMeshProUGUI> rankingTextsName;
    [SerializeField] List<TextMeshProUGUI> rankingTextsScore;


    public TouchScreenKeyboard keyboard;

    public static MenuController instance;
    List<User> userRanking;

    [SerializeField] Creature playerCreature;
    // Start is called before the first frame update
    
    void Start()
    {
        instance = this;
        //Vibrator.Vibrate();
        Handheld.Vibrate();

    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            userRanking = DatabaseManager.instance.usuariosParaRanking.OrderByDescending(w => w.puntaje).ToList();
            //foreach (User user in userRanking)
            //{
            //    Debug.Log(user.nombre);
            //    Debug.Log(user.puntaje);
            //}
            //Debug.Log("Lowest score" + userRanking[2].nombre + "Puntaje" + userRanking[2].puntaje);
            for (int i = 0; i < userRanking.Count - 1&&i<3; i++)
            {
                rankingTextsName[i].text = userRanking[i].nombre;
                rankingTextsScore[i].text = userRanking[i].puntaje.ToString();
            }
        }
        catch { 
        
        }
        
    }

    public void ActivarVolumenPanel()
    {
        volumenPanel.SetActive(true);
        jugarPanel.SetActive(false);

    }

    public void volverAMenúPrincipal()
    {
        volumenPanel.SetActive(false);
        jugarPanel.SetActive(true);
        criatura.SetActive(false);
        volverAMenuButton.SetActive(false);
        configurationPanel.SetActive(false);
    }
    public static void MoveAndroidApplicationToBack()
    {
        //AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        //activity.Call<bool>("moveTaskToBack", true);

        //Application.Quit();
#if UNITY_EDITOR
        // Cierra el Play Mode en el Editor
        EditorApplication.isPlaying = false;
#elif UNITY_ANDROID || UNITY_IOS
            // Cierra la app en móviles
            Application.Quit();
#else
            // Para otras plataformas (Windows, Mac, WebGL...)
            Application.Quit();
#endif
    }

    public void jugar()
    {
        Handheld.Vibrate();
        volumenPanel.SetActive(false);
        jugarPanel.SetActive(false);
        criatura.SetActive(true);
        volverAMenuButton.SetActive(true);
        playScene.SetActive(true);
        playerCreature.score = 0;
        cargarAFBButton.gameObject.SetActive(true);
        //DatabaseManager.instance.CreateUser();
        
    }

    public void Configure()
    {
        volumenPanel.SetActive(false);
        jugarPanel.SetActive(false);
        criatura.SetActive(false);
        volverAMenuButton.SetActive(false);
        configurationPanel.SetActive(true);
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }


    public void OpenKeyboard()
    {
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false);
    }

    //private void OnGUI()
    //{
    //    if (GUI.Button((keyboardButton.GetComponent<RectTransform>().rect),"Default")){
    //        Debug.Log("Fuck fuck fuck");
    //        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    //    }
    //}

}
