using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//all way longer and more drawn out then it needs to be but im bad
public class fearGaugeCalculator: MonoBehaviour {

	private bool spookyFearCD = false;
	public bool inSpookyArea = false;
	private bool antiDepressionFMincd = false;
	public bool awoken = false;
	private bool Insane = false;
	private bool fearUpgradeLevelChange = false;
	public float fearGaugeForMathDisplay = 0;
	private float fearGaugeForMathMax = 100;
	public bool partyMemberDied = false;
	private bool ranAway = false;
	private string fearUpgradeLevel = "1";
	private float antiDepressionFMin = 0;
	private float killedEnemy = 0;
	public int newEnemyFear = 0;
	public bool newAreaFear = false;
	private float fearGaugeForMath = 100f;
	Henry playerInstance;
	PlayerMovement playerInstanceTwo;

	//https://docs.unity3d.com/ScriptReference/MonoBehaviour.StartCoroutine.html pretty cool

	IEnumerator spookyAreaFear ()
	{
		fearGaugeForMath -= 1;
		fearGaugeForMathDisplay = fearGaugeForMathMax - fearGaugeForMath;
		spookyFearCD = true;
		yield return new WaitForSecondsRealtime (10);
		spookyFearCD = false;
	}
		
	IEnumerator nectarfear ()
	{
        for (int i = 0; i < 25; i++)
        {
            fearGaugeForMath -= 1;
            fearGaugeForMathDisplay = fearGaugeForMathMax - fearGaugeForMath;
            yield return new WaitForSecondsRealtime(2);
        }
	}

	IEnumerator adchecker ()
	{
		if (playerInstance.antiDepression == true) {
			antiDepressionFMin = 1;
			antiDepressionFMincd = true;
			yield return new WaitUntil (() => playerInstance.antiDepression == false);
			antiDepressionFMincd = false;
		}

	}

	void Update () {
		playerInstanceTwo = GetComponent<PlayerMovement> ();
		playerInstance = GetComponent<Henry> ();
		//--------------------------------
		//checking if antidepression is currently in use, then updates code to try to reduce fear if not insane
		if (antiDepressionFMincd == false) {
			if (playerInstance.antiDepression == true) {
				StartCoroutine (adchecker ());
			}
		}
		//--------------------------------
		//ded checker
		if (fearGaugeForMath < 1) {
			Insane = true;
			fearGaugeForMathDisplay = fearGaugeForMathMax;
		}
		//--------------------------------
		//more fear capacity per level
		if (fearUpgradeLevelChange == true) {
			switch (fearUpgradeLevel) {
			case "1":
				fearGaugeForMathMax = 100;
				fearGaugeForMath *= 1.3f;
				fearGaugeForMathDisplay = fearGaugeForMathMax - fearGaugeForMath;
				break;
			case "2":
				fearGaugeForMathMax = 200;
				fearGaugeForMath *= 1.3f;
				fearGaugeForMathDisplay = fearGaugeForMathMax - fearGaugeForMath;
				break;
			case "3":
				fearGaugeForMathMax = 250;
				fearGaugeForMath *= 1.3f;
				fearGaugeForMathDisplay = fearGaugeForMathMax - fearGaugeForMath;
				break;
			case "4":
				fearGaugeForMathMax = 270;
				fearGaugeForMath *= 1.3f;
				fearGaugeForMathDisplay = fearGaugeForMathMax - fearGaugeForMath;
				break;
			case "5":
				fearGaugeForMathMax = 300;
				fearGaugeForMath *= 1.3f;
				fearGaugeForMathDisplay = fearGaugeForMathMax - fearGaugeForMath;
				break;
			case "6":
				fearGaugeForMathMax = 310;
				fearGaugeForMath *= 1.3f;
				fearGaugeForMathDisplay = fearGaugeForMathMax - fearGaugeForMath;
				break;
			}
		}
		//-------------------------------
		//fear can not go above capacity
		if (fearGaugeForMath > fearGaugeForMathMax) {
			fearGaugeForMath = fearGaugeForMathMax;
		}
		//-------------------------------
		//fear can be toooooo bad
		if (fearGaugeForMathDisplay > fearGaugeForMathMax) {
				fearGaugeForMathDisplay = fearGaugeForMathMax;
		}
        //-------------------------------
        //fear increasing factors
        //-------------------------------
        //checking if you are already insane, only certain things like waking up fix this
        if (Insane == false)
        {
            //sets the amount of fear you gain from either normal enemies or bosses
            if (newEnemyFear == 1)
            {
                fearGaugeForMath -= 5;
                fearGaugeForMathDisplay = fearGaugeForMathMax - fearGaugeForMath;
            }
            else if (newEnemyFear == 2)
            {
                fearGaugeForMath -= 10;
                fearGaugeForMathDisplay = fearGaugeForMathMax - fearGaugeForMath;
            }
            //--------------------------------
            //Running away from fights is bad
            if (ranAway == true)
            {
                fearGaugeForMath -= 15;
                ranAway = false;
            }
            //--------------------------------
            //spooky area is bad
            if (inSpookyArea == true)
            {
                if (spookyFearCD == false)
                {
                    StartCoroutine(spookyAreaFear());
                }
            }
            //Afraid of the unknown
            if (newAreaFear == true)
            {
                fearGaugeForMath -= 10;
                newAreaFear = false;
                fearGaugeForMathDisplay = fearGaugeForMathMax - fearGaugeForMath;
            }
            //--------------------------------
            //save your friends or else
            if (partyMemberDied == true)
            {
                fearGaugeForMath -= 15;
                partyMemberDied = false;
                fearGaugeForMathDisplay = fearGaugeForMathMax - fearGaugeForMath;
            }
            //--------------------------------
            //fear decreasing factors
            //--------------------------------
            //facing fears 1-normal enemy 2-biggo bosso
            if (killedEnemy == 1)
            {
                fearGaugeForMath += 1;
                killedEnemy = 0;
                fearGaugeForMathDisplay = fearGaugeForMathMax - fearGaugeForMath;
            }
            if (killedEnemy == 2)
            {
                fearGaugeForMath += 15;
                killedEnemy = 0;
                fearGaugeForMathDisplay = fearGaugeForMathMax - fearGaugeForMath;
            }
            //--------------------------------
            //antidepressants and alcohol are good for you
            if (antiDepressionFMin == 1)
            {
                fearGaugeForMathDisplay *= 0.85f;
                antiDepressionFMin = 0;
                fearGaugeForMathDisplay = Mathf.Round(fearGaugeForMathDisplay);
                fearGaugeForMath = fearGaugeForMathMax - fearGaugeForMathDisplay;

            }
            if (playerInstance.alcohol == true)
            { 
                fearGaugeForMath += 25;
                StartCoroutine(nectarfear());
                playerInstance.alcohol = false;
            }
        }
			//---------------------------------
			//waking up
		if (awoken == true) {
			fearGaugeForMath = fearGaugeForMathMax;
            fearGaugeForMathDisplay = fearGaugeForMathMax - fearGaugeForMath;
            Insane = false;
			awoken = false;
		}
	}
}
