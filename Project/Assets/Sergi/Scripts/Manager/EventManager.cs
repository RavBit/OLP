﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    public delegate void ChoiceState (Choice _choice);
    public static event ChoiceState ChoiceLoad;
    public static event ChoiceState DisplayChoice;
    public static event ChooseEvent ChoiceUnLoad;

	public delegate void SubmitV4(Vector4 v4);
	public static event SubmitV4 SendV4;

	public delegate void ResourceEvent(params ResourceMessage[] res);
	public static event ResourceEvent SendResourceMessage;
	public static event ResourceEvent EnqueueMessageEvent;

	public delegate void AdvanceEvent();
	public static event AdvanceEvent NextDay;

    public delegate int GetDel();

    public delegate Queue<ResourceMessage> QueueEvent();
	public static event QueueEvent GetQueue;

	public delegate void ChooseEvent();
    public static event ChooseEvent ChoosePositive;
    public static event ChooseEvent ChooseNegative;
    public static event ChooseEvent UIEnable;
    public static event ChooseEvent UIDisable;
    public static event ChooseEvent UIContinue;
	public static event ChooseEvent EmptyQueue;

	public delegate void EndEvent(Resources r);
	public static event EndEvent EndGame;

	public static event ChooseEvent NightCycle;
    public static event GetDel GetPopulation;
    public static event GetDel GetCurrency;
    public static event GetDel GetEnvironment;
    public static event GetDel GetHappiness;

    public static event ChooseEvent DayCycle;
    public static void Day_Cycle() {
        DayCycle();
    }

    public static int Get_Population() {
        return GetPopulation();
    }
    public static int Get_Currency() {
        return GetCurrency();
    }
    public static int Get_Environment() {
        return GetEnvironment();
    }
    public static int Get_Happiness() {
        return GetHappiness();
    }
    public static void Night_Cycle() {
        NightCycle();
    }
    public static void Choice_Load(Choice _choice) {
        ChoiceLoad(_choice);
    }

    public static void Choice_Unload() {
        ChoiceUnLoad();
    }
	
	public static Queue<ResourceMessage> Get_Queue() {
		return GetQueue();
	}
    public static void InterMission_Enable() {
        UIEnable();
    }
    public static void InterMission_Disable() {
		UIDisable();
    }
    public static void InterMission_Continue() {
        UIContinue();
    }
    public static void Choose_Choice(int state) {
        switch(state) {
            case (0):
                ChooseNegative();
                break;
            case (1):
                ChoosePositive();
                break;

        }
    }
    public static void Display_Choice(Choice _choice) {
        DisplayChoice(_choice);
    }

    public static void _SendResourceMessage(params ResourceMessage[] res) {
		SendResourceMessage(res);
	}
	public static void _EnqueueMessage(params ResourceMessage[] res) {
		EnqueueMessageEvent(res);
	}
	public static void _ChoiceLoad() {
		ChoiceUnLoad();
	}
	public static void _NextDay() {
		NextDay();
	}
	public static void _EndGame(Resources r) {
		EndGame(r);
	}
	public static void _SendV4(Vector4 v4) {
		SendV4(v4);
	}
}
