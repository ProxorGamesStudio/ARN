using UnityEngine;
using System;
using System.Collections;

public class ScrGlobalTimeManager : MonoBehaviour
{
    public static ScrGlobalTimeManager Instance { get; set; }

    public int startYear = 2016;
    public int startMonth = 4;
    public int startDay = 8;
    public int startHour = 12;
    public int startMinut = 33;

    public int SecondsInHour = 30;
    public float[] TimeMods = new[] { 0.0f, 0.5f, 1.0f, 2.5f };
    
    private DateTime TimeInGame;
    private float Add;//в минутах

    void Awake()
    {
        Instance = this;
    }

    public void AfterLoadDB()
    {
        TimeInGame = System.DateTime.Now;
    }

	void Start () {
        Time.timeScale = TimeMods[2];
        Add = 0;
    }
	
	void Update ()
	{
        Add += Time.deltaTime * 60 / 30;
        while (Add > 1f)
        {
            TimeInGame = TimeInGame.AddMinutes(1);
            Add -= 1f;
        }
	}

    public void SetTimeMode(int idMode)
    {
        if (idMode > -1 && idMode < TimeMods.Length)
            Time.timeScale = TimeMods[idMode];
    }

    public DateTime GetDate()
    {
        return TimeInGame;
    }

    public string GetDateStr()
    {
        return TimeInGame.ToString("dd MMM HH:mm");
    }
}
