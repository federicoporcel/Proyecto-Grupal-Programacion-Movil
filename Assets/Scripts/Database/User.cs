using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public string nombre;
    public int puntaje;

    public User()
    {
    }

    public User(string name, int score)
    {
        this.nombre = name;
        this.puntaje= score;
    }
}
