using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hernandez : MonoBehaviour
{
	//public int inningNum;

	public int hitCount;

	public int currentDifficulty;


	public float cTimeModifier;


	public float offSpeedChance;
	public float ballPitchChance;


	public Davidson davidson;

	public Niehaus niehaus;

	public TextMeshProUGUI hitCountText;


	public List<GameObject> players = new List<GameObject>();


	public void GetHit()
	{
		hitCount++;

		hitCountText.text = "" + hitCount;

        if(hitCount > 0 && hitCount < 3)
		{
			UpdateDifficulty(0);
		} else if(hitCount >= 3 && hitCount < 5)
		{
			UpdateDifficulty(1);
		} else if(hitCount >= 5 && hitCount < 9)
		{
			UpdateDifficulty(2);
		} else if(hitCount == 10)
		{
			UpdateDifficulty(3);
		} else
		{
			print("YOU WIN!!!!!");
		}

		for (int i = 0; i < players.Count; i++)
		{
			players[i].GetComponent<Animator>().SetTrigger("Jump");
		}


	}

    public void UpdateDifficulty(int newDiff)
	{

		print(" AHHHHHHHHHHHHH" + newDiff);
		currentDifficulty = newDiff;

        if(currentDifficulty == 0)
		{
			cTimeModifier = 0.2f;

			offSpeedChance = 0.1f;

            

		} else if(currentDifficulty == 1)
		{
			cTimeModifier = 0.5f;

			offSpeedChance = 0.15f;

		} else if(currentDifficulty == 2)
		{
			cTimeModifier = 0.6f;
			offSpeedChance = 0.2f;

		} else if(currentDifficulty == 3)
		{
			cTimeModifier = 1f;
            offSpeedChance = 0.4f;
		}

		davidson.offSpeedChance = offSpeedChance;

		niehaus.timeSlider.value = cTimeModifier;




	}

    

    // Start is called before the first frame update
    void Start()
    {
		UpdateDifficulty(0);
    }

    // Update is called once per frame
    void Update()
    {
		//UpdateDifficulty(1);
    }
}
