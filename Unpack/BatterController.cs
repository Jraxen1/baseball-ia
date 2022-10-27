using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BatterController : MonoBehaviour
{

	public int numStrikes;
	public TextMeshProUGUI strikesText;
	public int numBalls;
	public TextMeshProUGUI ballsText;

	public Animator batAnimator;

	public GameObject swingCollider;

	public bool isSwinging = false;


	public List<GameObject> playerHeads = new List<GameObject>();






	public void StartNewAtBat()
	{
		numStrikes = 0;
		numBalls = 0;
	}

    public void GainStrike()
	{

        for(int i = 0; i < playerHeads.Count; i++)
		{
			playerHeads[i].GetComponent<Animator>().SetTrigger("Shake");
		}

		numStrikes++;
		AtBatUpdate();
	}

    public void GainBall()
	{
		if (isSwinging)
		{
			numStrikes++;
			isSwinging = false;
			AtBatUpdate();
		}
		else
		{
			
			numBalls++;
			AtBatUpdate();
		}
	}

    public void AtBatUpdate()
	{
		//playerHeads[i].GetComponent<Animator>.ResetTrigger("Shake");
		if (numStrikes >= 3)
		{
			print("STRIKE OUT");
			for (int i = 0; i < playerHeads.Count; i++)
			{
				playerHeads[i].GetComponent<Animator>().SetTrigger("Shake");
			}
			StartNewAtBat();
		} else if(numBalls >= 4)
		{
			print("WALK");
			StartNewAtBat();
		}
		else
		{
			print("Count is " + numStrikes + " strikes and " + numBalls + " balls.");
		}

		strikesText.text = numStrikes + " Strikes";
		ballsText.text = numBalls + " Balls";
	}

    


    public void SwingBat()
	{

		batAnimator.SetTrigger("Swing");
		//isSwinging = true;
		//GainStrike();

	}

    // Start is called before the first frame update
    void Start()
    {
        
    }
        
    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			isSwinging = true;
			batAnimator.ResetTrigger("Swing");
			batAnimator.StopPlayback();
			SwingBat();
		}

		/*if (!batAnimator.isPlaying)
		{
			isSwinging = false;
		}*/
    }
}
