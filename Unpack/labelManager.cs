using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class labelManager : MonoBehaviour
{

	public TextMeshPro label;

	public int roundTo = 2;

    public void UpdateLabel(Vector3 position, Vector3 velocity)
	{
		label.text = "Position: " + Math.Round(position.x,roundTo) + ", " + Math.Round(position.y,roundTo) + "\n" + "Velocity: " + Math.Round(velocity.x,roundTo) + ", " + Math.Round(velocity.y,roundTo);
	}



    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
