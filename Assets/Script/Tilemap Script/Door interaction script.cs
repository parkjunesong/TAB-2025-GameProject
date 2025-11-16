using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Doorinteractionscript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("Indoor Scene");
        }
    }
}
