using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1 : MonoBehaviour
{
    public int bullets = 10;
    public float speed = 5.5f;
    public float jump = 10f;
    public string hello = "hello";
    public string many = "10";
    public int players = 1000000000;
    void Start()
    {
        Debug.Log("Ниже переменные для целый чисел");
        Debug.Log(bullets);
        Debug.Log(players);
        Debug.Log("Ниже переменные для дробных чисел");
        Debug.Log(speed);
        Debug.Log(jump);
        Debug.Log("Ниже переменные для текста");
        Debug.Log(hello);
        Debug.Log(many);

    }

    void Update()
    {
        
    }
}
