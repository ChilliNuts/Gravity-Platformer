using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveManager : MonoBehaviour {

	public static int localMaxLevel = 0;
	public static int localDeaths = 0;
	public static int localShotsFired = 0;


	public static void SaveGame(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Open(Application.persistentDataPath +"/GravitronSave.dat", FileMode.OpenOrCreate);

		SaveData data = new SaveData();
		data.maxLevel = localMaxLevel;
		data.deaths = localDeaths;
		data.shotFired = localShotsFired;

		bf.Serialize(file, data);
		file.Close();
	}

	public static void LoadGame(){
		if(File.Exists(Application.persistentDataPath + "/GravitronSave.dat")){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath +"/GravitronSave.dat", FileMode.Open);
			SaveData data = (SaveData)bf.Deserialize(file);
			file.Close();

			localMaxLevel = data.maxLevel;
			localDeaths = data.deaths;
			localShotsFired = data.shotFired;
		}
	}
	public static void ResetData(){
		localMaxLevel = 0;
		localDeaths = 0;
		localShotsFired = 0;
		SaveGame();
	}
}

[Serializable]
class SaveData{

	public int maxLevel;
	public int deaths;
	public int shotFired;
}
