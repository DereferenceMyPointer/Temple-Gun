using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class credits : MonoBehaviour
{

    public string key;
    public string scene;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown(key))
        {
          SceneManager.LoadScene(scene);
        }
    }
}
