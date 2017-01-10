using UnityEngine;
using System.Collections.Generic;

using SQLite;

public class GenerateGameParametrsDB
{
    public string ID { get; set; }

    public int startYear { get; set; }
    public int startMonth { get; set; }
    public int startDay { get; set; }
    public int startHour { get; set; }
    public int startMinut { get; set; }

    public string startHubAI { get; set; }
    public string startHubEAI { get; set; }

    public float startValueThreat { get; set; }
    public int startLevelThreat { get; set; }

    public string startResID { get; set; }
    public string startResCount { get; set; }

    public string ServersOnCommonHub { get; set; }
    public string ServersOnComercHub { get; set; }
    public string ServersOnScienceHub { get; set; }
    public string ServersOnMilitaryHub { get; set; }
}

public class ScrGameGenerator : MonoBehaviour {
    public static ScrGameGenerator inst;

    public float Radius = 10f;

    public float[] ServersInCommonHub = new float[4];
    public float[] ServersInComercHub = new float[4];
    public float[] ServersInScienceHub = new float[4];
    public float[] ServersInMilitaryHub = new float[4];

    ScrPieceSystem piece;

    public string StartHub;

    public ScrHub HubPrefab;
    public ScrLine LinePrefab;
  
    private List<HubParametrsDatabase> HubDatabase;

    void Awake()
    {
        inst = this;
       
    }

    public void LoadDB()
    {
        var db = new SQLiteConnection(Application.dataPath + "/DataBase.db");

        var paramDB = db.Table<GenerateGameParametrsDB>();

        GenerateGameParametrsDB param = paramDB.First();

        ScrGlobalTimeManager.Instance.startYear = param.startYear;
        ScrGlobalTimeManager.Instance.startMonth = param.startMonth;
        ScrGlobalTimeManager.Instance.startDay = param.startDay;
        ScrGlobalTimeManager.Instance.startHour = param.startHour;
        ScrGlobalTimeManager.Instance.startMinut = param.startMinut;

        StartHub = param.startHubAI;
        //param.startHubEAI;

        ScrAlertSystem.Inst.StartAlertValue = param.startValueThreat;
        ScrAlertSystem.Inst.StartAlertValue = param.startLevelThreat;

        List<string> bs = ScrLoader.StringToArrayString(param.startResID);
        List<int> bi1 = ScrLoader.StringToArrayInt(param.startResCount);
        int c = 0;
        ScrResesController.inst.StartReses = new GameResStor[bs.Count];
        foreach (var r in bs)
        {
            ScrResesController.inst.StartReses[c] = new GameResStor(r, bi1[c]);
            c++;
        }

        ServersInCommonHub = ScrLoader.StringToArrayFloat(param.ServersOnCommonHub).ToArray();
        ServersInComercHub = ScrLoader.StringToArrayFloat(param.ServersOnComercHub).ToArray();
        ServersInScienceHub = ScrLoader.StringToArrayFloat(param.ServersOnScienceHub).ToArray();
        ServersInMilitaryHub = ScrLoader.StringToArrayFloat(param.ServersOnMilitaryHub).ToArray();

        db.Dispose();

        LoadHubs();
    }

    public void AfterLoadDB()
    {
        PlaceNet();
    }

    void CreateSQLdbGenParam()//Одноразовая функция, для создания базы данных.
    {
        var db = new SQLiteConnection(Application.dataPath + "/Resources/DataBase.db");

        db.CreateTable<GenerateGameParametrsDB>();

        db.Dispose();
    }

    public void CreateSQLdb()//Одноразовая функция, для создания базы данных.
    {
        var db = new SQLiteConnection(Application.dataPath + "/DataBase.db");

        db.CreateTable<HubParametrsDatabase>();

        db.Dispose();
    }

    public void LoadHubs()
    {
        var db = new SQLiteConnection(Application.dataPath + "/DataBase.db");

        var HDB = db.Table<HubParametrsDatabase>();
        HubDatabase = new List<HubParametrsDatabase>();
        print(HDB.Count());
        for(int i = 0; i < HDB.Count(); i++)
        {
            HubDatabase.Add(new HubParametrsDatabase());
            HubDatabase[i] = HDB.ElementAt(i);
        }

        db.Dispose();

        if(!HubDatabase.Exists(x => x.Id == StartHub))
        {
            StartHub = HubDatabase[Random.Range(0, HubDatabase.Count-1)].Id;
            Debug.LogWarning("Заданный в настройках узел в качестве стартового отсутствует в базе данных. Стартовым узлом будет " + StartHub + " .");
        }
    }

    void Start()
    {
        piece = FindObjectOfType<ScrPieceSystem>();
    }

    public void PlaceNet()
    {
        List<ScrHub> hubs = new List<ScrHub>();
        foreach (var h in HubDatabase)
        {
            /***/
            /*определение соседей*/
            h.lLinks = new List<string>(0);
            string ngthbrs = h.Links;
            while (ngthbrs.Length > 0)
            {
                string s = "";
                while (ngthbrs[0] != ' ')
                {
                    s += ngthbrs[0];
                    ngthbrs = ngthbrs.Remove(0, 1);
                    if (ngthbrs.Length == 0)
                        break;
                }
                if (ngthbrs.Length > 0)
                    ngthbrs = ngthbrs.Remove(0, 1);

                if (!HubDatabase.Exists(x => x.Id == s))
                {
                    Debug.LogWarning("Узел с id = " + s + " отсутствует в базе данных. Проверьте параметр Links у узла " + h.Id + ".");
                }
                else
                {
                    h.lLinks.Add(s);
                }
            }
            /***/
            /*вычисление шанса на генерацию узла*/
            float ch = Random.value;
            if (ch >= h.GenerationChance)
            {
                continue;
            }
            /***/
            /*Создание узла*/
            ScrHub bH = Instantiate(HubPrefab);
            bH.Id = h.Id;
            hubs.Add(bH);
            bH.Links = h.lLinks;
            bH.IsCoreEAI = false;
            /***/
            /*Установка координат узла*/
            float Teta = (h.KoordinatX + 90f) * Mathf.Deg2Rad;
            float Fi = h.KoordinatY * Mathf.Deg2Rad;
            float X = Radius * Mathf.Sin(Teta) * Mathf.Cos(Fi);
            float Z = Radius * Mathf.Sin(Teta) * Mathf.Sin(Fi);
            float Y = -Radius * Mathf.Cos(Teta);
            Vector3 NP = new Vector3(X, Y, Z);
            bH.transform.position = NP;
            if (h.Id == StartHub)
            {
                ScrViewControl.inst.ShowPoint(NP);
            }
            /***/
            /*Определение типа узла*/
            int c = 0;
            if (h.TC_Commn + h.TC_Commerce + h.TC_Science + h.TC_Military < 0.99999f)
            {
                Debug.LogError("Сумма вероятностей типов узла " + h.Id + " меньше 1! проверьте базу данных");
                bH.Type = 0;
            }
            else
            {
                ch = Random.value;
                float[] TC_ = { h.TC_Commn, h.TC_Commerce, h.TC_Science, h.TC_Military };
                while (ch > 0)
                {
                    ch -= TC_[c];
                    c++;
                }
                bH.Type = c - 1;
            }
            /***/
            /*определение видимости узла*/
            if (bH.Id != StartHub)
            {
                /*if (h.Id != "hab_austin")*/
                    bH.IsInvis = true;
            }
            /***/
            /*Определение защиты узла*/
            float[] DHC_ = { h.C_DefenceLevel1, h.C_DefenceLevel2, h.C_DefenceLevel3 };
            ch = Random.value;
            c = 0;
            while (ch > 0)
            {
                ch -= DHC_[c];
                c++;
                if (c > 2)
                {
                    c = 1;
                    break;
                }
            }
            bH.DefenceLvl = c;
            float[] tp = ScrNetController.inst.BaseNetParametrs.ProtectChanses;
            bH.Protects = new bool[tp.Length];
            int cp = ScrNetController.inst.BaseNetParametrs.CountProtects[bH.Type];
            for(int i = 0; i < bH.Protects.Length; i++)
            {
                bH.Protects[i] = false;
            }
            int bc = 0;
            while (cp > 0)
            {
                ch = Random.value;
                c = 0;
                while (ch > 0)
                {
                    ch -= DHC_[c];
                    c++;
                    if (c >= tp.Length)
                    {
                        c = 0;
                        break;
                    }
                }
                if(bH.Protects[c])
                {
                    bc++;
                }
                else
                {
                    bH.Protects[c] = true;
                    cp--;
                    bc = 0;
                }
                if(bc>50)
                {
                    break;
                }
            }
            /***/
            /*Определение кол-ва серверов на узле*/
            int cs = Random.Range(h.MinCountServers,h.MaxCountServers+1);
            bH.servers = new Server[cs];
            c = 0;
            while(c<cs)
            {
                bH.servers[c] = new Server();
                bH.servers[c].CoolDown = 0;
                bH.servers[c].trojan = new TrojanInServer();
                bH.servers[c].NeedUpdReses = true;
                c++;
            }
            /***/
            /*Определение типов серверов*/
            foreach (Server srv in bH.servers)
            {
                float[] TSC_;
                switch (bH.Type)
                {
                    case 1:
                        TSC_ = ServersInComercHub;
                        break;
                    case 2:
                        TSC_ = ServersInScienceHub;
                        break;
                    case 3:
                        TSC_ = ServersInMilitaryHub;
                        break;
                    case 0:
                    default:
                        TSC_ = ServersInCommonHub;
                        break;
                }
                ch = Random.value;
                c = -1;
                do
                {
                    c++;
                    ch -= TSC_[c];
                }
                while (ch > 0);
                srv.Type = c;
                /***/
                /*определение уровня сервера*/
                float[] LSC_ = { h.C_ServersLevel1, h.C_ServersLevel2, h.C_ServersLevel3 };
                ch = Random.value;
                c = 0;
                while (ch > 0)
                {
                    ch -= LSC_[c];
                    c++;
                    if (c > 2)
                    {
                        c = 1;
                        break;
                    }
                }
                srv.Lvl = c;
                srv.Defens = c;
            }
        }
        /***/
        /*создание линий между узлами*/
        foreach (ScrHub h in hubs)
        {
            int c = 0;
            while (c < h.Links.Count)
            {
                if (!hubs.Exists(x => x.Id == h.Links[c]))
                {
                    if (!HubDatabase.Exists(x => x.Id == h.Links[c]))
                    {
                        continue;
                    }
                    else
                    {
                        foreach (string bh in HubDatabase.Find(x => x.Id == h.Links[c]).lLinks)
                        {
                            if (!h.Links.Contains(bh) && bh != h.Id)
                            {
                                h.Links.Add(bh);
                                Debug.Log("New link in " + h.Id);
                            }
                        }
                    }
                }
                c++;
            }
        }
        foreach (ScrHub h in hubs)
        {
            int c = h.Links.Count - 1;
            while (c >= 0)
            {
                if (!hubs.Exists(x => x.Id == h.Links[c]))
                {
                    Debug.Log("Remove " + h.Links[c] + " at " + h.Id);
                    h.Links.RemoveAt(c);
                }
                c--;
            }
        }
        foreach (ScrHub h in hubs)
        {
            foreach (string s in h.Links)
            {
                ScrHub nh = hubs.Find(x => x.Id == s);
                if (h.Lines.Exists(x => x.GetAnother(h.transform) == nh.transform))
                {
                    continue;
                }
                else
                {
                    ScrLine nL = Instantiate(LinePrefab);
                    nL.From = h.transform;
                    nL.To = nh.transform;
                    h.Lines.Add(nL);
                    nh.Lines.Add(nL);
                    nL.IsVisibl = false;
                }
            }
        }
        if(!hubs.Exists(x => x.Id == StartHub))
        {
            StartHub = HubDatabase[Random.Range(0, HubDatabase.Count - 1)].Id;
            Debug.LogWarning("Заданный в настройках узел в качестве стартового не был сгенерирован, возможно вероятность его генерации меньше 1. Стартовым узлом будет " + StartHub + " .");
        }
        piece.startHub = hubs.Find(x => x.Id == StartHub);
        FindObjectOfType<ScrViewControl>().ShowPoint(piece.startHub.transform.position);
        ScrAI.inst.GenerateGame(hubs.Find(x => x.Id == StartHub));
    }
}
