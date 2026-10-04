using UnityEngine;

public class ControlDireccion : MonoBehaviour 
{
	[SerializeField] private Player player;
	private BaseInputController input;

	public Transform ManoDer;
	public Transform ManoIzq;
	
	public float MaxAng = 90;
	public float DesSencibilidad = 90;
	
	float Giro = 0;
	
	public enum Sentido {Der, Izq}
	Sentido DirAct;
	
	public bool Habilitado = true;
	//float Diferencia;
		
	//---------------------------------------------------------//
	
	// Use this for initialization
	void Start () 
	{
		input = player.GetInputController;
	}
	
	// Update is called once per frame
	void Update () 
	{
        gameObject.GetComponent<CarController>().SetGiro(input.GetSteering());

       // switch (InputAct)
		//{
		//case TipoInput.Mouse:
		//	if(Habilitado) 
		//		gameObject.GetComponent<CarController>().SetGiro(MousePos.Relation(MousePos.AxisRelation.Horizontal));
	   //
       //     break;
		//	
       //     case TipoInput.AWSD:
       //         if (Habilitado) {
       //             if (input)
       //             {
       //                 gameObject.GetComponent<CarController>().SetGiro(-1);
       //             }
       //             if (Input.GetKey(KeyCode.D))
       //             {
       //                 gameObject.GetComponent<CarController>().SetGiro(1);
       //             }
       //         }
       //         break;
       //     case TipoInput.Arrows:
       //         if (Habilitado) {
       //             if (Input.GetKey(KeyCode.LeftArrow))
       //             {
       //                 gameObject.GetComponent<CarController>().SetGiro(-1);
       //             }
       //             if (Input.GetKey(KeyCode.RightArrow))
       //             {
       //                 gameObject.GetComponent<CarController>().SetGiro(1);
       //             }
       //         }
       //         break;
       // }		
	}

	public float GetGiro()
	{
		/*
		switch(DirAct)
			{
			case Sentido.Der:
				if(Angulo() <= MaxAng)
					return Angulo() / MaxAng;
				else
					return 1;
				break;
				
			case Sentido.Izq:
				if(Angulo() <= MaxAng)
					return (Angulo() / MaxAng) * (-1);
				else
					return (-1);
				break;
			}
		*/
		
		return Giro;
	}
	
	float Angulo()
	{
		Vector2 diferencia = new Vector2(ManoDer.localPosition.x, ManoDer.localPosition.y)
						   - new Vector2(ManoIzq.localPosition.x, ManoIzq.localPosition.y);
		
		return Vector2.Angle(diferencia,new Vector2(1,0));
	}
	
}
