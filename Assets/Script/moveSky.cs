using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveSky : MonoBehaviour
{
    private Material currentMaterial;
    public float speed;
    private float offSet;

    // Despertado é chamado quando a instância do script for carregada
    private void Awake()
    {
        currentMaterial = GetComponent<Renderer>().material;
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        offSet += speed * Time.deltaTime;
        currentMaterial.SetTextureOffset("_MainTex", new Vector2(offSet, 0));
    }
}
