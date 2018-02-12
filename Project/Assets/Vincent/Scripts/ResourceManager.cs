﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Resources { population, currency, happiness, environment}

public class ResourceManager : MonoBehaviour {

	private int population = 10000;
	private int currency = 1000000;
	private int happiness = 70;
	private int environment = 85;

	private int prevPop = 10000;
	private int prevCur = 1000000;
	private int prevHap = 70;
	private int prevEnv = 85;

	private Vector4 resourceDelta = new Vector4();

	//Properties for the four resources to enable function calls upon change. 

	/*
	 * ///////////////////////////////////////////////////////////////////////////////////
	 * ///																			   ///
	 * !!! The only place in this script that needs to be changed are these properties.!!!
	 * ///																			   ///
	 * ///////////////////////////////////////////////////////////////////////////////////
	 */

	private int Population {
		get {
			return population;
		}
		set {
			population = value;
			Debug.Log("Adding to population in property!");
			//Some other update functions that need to be called upon changing this value.
		}
	}
	private int Currency {
		get {
			return currency;
		}
		set {
			currency = value;
			Debug.Log("Adding to currency in property!");
			//Some other update functions that need to be called upon changing this value.
		}
	}
	private int Happiness {
		get {
			return happiness;
		}
		set {
			happiness =  value;
			//Some other update functions that need to be called upon changing this value.
		}
	}
	private int Environment {
		get {
			return environment;
		}
		set {
			environment = value;
			Debug.Log("Adding to environment in property!");
			//Some other update functions that need to be called upon changing this value.
		}
	}

    private int GetHappiness() {
        return happiness;
    }
    private int GetCurrency() {
        return currency;
    }
    private int GetEnvironment() {
        return environment;
    }
    private int Get_Population() {
        return population;
    }
    private void Start() {
		EventManager.SendResourceMessage += SendResourceMessage;
        EventManager.GetPopulation += Get_Population;
        EventManager.GetHappiness += GetHappiness;
        EventManager.GetEnvironment += GetEnvironment;
        EventManager.GetCurrency += GetCurrency;
        //SendResourceMessage(new ResourceMessage(Resources.currency, 10), new ResourceMessage(Resources.happiness, 50), new ResourceMessage(Resources.environment, 30), new ResourceMessage(Resources.population, 100));
    }

	//This function is made to handle all resource change subjects. It accepts both positive and negative values.
	//It also accepts an infinite amount of changes per function call.
	//This is the main communication reciever meant for the rest of the game to have influence on the resources.
	public void SendResourceMessage(params ResourceMessage[] res) {
		if(res != null) {
			SavePrevious();
			foreach(ResourceMessage i in res) {
				Resources temp = i.GetResourceType();
				int amt = i.amount;
				//Debug.Log(temp);
				//Debug.Log(i.GetResourceType());
				switch(temp) {
					case Resources.population:
						Population = Population + amt;
						break;
					case Resources.currency:
						Currency = Currency + amt;
						break;
					case Resources.happiness:
						Happiness = Happiness + amt;
						break;
					case Resources.environment:
						Environment = Environment + amt;
						break;
					default:
						Debug.Log("Unhandled enum type");
						break;
				}
			}
			EventManager._SendV4(CalculateDeltas());
			CheckEnd();
		}
	}

	private void SavePrevious() {
		prevPop = population;
		prevCur = currency;
		prevHap = happiness;
		prevEnv = environment;
	}

	private void CheckEnd() {
		Debug.Log("EndCheck");
		if(population <= 0) {
			EventManager._EndGame(Resources.population);
		}
		if(currency <= -1000000) {
			EventManager._EndGame(Resources.currency);
		}
		if(happiness <= 0) {
			EventManager._EndGame(Resources.happiness);
		}
		if(environment <= 0) {
			EventManager._EndGame(Resources.environment);
		}
	}

	//Format of: population, currency, happiness, environment
	private Vector4 CalculateDeltas() {
		resourceDelta = new Vector4(population - prevPop, currency - prevCur, happiness - prevHap, environment - prevEnv);
		return resourceDelta;
	}
}
