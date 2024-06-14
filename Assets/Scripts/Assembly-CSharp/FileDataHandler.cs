using System;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
	private string dataDirPath = "";

	private string dataFileName = "";

	public FileDataHandler(string dataDirPath, string dataFileName)
	{
		this.dataDirPath = dataDirPath;
		this.dataFileName = dataFileName;
	}

	public GameData Load()
	{
		string text = Path.Combine(dataDirPath, dataFileName);
		GameData result = null;
		if (File.Exists(text))
		{
			try
			{
				string json = "";
				using (FileStream stream = new FileStream(text, FileMode.Open))
				{
					using StreamReader streamReader = new StreamReader(stream);
					json = streamReader.ReadToEnd();
				}
				result = JsonUtility.FromJson<GameData>(json);
			}
			catch (Exception ex)
			{
				Debug.LogError("Error occured when trying to load data from file: " + text + "\n" + ex);
			}
		}
		return result;
	}

	public void Save(GameData data)
	{
		string text = Path.Combine(dataDirPath, dataFileName);
		try
		{
			Directory.CreateDirectory(Path.GetDirectoryName(text));
			string value = JsonUtility.ToJson(data, prettyPrint: true);
			using FileStream stream = new FileStream(text, FileMode.Create);
			using StreamWriter streamWriter = new StreamWriter(stream);
			streamWriter.Write(value);
		}
		catch (Exception ex)
		{
			Debug.LogError("Error occured when trying to save data to file: " + text + "\n" + ex);
		}
	}
}
