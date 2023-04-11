using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionZone : MonoBehaviour
{
    [SerializeField] int nextScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.CompareTag("Player")) {
            if(nextScene == 6) {
                PlayerPrefs.SetInt("currentHealth", 7);
                PlayerPrefs.SetInt("currentLava", 5);
            }
            SceneManager.LoadScene(nextScene);
        }
    }
}
