using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public int Dinero = 0;
    public int IdPlayer = 0;

    public Bolsa[] Bolasas;
    int CantBolsAct = 0;
    public string TagBolsas = "";

    public enum Estados { EnDescarga, EnConduccion, EnCalibracion, EnTutorial }
    public Estados EstAct = Estados.EnConduccion;

    public bool EnConduccion = true;
    public bool EnDescarga = false;

    public ControladorDeDescarga ContrDesc;
    public ContrCalibracion ContrCalib;
    public ContrTutorial ContrTuto;

    public Visualizacion MiVisualizacion;

    private EventBus eventBus;

    private BaseInputController input;
    [SerializeField] private string mask;

    public BaseInputController GetInputController { get { return input; } }

    [SerializeField] private CanvasGroup steerWheel;

    //------------------------------------------------------------------//

    // Use this for initialization

    private void Awake()
    {
#if UNITY_ANDROID
        UIUtils.SetCanvasState(steerWheel, true);
        //input = new MobileInput();
#elif UNITY_STANDALONE || UNITY_EDITOR

#endif
        input = new InputSystemController(mask);
    }
    void Start()
    {

        for (int i = 0; i < Bolasas.Length; i++)
            Bolasas[i] = null;

        MiVisualizacion = GetComponent<Visualizacion>();

        eventBus = ServiceLocator.Instance.GetService<EventBus>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //------------------------------------------------------------------//

    public void AddMoney(int money)
    {
        Dinero += money;
        eventBus.Raise<OnPlayerMoneyUpdated>(this, Dinero);
    }

    public bool AgregarBolsa(Bolsa b)
    {
        if (CantBolsAct + 1 <= Bolasas.Length)
        {
            Bolasas[CantBolsAct] = b;
            CantBolsAct++;
            AddMoney((int)b.Monto);

            b.Desaparecer();

            eventBus.Raise<OnBagCollected>(this, CantBolsAct);

            return true;
        }
        else
        {
            return false;
        }
    }

    public void VaciarInv()
    {
        for (int i = 0; i < Bolasas.Length; i++)
            Bolasas[i] = null;

        eventBus.Raise<OnBagCollected>(this, 0);

        CantBolsAct = 0;
    }

    public bool ConBolasas()
    {
        for (int i = 0; i < Bolasas.Length; i++)
        {
            if (Bolasas[i] != null)
            {
                return true;
            }
        }
        return false;
    }

    public void SetContrDesc(ControladorDeDescarga contr)
    {
        ContrDesc = contr;
    }

    public ControladorDeDescarga GetContr()
    {
        return ContrDesc;
    }

    public void CambiarACalibracion()
    {
        MiVisualizacion.CambiarACalibracion();
        EstAct = Player.Estados.EnCalibracion;
    }

    public void CambiarATutorial()
    {
        MiVisualizacion.CambiarATutorial();
        EstAct = Player.Estados.EnTutorial;
        ContrTuto.Iniciar();

    }

    public void CambiarAConduccion()
    {
        VaciarInv();
        eventBus.Raise<OnPlayerWaitingTextShouldUpdate>(this, false);

        MiVisualizacion.CambiarAConduccion();
        EstAct = Player.Estados.EnConduccion;
    }

    public void CambiarADescarga()
    {
        MiVisualizacion.CambiarADescarga();
        EstAct = Player.Estados.EnDescarga;
    }


    public void SacarBolasa()
    {
        for (int i = 0; i < Bolasas.Length; i++)
        {
            if (Bolasas[i] != null)
            {
                Bolasas[i] = null;

                CantBolsAct--;
                eventBus.Raise<OnBagCollected>(this, CantBolsAct);

                return;
            }
        }
    }
}
