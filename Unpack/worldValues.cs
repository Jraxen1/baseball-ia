using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class worldValues : MonoBehaviour
{

	public float airDensity = 1.22f;

	public float airPressure = 1013.25f; // hPa (average at sea level taken)

	public float airTemperature = 287.594f; // K (Year-long average Seattle weather)

	public float airTempCelsius; // C (Calculate from airTemp in Kelvin -> celsius)

	public float relativeHumidity = 71.4f; // Percentage (Taken from average Washington humidity)

	public float vaporPressure; // 

	float gasConstantForAir = 287.058f; // J/kg*K
	float gasConstantForWater = 461.495f; // J/kg*K




	public float calculateAirDensity()
	{

		//ρ = (pd / (Rd * T)) + (pv / (Rv * T))

		float airPressureDry = (float)calculateAirPressureDry();

        

		float airDensity = (airPressureDry / (gasConstantForAir * airTemperature)) + (vaporPressure / (gasConstantForWater * airTemperature));


		return airDensity;

	}





    public double calculateAirPressureDry()
	{
		double ret;

		vaporPressure = (float)calculateSaturationVaporPressure() * relativeHumidity;

		ret = airPressure - vaporPressure;

		print(ret + "");

		return ret;

	}

    public double calculateSaturationVaporPressure()
	{
		//6.1078 * 10^[7.5*T /(T + 237.3)
		double ret = 6.1078 * Math.Pow(10, ((7.5 * airTempCelsius) / (airTempCelsius + 237.3)));
		print("Saturation vapor pressure : " + ret);

		return ret;
	}
	// Start is called before the first frame update
	void Start()
    {

		/*airTempCelsius = airTemperature - 273.15f;

		print(calculateAirDensity() + ".");*/

        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
