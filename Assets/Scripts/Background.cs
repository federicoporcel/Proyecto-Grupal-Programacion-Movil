using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    [Range(-1f, 1f)]
    public float speed = 0.5f;

    public float offset;
    private Material material;


    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offset += (speed * Time.deltaTime) / 10f;
        material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
