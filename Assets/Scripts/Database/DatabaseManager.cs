using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using UnityEngine.UI;
using Google.MiniJSON;
using System;
using System.IO;

public class DatabaseManager : MonoBehaviour
{

    public string nombre;
    public int puntaje;
    public string key="";

    [SerializeField] Creature playerCreature;
    private string machineID;
    const string databaseUrl = "https://console.firebase.google.com/u/0/project/trabajointegradorfb/database/trabajointegradorfb-default-rtdb/data/~2F?hl=es-419";
    // Start is called before the first frame update

    private DatabaseReference reference;

    private static string filePath;
    [SerializeField] InputField creatureInputField;
    [SerializeField] InputField userInputField;
    public string userName;

    void Start()
    {

        machineID = SystemInfo.deviceUniqueIdentifier;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        //reference = FirebaseDatabase.GetInstance(databaseUrl).RootReference;
        filePath = "/data.json";
        //filePath = Application.persistentDataPath;
        Debug.Log(Application.persistentDataPath);
    }

    public void CreateUser()
    {
        User newUser = new User("TestUserJson", puntaje);
        string json = JsonUtility.ToJson(newUser);
        reference.Child("Usuarios").Child("2").SetRawJsonValueAsync(json);
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
        }));
    }

    public void SaveCreatureName()
    {
        playerCreature.creatureName=creatureInputField.text;
        userName = userInputField.text;
        
    }

}
