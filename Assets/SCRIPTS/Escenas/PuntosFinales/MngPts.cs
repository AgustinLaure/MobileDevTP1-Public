using System.Collections;
using TMPro;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class MngPts : MonoBehaviour
{
    Rect R = new Rect();

    public float TiempEmpAnims = 2.5f;
    float Tempo = 0;

    int IndexGanador = 0;

    public Vector2[] DineroPos;
    public Vector2 DineroEsc;
    public GUISkin GS_Dinero;

    public Vector2 GanadorPos;
    public Vector2 GanadorEsc;
    public Texture2D[] Ganadores;
    public GUISkin GS_Ganador;

    public GameObject Fondo;

    public float TiempEspReiniciar = 10;


    public float TiempParpadeo = 0.7f;
    float TempoParpadeo = 0;
    bool PrimerImaParp = true;

    public bool ActivadoAnims = false;

    Visualizacion Viz = new Visualizacion();

    [SerializeField] private TextMeshProUGUI winner;
    [SerializeField] private CanvasGroup winnerCG;
    [SerializeField] private TextMeshProUGUI leftSideMoney;
    [SerializeField] private TextMeshProUGUI rightSideMoney;

    private const string player1Text = "PLAYER #1 ";
    private const string player2Text = "PLAYER #2 ";

    private const string isTheWinnerText = "IS THE WINNER";

   

    // Use this for initialization
    void Start()
    {
        SetGanador();
        SetDinero();
    }

    // Update is called once per frame
    void Update()
    {
        //PARA JUGAR
        if (Input.GetKeyDown(KeyCode.KeypadEnter) ||
           Input.GetKeyDown(KeyCode.Return) ||
           Input.GetKeyDown(KeyCode.Mouse0))
        {
            UnityEngine.Application.LoadLevel(0);
        }

        //REINICIAR
        if (Input.GetKeyDown(KeyCode.Mouse1) ||
           Input.GetKeyDown(KeyCode.Keypad0))
        {
            UnityEngine.Application.LoadLevel(UnityEngine.Application.loadedLevel);
        }

        //CIERRA LA APLICACION
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.Application.Quit();
        }

        ///CALIBRACION DEL KINECT
        //f(Input.GetKeyDown(KeyCode.Backspace))
        //
        //	Application.LoadLevel(3);
        //		

        TiempEspReiniciar -= Time.deltaTime;
        if (TiempEspReiniciar <= 0)
        {
            UnityEngine.Application.LoadLevel(0);
        }


        if (ActivadoAnims)
        {
            TempoParpadeo += Time.deltaTime;

            if (TempoParpadeo >= TiempParpadeo)
            {
                TempoParpadeo = 0;

                if (PrimerImaParp)
                    PrimerImaParp = false;
                else
                {
                    TempoParpadeo += 0.1f;
                    PrimerImaParp = true;
                }
            }
        }


        if (!ActivadoAnims)
        {
            Tempo += Time.deltaTime;
            if (Tempo >= TiempEmpAnims)
            {
                Tempo = 0;
                ActivadoAnims = true;
            }
        }
    }

    /*
	void OnGUI()
	{
		SetGUIGanador();
		SetGUIPerdedor();
		GUI.skin = null;
	}
	*/

    void OnGUI()
    {
        if (ActivadoAnims)
        {
            //SetDinero();
            SetCartelGanador();
        }

        GUI.skin = null;
    }

    //---------------------------------//

    /*
	void SetGUIGanador()
	{
		GUI.skin = GS_Vict;
		
		R.width = ScoreEsc.x * Screen.width /100;
		R.height = ScoreEsc.y * Screen.height /100;
		
		R.x = ScorePos.x * Screen.width / 100;
		R.y = ScorePos.y * Screen.height / 100;
		
		if(DatosPartida.LadoGanadaor == DatosPartida.Lados.Der)
			R.x = (Screen.width) - R.x - R.width;
		
		GUI.Box(R, "GANADOR" + '\n' + "DINERO: " + DatosPartida.PtsGanador);
		
	}
	
	void SetGUIPerdedor()
	{
		GUI.skin = GS_Derr;
		
		R.width = ScoreEsc.x * Screen.width /100;
		R.height = ScoreEsc.y * Screen.height /100;
		
		R.x = ScorePos.x * Screen.width / 100;
		R.y = ScorePos.y * Screen.height / 100;
		
		if(DatosPartida.LadoGanadaor == DatosPartida.Lados.Izq)
			R.x = (Screen.width) - R.x - R.width;
		
		GUI.Box(R, "PERDEDOR" + '\n' + "DINERO: " + DatosPartida.PtsPerdedor);
	}
	*/


    void SetGanador()
    {
        switch (DatosPartida.LadoGanadaor)
        {
            case DatosPartida.Lados.Der:

                winner.text = player2Text + isTheWinnerText;

                break;

            case DatosPartida.Lados.Izq:

                winner.text = player1Text + isTheWinnerText;

                break;
        }
    }

    void SetDinero()
    {
        int leftTotalPoints = 0;
        int rightTotalPoints = 0;

        if (DatosPartida.LadoGanadaor == DatosPartida.Lados.Izq)
        {
            leftTotalPoints = DatosPartida.PtsGanador;
            rightTotalPoints = DatosPartida.PtsPerdedor;
        }
        else if (DatosPartida.LadoGanadaor == DatosPartida.Lados.Der)
        {
            leftTotalPoints = DatosPartida.PtsPerdedor;
            rightTotalPoints = DatosPartida.PtsGanador;
        }

        leftSideMoney.text = "$" + leftTotalPoints.ToString("N0");
        rightSideMoney.text = "$" + rightTotalPoints.ToString("N0");


        //GUI.skin = GS_Dinero;
        //
        //R.width = DineroEsc.x * Screen.width / 100;
        //R.height = DineroEsc.y * Screen.height / 100;
        //
        //
        ////IZQUIERDA
        //R.x = DineroPos[0].x * Screen.width / 100;
        //R.y = DineroPos[0].y * Screen.height / 100;
        //
        //if (DatosPartida.LadoGanadaor == DatosPartida.Lados.Izq)//izquierda
        //{
        //    if (!PrimerImaParp)//para que parpadee
        //        GUI.Box(R, "$" + Viz.PrepararNumeros(DatosPartida.PtsGanador));
        //}
        //else
        //{
        //    GUI.Box(R, "$" + Viz.PrepararNumeros(DatosPartida.PtsPerdedor));
        //}
        //
        //
        //
        ////DERECHA
        //R.x = DineroPos[1].x * Screen.width / 100;
        //R.y = DineroPos[1].y * Screen.height / 100;
        //
        //if (DatosPartida.LadoGanadaor == DatosPartida.Lados.Der)//derecha
        //{
        //    if (!PrimerImaParp)//para que parpadee
        //        GUI.Box(R, "$" + Viz.PrepararNumeros(DatosPartida.PtsGanador));
        //}
        //else
        //{
        //    GUI.Box(R, "$" + Viz.PrepararNumeros(DatosPartida.PtsPerdedor));
        //}
    }

    void SetCartelGanador()
    {
        //GUI.skin = GS_Ganador;
        //
        //R.width = GanadorEsc.x * Screen.width / 100;
        //R.height = GanadorEsc.y * Screen.height / 100;
        //R.x = GanadorPos.x * Screen.width / 100;
        //R.y = GanadorPos.y * Screen.height / 100;
        //
        ////if(PrimerImaParp)//para que parpadee
        //GUI.Box(R, "");
    }

    public void DesaparecerGUI()
    {
        ActivadoAnims = false;
        Tempo = -100;
    }
}
