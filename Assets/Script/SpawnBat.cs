using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBat : MonoBehaviour
{
    public GameObject batPrefab;
    public GameObject spawnPrefab;
    private GameObject tempBat;
    public float rateSpawn;
    public float currentTime;
    public bool spawned;

    // Despertado é chamado quando a instância do script for carregada
    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (tempBat == null)
        {
            spawned = false;
        }
        if (currentTime >= rateSpawn && spawned == false)
        {
            currentTime = 0;
            GameObject temp = Instantiate(spawnPrefab, transform.position, transform.localRotation);
            temp.transform.position = new Vector3(transform.position.x, temp.transform.position.y, temp.transform.position.z);
            Destroy(temp.gameObject, 0.2f);
            tempBat = Instantiate(batPrefab) as GameObject;
            tempBat.transform.position = new Vector3(transform.position.x, transform.position.y, tempBat.transform.position.z);
            spawned = true;
            rateSpawn = Random.Range(30, 120);
        }
    }
}
