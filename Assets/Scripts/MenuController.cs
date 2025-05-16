using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    public GameObject volumenPanel;
    public GameObject jugarPanel;
    public GameObject criatura;
    public GameObject volverAMenuButton;
    public GameObject playScene;

    public static MenuController instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        volumenPanel.SetActive(false);
        jugarPanel.SetActive(false);
        criatura.SetActive(true);
        volverAMenuButton.SetActive(true);
        playScene.SetActive(true);
    }

}
