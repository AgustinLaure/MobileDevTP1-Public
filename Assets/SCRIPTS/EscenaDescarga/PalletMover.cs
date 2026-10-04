using UnityEngine;

public class PalletMover : ManejoPallets
{
    [SerializeField] private Player player;

    public MoveType miInput;
    public enum MoveType
    {
        WASD,
        Arrows
    }

    public ManejoPallets Desde, Hasta;
    private BaseInputController input;
    bool segundoCompleto = false;

    private void Start()
    {
        input = player.GetInputController;
    }

    private void Update()
    {
        Vector2 currentInputAxis = input.GetAxis();

        if (!Tenencia() && Desde.Tenencia() && currentInputAxis.x < 0f)
        {
            PrimerPaso();
        }
        if (Tenencia() && currentInputAxis.y < 0f)
        {
            SegundoPaso();
        }
        if (segundoCompleto && Tenencia() && currentInputAxis.x > 0f)
        {
            TercerPaso();
        }
    }

    void PrimerPaso()
    {
        Desde.Dar(this);
        segundoCompleto = false;
    }
    void SegundoPaso()
    {
        base.Pallets[0].transform.position = transform.position;
        segundoCompleto = true;
    }
    void TercerPaso()
    {
        Dar(Hasta);
        segundoCompleto = false;
    }

    public override void Dar(ManejoPallets receptor)
    {
        if (Tenencia())
        {
            if (receptor.Recibir(Pallets[0]))
            {
                Pallets.RemoveAt(0);
            }
        }
    }
    public override bool Recibir(Pallet pallet)
    {
        if (!Tenencia())
        {
            pallet.Portador = this.gameObject;
            base.Recibir(pallet);
            return true;
        }
        else
            return false;
    }
}
