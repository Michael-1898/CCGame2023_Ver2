using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] GameObject gemFX;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Player" || col.gameObject.name == "Player(Clone)")
        {
            Instantiate(gemFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
