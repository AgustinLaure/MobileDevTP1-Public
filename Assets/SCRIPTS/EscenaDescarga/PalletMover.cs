using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

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
        if (!Tenencia() && Desde.Tenencia() && input.GetLeftPressed())
        {
            PrimerPaso();
        }
        if (Tenencia() && input.GetDownPressed())
        {
            SegundoPaso();
        }
        if (segundoCompleto && Tenencia() && input.GetRightPressed())
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
