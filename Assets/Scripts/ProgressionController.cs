using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ProgressionController
{
    private static int _levelCount;
    private static int _currentLevel;

    public static int LevelCount { get { return _levelCount; } }
    public static int CurrentLevel { get { return _currentLevel; } }

    public static void InitializeLevelCount()
    {
        if (_levelCount != 0)
            return;

        int sceneCount = SceneManager.sceneCountInBuildSettings;

        int maxLevelFound = 0;

        for (int i = 0; i < sceneCount; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = Path.GetFileNameWithoutExtension(scenePath);

            if (GetLevelIndex(sceneName, out var levelIndex))
            {
                maxLevelFound = Mathf.Max(maxLevelFound, levelIndex);
                _levelCount++;
            }
        }
    }

    public static void GoToLevel(int levelIndex)
    {
        SceneManager.LoadScene($"Level{levelIndex}");
    }

    public static bool GetLevelIndex(string sceneName, out int levelIndex)
    {
        Match find = Regex.Match(sceneName, "\\d+"); // Match.Empty

        if (find != Match.Empty)
        {
            levelIndex = Int32.Parse(find.Value);
            return true;
        }

        levelIndex = -1;
        return false;
    }

    public static void SetCurrentLevel()
    {
        if (!GetLevelIndex(SceneManager.GetActiveScene().name, out _currentLevel))
        {
            Debug.LogError("Level Controller on a non-level scene!");
        }
    }

    public static void RetryGame()
    {
        GoToLevel(1);
    }

    public static void NextLevel()
    {
        GoToLevel(_currentLevel + 1);
    }

    public static bool IsLastLevel()
    {
        if (_currentLevel == _levelCount)
        {
            return true;
        }

        return false;
    }
}
