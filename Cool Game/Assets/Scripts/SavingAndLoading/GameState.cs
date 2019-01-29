namespace Assets.Scripts.SavingAndLoading
{
    using UnityEngine;

    public class GameState
    {
        public static int CurrentLevel { get; set; }

        private const string _INTRO_SCENE_PLAYED = "IntroPlayed";
        private const string _OUTRO_SCENE_PLAYED = "OutroPlayed";
        private const string _CURRENT_LEVEL_NAME = "CurrentLevel";

        public static bool GetIntroScenePlayed()
        {
            // 0 not played
            int playedInt = PlayerPrefs.GetInt(_INTRO_SCENE_PLAYED, 0);
            return playedInt != 0;
        }
        public static void SetIntroScenePlayed(bool hasPlayed)
        {
            PlayerPrefs.SetInt(_INTRO_SCENE_PLAYED, hasPlayed ? 1 : 0);
            PlayerPrefs.Save();
        }

        public static bool GetOutroScenePlayed()
        {
            // 0 not played
            int playedInt = PlayerPrefs.GetInt(_OUTRO_SCENE_PLAYED, 0);
            return playedInt != 0;
        }
        public static void SetOutroScenePlayed(bool hasPlayed)
        {
            PlayerPrefs.SetInt(_OUTRO_SCENE_PLAYED, hasPlayed ? 1 : 0);
            PlayerPrefs.Save();
        }

        public static void LoadCurrentLevel()
        {
            int level = Mathf.Max(PlayerPrefs.GetInt(_CURRENT_LEVEL_NAME), 1);
            CurrentLevel = level;
        }
        public static void SaveCurrentLevel()
        {
            PlayerPrefs.SetInt(_CURRENT_LEVEL_NAME, CurrentLevel);
            PlayerPrefs.Save();
        }
    }
}
