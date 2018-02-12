﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ChoiceCreator : EditorWindow {

	private string fileName = "DefaultName";
	public ResourceMessage negMessage;
	public ResourceMessage posMessage;
	private List<ResourceMessage> negMessages;
	private List<ResourceMessage> posMessages;
	public Choice choice;
	public Dialog negative;
	public Dialog positive;


	public int[] ints = {0, 1, 2};

	[MenuItem("Window/Choice Creator")]
	static public void OpenWindow() {
		ChoiceCreator window = (ChoiceCreator)GetWindow(typeof(ChoiceCreator));
		window.minSize = new Vector2(400, 350);
		window.maxSize = new Vector2(400, 800);
		window.Show();
	}

	private void OnEnable() {
		InitData();
	}

	private void PositiveField() {
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Positive dialog");
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Dialog");
		positive.text = EditorGUILayout.TextField(positive.text, GUILayout.Height(80));
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Message to add");
		posMessage = EditorGUILayout.ObjectField(posMessage, typeof(ResourceMessage), false) as ResourceMessage;
		if(posMessage != null) {
			if(GUILayout.Button("Push Message to positive list", GUILayout.Height(30))) {
				posMessages.Add(posMessage);
				posMessage = null;
			}
		}
		else {
			EditorGUILayout.HelpBox("No message to add.", MessageType.Warning);
		}
		EditorGUILayout.EndHorizontal();
	}

	private void NegativeField() {
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Negative dialog");
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Dialog");
		negative.text = EditorGUILayout.TextField(negative.text, GUILayout.Height(80));
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Message to add");
		negMessage = EditorGUILayout.ObjectField(negMessage, typeof(ResourceMessage), false) as ResourceMessage;
		if(negMessage != null) {
			if(GUILayout.Button("Push Message to positive list", GUILayout.Height(30))) {
				negMessages.Add(negMessage);
				negMessage = null;
			}
		}
		else {
			EditorGUILayout.HelpBox("No message to add.", MessageType.Warning);
		}
		EditorGUILayout.EndHorizontal();
	}

	private void OnGUI() {

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("FileName");
		fileName = EditorGUILayout.TextField(fileName);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Charater name");
		choice.Name = EditorGUILayout.TextField(choice.Name);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Character art");
		choice.Character = EditorGUILayout.ObjectField(choice.Character, typeof(GameObject), false) as GameObject;
		
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Dilemma text");
		choice.Dilemma = EditorGUILayout.TextField(choice.Dilemma, GUILayout.Height(80));
		EditorGUILayout.EndHorizontal();

		PositiveField();
		
		NegativeField();
		
		
		EditorGUILayout.BeginHorizontal();
		
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		if(GUILayout.Button("Reset all values", GUILayout.Height(30))) {
			InitData();
		}
		if(choice.Character == null) {
			EditorGUILayout.HelpBox("Sprite is needed to create choice.", MessageType.Warning);
		}
		else if(fileName == null || fileName.Length < 1 || fileName == "DefaultName") {
			EditorGUILayout.HelpBox("FileName is too short or hasn't been changed.", MessageType.Warning);
		}
		else if(GUILayout.Button("Create Choice", GUILayout.Height(30))) {
			SaveChoice();
		}
		EditorGUILayout.EndHorizontal();
	}

	public void OnInspectorUpdate() {
		this.Repaint();
	}

	public void InitData() {
		negMessages = new List<ResourceMessage>();
		posMessages = new List<ResourceMessage>();
		choice = new Choice();
		negative = new Dialog();
		positive = new Dialog();
		fileName = "DefaultName";
		negMessage = null;
		posMessage = null;
}

	private void SaveChoice() {
		negative.messages = new ResourceMessage[negMessages.Count];
		positive.messages = new ResourceMessage[posMessages.Count];
		for(int i = 0; i < negMessages.Count; i++) {
			negative.messages[i] = negMessages[i];
		}
		for(int i = 0; i < posMessages.Count; i++) {
			positive.messages[i] = posMessages[i];
		}
		choice.NegativeDialog = negative;
		choice.PositiveDialog = positive;

		string dataPath = "Assets/Resources/DialogOptions/NegativeDialog/" + fileName + "Neg.asset";
		AssetDatabase.CreateAsset(negative, dataPath);
		dataPath = "Assets/Resources/DialogOptions/PositiveDialog/" + fileName + "Pos.asset";
		AssetDatabase.CreateAsset(positive, dataPath);
		dataPath = "Assets/Resources/DialogOptions/Choices/" + fileName + ".asset";
		AssetDatabase.CreateAsset(choice, dataPath);
		InitData();
		//Close();

	}
}
