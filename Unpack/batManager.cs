using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batManager : MonoBehaviour
{

	public BatterController batter;

	public void AlertObservers(string message)
	{
		if (message.Equals("SwingEnded"))
		{
			//batter.isSwinging = false;
		}
	}
}
