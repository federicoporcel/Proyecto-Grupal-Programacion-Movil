using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public string nombre;
    public int puntaje;
    public string id;
    public string creatureNombre;
    public User()
    {
    }

    public User(string name, int score)
    {
        this.nombre = name;
        this.puntaje= score;
    }

    public User(string name, int score, string userID, string creatureName)
    {

        this.nombre = name;
        this.puntaje = score;
        this.id=userID;
        this.creatureNombre = creatureName;
        
    }
}
