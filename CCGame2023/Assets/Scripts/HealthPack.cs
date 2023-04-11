using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField] int maxPacks;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.CompareTag("Player") && col.gameObject.GetComponent<PlayerHealthPack>().currentPacks < maxPacks) {
            col.gameObject.GetComponent<PlayerHealthPack>().addPack(1);
            Destroy(gameObject);
        }
    }
}
