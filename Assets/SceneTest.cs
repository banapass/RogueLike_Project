using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SceneTest : SingleTon<SceneTest>
{
    [SerializeField] List<string> scenes = new List<string>();
    [SerializeField] List<string> stages = new List<string>();
    static int stageCount;
    // Start is called before the first frame update
    void Start()
    {


        GetAllScene();
        RandomChoiceStage();

    }
    private void GetAllScene()
    {
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            scenes.Add(scene.path);
        }

    }
    private void RandomChoiceStage()
    {
        int randomNum = Random.Range(0, scenes.Count);
        SceneManager.LoadScene(scenes[randomNum]);
        scenes.Remove(scenes[randomNum]);
        stageCount++;
    }
}
