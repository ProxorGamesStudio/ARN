using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GameResStor
{
    public string Id;
    public int count;

    public GameResStor()
    {
        Id = "";
        count = 0;
    }
    public GameResStor(string id, int cnt)
    {
        Id = id;
        count = cnt;
    }
}

public class ScrResesController : MonoBehaviour {
    public static ScrResesController inst;

    public GameResStor[] StartReses;

    public ScrUIResesViewer Viewer;

    public List<GameResStor> Reses;

    void Awake()
    {
        inst = this;
    }

    public void AfterLoadDB()
    {
        Reses = new List<GameResStor>();
        foreach (GameResStor GRS in StartReses)
        {
            Reses.Add(GRS);
        }
        UpdtViewer();
    }

    public int GetResInfo(string Id)
    {
        int r = 0;

        if(Reses.Exists(x => x.Id == Id))
        {
            r = Reses.Find(x => x.Id == Id).count;
        }

        return r;
    }

    public void AddRes(GameResStor res)
    {
        if (Reses.Exists(x => x.Id == res.Id))
        {
            Reses.Find(x => x.Id == res.Id).count += res.count;

        }
        UpdtViewer();
    }

    public bool TakeRes(GameResStor res)
    {
        if (Reses.Exists(x => x.Id == res.Id))
        {
            if(Reses.Find(x => x.Id == res.Id).count >= res.count)
            {
                Reses.Find(x => x.Id == res.Id).count -= res.count;
                UpdtViewer();
                return true;
            }
        }
        UpdtViewer();
        return false;
    }

    public void UpdtViewer()
    {
        int c = 0;
        foreach(GameResStor GRS in Reses)
        {
            Viewer.txs[c].text = GRS.count.ToString();//пере
            c++;
            if(c >= Viewer.txs.Length)
            {
                break;
            }
        }
    }
}
