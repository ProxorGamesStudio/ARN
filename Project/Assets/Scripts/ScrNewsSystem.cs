using UnityEngine;
using System.Collections.Generic;
using SQLite;

public class NewsDB
{
    public string id { get; set; }
    public string dependies { get; set; }
}

public class NewsTriggerDB
{
    public string id { get; set; }
    public string news { get; set;}
    public string type { get; set; }
    public string parametr { get; set; }
    public float chance { get; set; }
}

public class NewsTrigger
{
    public string id;
    public List<string> News;
    public string type;
    public string parametr;
    public float chance;

    public NewsTrigger(NewsTriggerDB db)
    {
        id = db.id;
        News = ScrLoader.StringToArrayString(db.news);
        type = db.type;
        parametr = db.parametr;
        chance = db.chance;
    }
}

public class StoryTriggerDB
{
    public string id { get; set; }
    public string news { get; set; }
    public string time { get; set; }
    public string dependies { get; set; }
}

public class ScrNewsSystem : MonoBehaviour {
    public static ScrNewsSystem inst;

    public List<NewsDB> AllNews;
    public List<string> loadNews;

    public List<NewsTrigger> triggers;

    void CreateSQLdb()//Одноразовая функция, для создания базы данных.
    {
        var db = new SQLiteConnection(Application.dataPath + "/DataBase.db");

        db.CreateTable<NewsDB>();
        db.CreateTable<NewsTriggerDB>();
        db.CreateTable<StoryTriggerDB>();

        db.Dispose();
    }

    void Awake()
    {
        CreateSQLdb();
        inst = this;
    }

    public void LoadDB()
    {
        var db = new SQLiteConnection(Application.dataPath + "/DataBase.db");

        var paramDB = db.Table<NewsDB>();
        AllNews = new List<NewsDB>(paramDB);

        var paramDB2 = db.Table<NewsTriggerDB>();
        triggers = new List<NewsTrigger>();
        foreach(var t in paramDB2)
        {
            triggers.Add(new NewsTrigger(t));
        }

        db.Dispose();
    }
    
    public void HapEvent(string type, string parametr)
    {
        int it = triggers.FindIndex(x => x.type == type && x.parametr == parametr);
        if(it != -1)
        {
            if(triggers[it].chance < Random.value)
            {
                return;
            }
            foreach(var n in triggers[it].News)
            {
                if(!loadNews.Contains(n) && (loadNews.Contains(AllNews.Find(x => x.id == n).dependies) || AllNews.Find(x => x.id == n).dependies == ""))
                {
                    loadNews.Add(n);
                    break;
                }
            }
        }
    }
}
