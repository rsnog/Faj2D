using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    public Transform background;
    public float speed;
    private Transform cam;
    private Vector3 previewCamPosition;

    // Despertado é chamado quando a instância do script for carregada
    private void Awake()
    {
        cam = Camera.main.transform;
        previewCamPosition = cam.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // LateUpdate será chamada a cada quadro, se o Comportamento estiver habilitado
    private void LateUpdate()
    {
        float paralaxX = previewCamPosition.x - cam.position.x;
        float bgTargetX = background.position.x + paralaxX;
        Vector3 bgPosition = new Vector3(bgTargetX, background.position.y, background.position.z);
        background.position = Vector3.Lerp(background.position, bgPosition, speed * Time.deltaTime);
        previewCamPosition = cam.position;
    }


}
