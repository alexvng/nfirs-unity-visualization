using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FireMarkerManager : MonoBehaviour
{
    [SerializeField] private TextAsset csvFile;
    private List<string[]> records = null;

    // Start is called before the first frame update
    void Start()
    {
        if (csvFile != null)
        {
            records = ReadCSV(csvFile);
            Debug.Log(records[0][0]);
        }
        else
        {
            Debug.LogError("No file attached");
        }

        
    }

    // Update is called once per frame
    void Update()
    {

    }

    List<string[]> ReadCSV(TextAsset file)
    {
        List<string[]> rows = new List<string[]>();
        try
        {
            using (StringReader reader = new StringReader(file.text))
            {
                string line = reader.ReadLine();
                string[] values = line.Split(',');
                rows.Add(values);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
        return rows;
    }

}