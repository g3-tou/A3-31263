using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public void LoadNextScene(){
        DontDestroyOnLoad(this);
        SceneManager.LoadScene("Assignment4");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void QuitGame(){
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(0);
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        if(scene.buildIndex == 1){
            Button exit = GameObject.FindWithTag("QuitButton").GetComponent<Button>();
            exit.onClick.AddListener(QuitGame);
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
