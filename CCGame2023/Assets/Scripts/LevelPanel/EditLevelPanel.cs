using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EditLevelPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EditLevel()
    {
        LoadLevel.LoadLevelFilePath = transform.parent.gameObject.transform.GetChild(0).GetComponent<Text>().text;
        SceneManager.LoadScene("LevelEditor");
    }
}
