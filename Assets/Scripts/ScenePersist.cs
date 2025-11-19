using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

 class ScenePersist : MonoBehaviour
{
    
    int startingSceneIndex;

     void Awake()
    {
        int numScenePersist = FindObjectsOfType<ScenePersist>().Length;
        if (numScenePersist > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start () {
        startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
	}
	

	void Update () {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex != startingSceneIndex)
        {
            Destroy(gameObject);
        }
	}
    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }

}
