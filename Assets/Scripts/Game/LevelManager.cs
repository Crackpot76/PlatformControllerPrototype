using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public AudioClip bgMusic;

    public void LoadLevel (string name) {
		Debug.Log("Level load requested for:"+ name);
        SceneManager.LoadScene(name);
	}
	
	public void LoadNextLevel () {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1));
	}
	
	
	public void Quit () {
		Debug.Log("Quit requested!");
		Application.Quit();
	}

    void OnEnable() {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable() {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
//        Debug.Log("Level Loaded");
//        Debug.Log(scene.name);
//        Debug.Log(mode);

        SoundManager.SetBGMVolume(0.5f);
        SoundManager.PlayBGM(bgMusic, false, 0);
    }
}
