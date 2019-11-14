using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followScript : MonoBehaviour
{
    int deadParam = Animator.StringToHash("dead");

    private GameController _GameController;
    private bool isFollow;
    private Animator batAnimator;
    public bool isLookLeft;
    public GameObject hitBox;

    // Despertado é chamado quando a instância do script for carregada
    private void Awake()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        batAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isFollow == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _GameController.playerTransform.position, 2 * Time.deltaTime);
        }

        if (transform.position.x < _GameController.playerTransform.position.x && isLookLeft == false)
        {
            flip();
        }
        else if (transform.position.x > _GameController.playerTransform.position.x && isLookLeft == true)
        {
            flip();
        }
    }

    // OnTriggerEnter2D é chamado quando outro Collider2D entra no gatilho (somente física de 2D)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "hitBox":
                isFollow = false;
                _GameController.playSFX(_GameController.sfxEnemyDie, 1f);
                Destroy(hitBox);
                _GameController.scorePoints += 2;
                _GameController.getCoin();
                batAnimator.SetTrigger(deadParam);
                break;
        }
    }

    void onDead()
    {
        Destroy(this.gameObject);
    }

    void flip()
    {
        isLookLeft = !isLookLeft;
        float x = transform.localScale.x;
        x *= -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    void OnBecameVisible()
    {
        isFollow = true;
    }
}
