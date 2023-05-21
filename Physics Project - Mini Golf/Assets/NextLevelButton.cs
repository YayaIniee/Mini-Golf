using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButton : MonoBehaviour
{
    private void OnEnable()
    {
        // check next scene | kalau ga ada hide button next
        var currentScene = SceneManager.GetActiveScene();
        int currentLevel = int.Parse(currentScene.name.Split("level ")[1]);
        int nextLevel = currentLevel + 1;
        var nextSceneBuildIndex = SceneUtility.GetBuildIndexByScenePath("Level "+ nextLevel);
        if(nextSceneBuildIndex == -1)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void NextLevel()
    {
        var currentScene = SceneManager.GetActiveScene();
        int currentLevel = int.Parse(currentScene.name.Split("level ")[1]);
        int nextLevel = currentLevel + 1;
        SceneManager.LoadScene("Level " + nextLevel);
    }

}
