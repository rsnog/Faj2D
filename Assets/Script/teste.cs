using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teste : MonoBehaviour
{
    private SpriteRenderer _sR;
    private Rigidbody2D _rB;
    private CompositeCollider2D _cC;

    // Despertado é chamado quando a instância do script for carregada
    private void Awake()
    {
        _sR = GetComponent<SpriteRenderer>();
        _rB = GetComponent<Rigidbody2D>();
        _cC = GetComponent<CompositeCollider2D>();

        if (_sR != null)
        {
            Debug.Log("Eu tenho um SpriteRenderer");
            _sR.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Não tenho um SpriteRenderer");
        }

        if (_rB != null)
        {
            Debug.Log("Eu tenho um Rigidbody");
        }
        else
        {
            Debug.Log("Não tenho um Rigidbody");
        }

        if (_cC != null)
        {
            Debug.Log("Eu tenho um CompositeCollider2D");
        }
        else
        {
            Debug.Log("Não tenho um CompositeCollider2D");
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
