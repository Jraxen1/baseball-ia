using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{

	public bool isPaused = false;


    public void ModifyTimeModifier(float newModifier)
	{
		gameObject.GetComponent<LiteralActualMath>().timeModifier = newModifier;
	}

    public void StartSim()
	{
		gameObject.GetComponent<LiteralActualMath>().hasStarted = true;

	}

    public void PauseSim()
	{
		isPaused = true;
	}

    public void ResumeSim()
	{
		isPaused = false;
	}





}
