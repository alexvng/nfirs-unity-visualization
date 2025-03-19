using System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class NFIRSDataPoint
{
	public string _addr;
	public DateTime INC_DATE;
	public float lat;
	public float lng;
	public float elevation;
	public string _desc;

	public static NFIRSDataPoint[] CreateFromJSON(TextAsset jsonFile)
	{
		string json = jsonFile.text;
		return JsonUtility.FromJson<NFIRSDataArray>("{\"points\":" + json + "}").points;
	}

	private class NFIRSDataArray
	{
		public NFIRSDataPoint[] points;
	}
}
