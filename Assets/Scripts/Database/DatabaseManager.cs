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

    [SerializeField] Creature playerCreature;
    private string userID;
    const string databaseUrl = "https://console.firebase.google.com/u/0/project/trabajointegradorfb/database/trabajointegradorfb-default-rtdb/data/~2F?hl=es-419";
    // Start is called before the first frame update

    private DatabaseReference reference;

    private static string filePath;

    void Start()
    {
        
        userID = SystemInfo.deviceUniqueIdentifier;
        reference= FirebaseDatabase.DefaultInstance.RootReference;
        //reference = FirebaseDatabase.GetInstance(databaseUrl).RootReference;
        filePath = "/data.json";
    }

    public void CreateUser()
    {
        User newUser= new User("TestUserJson", puntaje);
        string json=JsonUtility.ToJson(newUser);
        reference.Child("Usuarios").SetRawJsonValueAsync(json);
        File.WriteAllText(filePath, json);
        Debug.Log("User creado");

    }

    public IEnumerator GetNombre(Action<String> oncallback)
    {
        var userData = reference.Child("Usuarios").Child(userID).Child("nombre").GetValueAsync();
        yield return new WaitUntil(predicate:()=>userData.IsCompleted);
        if (userData!=null)
        {
            DataSnapshot snapshot=userData.Result;
            oncallback.Invoke(snapshot.Value.ToString());
            Debug.Log(snapshot.Value.ToString());
        }
    }

    private void Update()
    {
         puntaje= playerCreature.score;
    }


    public void LoadUsuario()
    {
        StartCoroutine(GetNombre((String nombre) =>
        {
            Debug.Log( $"Usuario: {nombre}");
        }));
    }

}
