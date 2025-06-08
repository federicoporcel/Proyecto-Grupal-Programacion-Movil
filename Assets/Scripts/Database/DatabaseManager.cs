using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using UnityEngine.UI;

public class DatabaseManager : MonoBehaviour
{

    public InputField Nombre;
    public InputField Puntaje;

    private string userID;
    // Start is called before the first frame update
        
    void Start()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
        DatabaseReference reference= FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void CreateUser()
    {
        //User newUser= new User(Nombre.);
    }

}
