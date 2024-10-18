using System;
using System.Globalization;
using UnityEngine;

namespace Features.MainScript.Main
{
    public static class GameDataPref
    {
        private const string KillCountKey = "KILL_COUNT_KEY";
        private const string GamePlayStartTimKey = "GAMWE_PLAY_START_TIME";
        private const string GamePlayTimKey = "GAMWE_PLAY_TIME";


        public static int KillCount
        {
            get => PlayerPrefs.GetInt(KillCountKey, 0);
            set => PlayerPrefs.SetInt(KillCountKey, value);
        }

        public static DateTime GamePlayStartTime
        {
            get
            {
                var canParse = DateTime.TryParse(PlayerPrefs.GetString(GamePlayStartTimKey, ""),
                    out var startedTime);
                return canParse ? startedTime : DateTime.Now;
            }
            set => PlayerPrefs.SetString(GamePlayStartTimKey, value.ToString(CultureInfo.CurrentCulture));
        }

        public static float GamePlayTime
        {
            get => PlayerPrefs.GetFloat(GamePlayTimKey, 0);
            set => PlayerPrefs.SetFloat(GamePlayTimKey, value);
        }

        public static void ResetData()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}