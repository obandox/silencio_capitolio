using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public static class Log
{
    public static int ShowLevel = 4;
    public static int TraceLevel = 3;

    public static bool UseWeb = false;
    
    private static string[] Levels = { "Fatal", "Error", "Warning", "Info" };

    public static void Error(object output)
    {
        Entry(1, output.ToString());
    }
	public static void Danger(object output)
    {
		Entry(2, output.ToString());
    }
	public static void Warning(object output)
    {
		Entry(3, output.ToString());
    }
	public static void Info(object output)
    {
		Entry(4, output.ToString());
    }
	
	public static T[] Slice<T>(this T[] arr, int indexFrom, int LengthTo = -1)
	{
		if (LengthTo < 0)
		{
			LengthTo = arr.Length - LengthTo;
		}		
		if (indexFrom < 0)
		{
			indexFrom = arr.Length - indexFrom;
		}
		if (LengthTo > (arr.Length - indexFrom))
		{
			LengthTo = (arr.Length - indexFrom);
		}		
		if (LengthTo < 0) return new T[0];
		T[] result = new T[LengthTo];

		Array.Copy(arr, indexFrom, result, 0, LengthTo);

		return result;
	}
	
	public static string Join(string[] array, string glue)
	{
		string builder = "";
		foreach (string value in array)
		{
			builder += glue + value;
		}
		if(builder.Length > 0)
			builder = builder.Substring(glue.Length);
		return builder;
	}

    public static void Entry(uint level, string log)
    {
        if (ShowLevel < level) return;

        string[] trace = Slice(Environment.StackTrace.Split('\n'), 4, TraceLevel);

		PrintLn(Levels[level - 1] + ": " + DateTime.Now.ToString("dddd, MMMM dd, yyyy h:mm:ss tt"), log , " << "+ Join(trace, " << "));
    }

    public static Coroutine StartCoroutine(IEnumerator iterationResult) {
        //Create GameObject with MonoBehaviour to handle task.
        GameObject routeneHandlerGo = new GameObject("CoroutinerLog");
        CoroutinerInstance routeneHandler
                = routeneHandlerGo.AddComponent(typeof(CoroutinerInstance))
                                                        as CoroutinerInstance;
        return routeneHandler.ProcessWork(iterationResult);
    }

    public static void PrintLn(string header , string content, string trace)
    {
		int currentShowLevel = ShowLevel;
		ShowLevel = 0;
		Debug.Log(content);
        //System.Diagnostics.Debug.WriteLine(output);
        WriteFileLog(header , content, trace);
        if(UseWeb){
			StartCoroutine(SendToServer(header , content, trace));
        }
        ShowLevel = currentShowLevel;
    }

    public static IEnumerator SendToServer(string header , string content, string trace){
    	    WWWForm form = new WWWForm ();			
			form.AddField ("header", header);		
			form.AddField ("content", content);		
			form.AddField ("trace", trace);			
			WWW connection = new WWW ("http://localhost/debugLog", form);
			yield return connection;
    }
    public static void WriteFileLog(string header , string content, string trace)
    {
        try
        {   string output = header+": "+content+" Trace: "+trace;
			string logFile =  Application.dataPath+"/data.log";
            logFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + logFile;

            using (StreamWriter w = File.AppendText(logFile))
            {
                w.WriteLine(output);
            }
        }
        catch (Exception)
        {
            //unused
        }
    }
}

public class CoroutinerInstance : MonoBehaviour {

    public Coroutine ProcessWork(IEnumerator iterationResult) {
        return StartCoroutine(DestroyWhenComplete(iterationResult));
    }

    public IEnumerator DestroyWhenComplete(IEnumerator iterationResult) {
        yield return StartCoroutine(iterationResult);
        Destroy(gameObject);
    }

}