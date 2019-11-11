using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.DesignPattern;
using System;

public class LevelManager : PublicSingleton<LevelManager> {

    public int CurrentLevel = 0;

    public string MutationsScreen = "UI_Gestion";

    public string[] Levels =
    {
        "Level1",
        "Level2",
        "Level3",
        "Level4",
        "Level5"
    };


    public void NextLevel()
    {
        Scenes.UnloadAsync(Levels[CurrentLevel]);
        CurrentLevel++;
        if (CurrentLevel < Levels.Length)
        {
            Scenes.Load(Levels[CurrentLevel], UnityEngine.SceneManagement.LoadSceneMode.Additive);
            Scenes.UnloadAsync(MutationsScreen);
        }
        else
        {
            Scenes.Load("EndGameScreen", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }
        Time.timeScale = 1;
    }
}
