using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using UnityEngine.UI;
using Google.MiniJSON;
using System;
using System.IO;
using TMPro;
using UnityEditor;

public class DatabaseManager : MonoBehaviour
{

    public string nombre;
    public int puntaje;
    public string key="";
    public int actualKey ;

    [SerializeField] Creature playerCreature;
    private string machineID;
    const string databaseUrl = "https://console.firebase.google.com/u/0/project/trabajointegradorfb/database/trabajointegradorfb-default-rtdb/data/~2F?hl=es-419";
    // Start is called before the first frame update

    private DatabaseReference reference;

    private static string filePath;
    [SerializeField] TMP_InputField creatureInputField;
    [SerializeField] TMP_InputField userInputField;
    public string userName;

    public List<User> usuariosParaRanking;

    public static DatabaseManager instance { get; private set; }
    void Start()
    {
        instance = this;
        machineID = SystemInfo.deviceUniqueIdentifier;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        //reference = FirebaseDatabase.GetInstance(databaseUrl).RootReference;
        filePath = "/data.json";
        //filePath = Application.persistentDataPath;
        Debug.Log(Application.persistentDataPath);
        Test();
    }

    public void CreateUser()
    {
        User newUser = new User(userName, puntaje, machineID, playerCreature.creatureName);
        string json = JsonUtility.ToJson(newUser);
        reference.Child("Usuarios").Child(actualKey.ToString()).SetRawJsonValueAsync(json);
        File.WriteAllText(filePath, json);
        Debug.Log("User creado");


    }

    public IEnumerator GetNombre(Action<String> oncallback)
    {
        Debug.Log(reference.Child("Usuarios").GetValueAsync());

        var userData = reference.Child("Usuarios").Child("0").Child("nombre").GetValueAsync();
        //var userData = reference.Child("Usuarios").LimitToLast(1);
        Debug.Log(reference.Child("Usuarios").OrderByKey().LimitToLast(1));
        yield return new WaitUntil(predicate: () => userData.IsCompleted);
        if (userData != null)
        {
            DataSnapshot snapshot = userData.Result;
            oncallback.Invoke(snapshot.Value.ToString());
            Debug.Log(snapshot.Value.ToString());
        }
    }

    private void Update()
    {
        puntaje = playerCreature.score;
        BuscarUsuarios();

    }


    public void LoadUsuario()
    {
        StartCoroutine(GetNombre((String nombre) =>
        {
            Debug.Log($"Usuario: {nombre}");
        }));
    }

    public IEnumerator GetLatestUserNumber(Action<String> oncallback)
    {
        int number = 1;
        
        var userData = reference.Child("Usuarios").GetValueAsync();
        Debug.Log(reference.Child("Usuarios").Child(number.ToString()).Child("nombre").GetValueAsync());
        yield return new WaitUntil(predicate: () => userData.IsCompleted);
        if (userData != null)
        {
            DataSnapshot snapshot = userData.Result;
            Debug.Log(snapshot.Children);
            foreach (var child in snapshot.Children)
            {
                Debug.Log($"{child}");
                key= child.Key;
            }
            oncallback.Invoke(snapshot.Value.ToString());
            Debug.Log(snapshot.Value.ToString());
        }


    }
    public IEnumerator GetUsuarios(Action<String> oncallback)
    {
        int number = 5;
        usuariosParaRanking = new List<User>();
        while (number < actualKey) { 
        var userData = reference.Child("Usuarios").Child(number.ToString()).GetValueAsync();
       // Debug.Log(reference.Child("Usuarios").Child(number.ToString()).Child("nombre").GetValueAsync());
        yield return new WaitUntil(predicate: () => userData.IsCompleted);
        if (userData != null)
        {
            DataSnapshot snapshot = userData.Result;
            Debug.Log(snapshot.Children);
                string nameForUser="";
                int scoreForUser=0;
                string idForUser="";
                string creatureNameForUser = "";
            foreach (var child in snapshot.Children)
            {
                    
                    Debug.Log($"{child.Key.ToString()}");
                    switch (child.Key.ToString())
                    {
                        case "creatureNombre":
                            Debug.Log("Llegue a la kriature");
                            creatureNameForUser = child.Value.ToString();
                        break;
                        case "id":
                            idForUser = child.Value.ToString();
                            break;
                            case "nombre":
                            nameForUser = child.Value.ToString();
                            break;
                        case "puntaje":
                            scoreForUser= Convert.ToInt32( child.Value.ToString());
                            break;
                    }
                
                
            }
                usuariosParaRanking.Add(new User(nameForUser, scoreForUser, idForUser, creatureNameForUser));
                //Debug.Log(usuariosParaRanking[0].nombre);
                //Debug.Log(usuariosParaRanking[0].id);
                //Debug.Log(usuariosParaRanking[0].creatureNombre);
                //Debug.Log(usuariosParaRanking[0].puntaje);
                //Debug.Log(usuariosParaRanking.Count);
                //Debug.Log($"{child.Value.ToString()}");
                //oncallback.Invoke(snapshot.Value.ToString());
                Debug.Log(snapshot.Value.ToString());
                ;
            }
        number++;
        }

    }
    //    //while (userData != null)
    //    //{
    //    //    Debug.Log(number);
    //    //    number++;
    //    //    userData = reference.Child("Usuarios").Child(number.ToString()).ToString();
    //    //}

    //    //if (userData == null)
    //    //{
    //    //    return number;
    //    //}

    //    //return number;
    //}

    public void Test()
    {
        StartCoroutine(GetLatestUserNumber((String nombre) =>
        {
            Debug.Log($"Ultimo numero cargado: {key}");
            actualKey = Convert.ToInt32(key.ToString()) + 1;
            
        }));
       
    }

    public void SaveCreatureName()
    {
        playerCreature.creatureName=creatureInputField.text;
        userName = userInputField.text;
        
    }

    public void BuscarUsuarios()
    {
        StartCoroutine(GetUsuarios((String nombre) =>
        {
            
            

        }));

    }
}
