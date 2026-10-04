using UnityEngine;
using System.Collections;

public class DatosPartida
{
    public static float TiempoDeJuego;

    public static Difficulty difficulty;
    public enum Lados { Izq, Der }
    public static Lados LadoGanadaor;
    public static int PtsGanador;
    public static int PtsPerdedor;
    public static bool isSinglePlayer = true;
    public static int highestScore = 0;

    private const string highScoreKey = "highScore";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadData()
    {
        highestScore = PlayerPrefs.GetInt(highScoreKey, 0);
    }

    public static int GetHighscore()
    {
        return PlayerPrefs.GetInt(highScoreKey, 0);
    }

    public static void SetHighscore(int newHighscore)
    {
        if (PlayerPrefs.GetInt(highScoreKey, 0) < newHighscore)
        {
            highestScore = newHighscore;
            PlayerPrefs.SetInt(highScoreKey, highestScore);
            PlayerPrefs.Save();
        }
    }
}
