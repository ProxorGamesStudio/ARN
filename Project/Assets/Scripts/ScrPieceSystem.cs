using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Piece
{
    public string id;
    public float Param;
    public int distance, depth;

    public Piece(PieceOfGameEvent pge)
    {
        id = pge.Id;
        Param = pge.Parametr;
        distance = pge.distance;
        depth = pge.depth;
    }
}

public class PieceOnHubs
{
    public Piece p;
    public List<ScrHub> hubs;
}

public class ScrPieceSystem : MonoBehaviour {

    ScrEAI eAI;
    public ScrHub[] allHubs;
    public int ev_hub_level_change_Count = 3;
    [HideInInspector]
    public int ev_hub_level_change_Count_now = 0;
    [HideInInspector]
    public int ev_resources_increase_Count;
    public List<ScrHub> test = new List<ScrHub>();
    public GameObject cube1, cube2, cube3;
    public ScrHub startHub;
    float timer;
    bool active = false;
    void Awake()
    {
        
    }

    void Start () {
        eAI = FindObjectOfType<ScrEAI>();
        allHubs = FindObjectsOfType<ScrHub>();
        test = FindNeedHubs(allHubs[0], 0, 3);
	}
	
	void Update () {
        if(timer > 0)
            timer -= Time.deltaTime;       
        else
        {
            if(active)
            {
                GameObject.FindObjectOfType<ScrEAI>().enabled = true;
                GameObject.Find("HubUnderAttack").SetActive(true);
                active = false;
            }
        }
    }

    public void PieceFunctionOn(PieceOfGameEvent pge, float lifeTime)
    {
        PieceFunctionOn(new Piece(pge), lifeTime);
    }

    public static void PieceFunctionOff(PieceOfGameEvent pge)
    {
        PieceFunctionOff(new Piece(pge));
    }

    public void PieceFunctionOn(Piece p, float lifeTime)
    {

        switch (p.id)
        {
            
            default:
                Debug.LogWarning("Не реализованый кирпичик: " + p.id);
                break;
            case "#ev_eai_blind":
                Debug.LogWarning("Был запущен кирпичик: " + p.id);
                eAI.blindTime = eAI.blindTimeDown = p.Param;
                eAI.blind = true;
                break;
            case "#ev_hub_level_change":
                
                foreach (ScrHub hs in FindNeedHubs(startHub, p.distance, p.depth))
                {
                    hs.ev_hub_level_change = p.Param;
                    hs.ev_hub_level_change_time = hs.tDown = lifeTime;
                    hs.pieceText += " " + p.id;
                    hs.ev_hub_level_change_active = true;
                }
                break;
            case "#ev_resources_increase":
                Debug.LogWarning("Был запущен кирпичик: " + p.id);
                foreach (ScrHub hs in FindNeedHubs(startHub, p.distance, p.depth))
                {
                    hs.ev_resources_increase = (int)p.Param;
                    hs.ev_resources_increase_time = hs.tDown2 = lifeTime;
                    hs.pieceText += " " + p.id;
                    hs.ev_resources_increase_active = true;
                }
                break;
            case "#ev_threat_decrease":
                Debug.LogWarning("Был запущен кирпичик: " + p.id);
                foreach (ScrHub hs in FindNeedHubs(startHub, p.distance, p.depth))
                {
                    hs.ev_threat_decrease = (int)p.Param;
                    hs.ev_threat_decrease_time = hs.tDown3 = lifeTime;
                    hs.pieceText += " " + p.id;
                    hs.ev_threat_decrease_active = true;
                }
                break;
            case "#ev_eai_base":
                Debug.LogWarning("Был запущен кирпичик: " + p.id);
                test = FindNeedHubs(eAI.eai_base, p.distance, p.depth);
                foreach (ScrHub hs in FindNeedHubs(eAI.eai_base, p.distance, p.depth))
                {
                    test[0] = hs;
                    hs.ev_eai_base = (int)p.Param;
                    hs.ev_eai_base_time = hs.tDown4 = lifeTime;
                    hs.pieceText += " " + p.id;
                    hs.ev_eai_base_active = true;
                }
                break;
            case "#ev_eai_base_find":
                Debug.LogWarning("Был запущен кирпичик: " + p.id);
                eAI.eai_base.pieceText += " " + p.id;
                eAI.eai_base.ev_eai_base_find = 1;
                break;
            case "#ev_hub_cooldown":
                Debug.LogWarning("Был запущен кирпичик: " + p.id);
                foreach (ScrHub hs in FindNeedHubs(eAI.eai_base, p.distance, p.depth))
                    hs.Ungrab();
                
                break;
            case "#ev_eai_scan":
                Debug.LogWarning("Был запущен кирпичик: " + p.id);
                eAI.ev_eai_scan_hub = FindNeedHubs(startHub, p.distance, p.depth)[Random.Range(0, FindNeedHubs(startHub, p.distance, p.depth).Count)];
                eAI.ev_eai_scan_active = true;
                eAI.eai_base.pieceText += " " + p.id;
                break;
            case "#ev_eai_trick":
                Debug.LogWarning("Был запущен кирпичик: " + p.id);
                GameObject.FindObjectOfType<ScrEAI>().enabled = false;
                timer = lifeTime;
                active = true;
                break;
            case "#ev_eai_attack":
                Debug.LogWarning("Был запущен кирпичик: " + p.id);
                GameObject.FindObjectOfType<ScrEAI>().enabled = false;
                GameObject.Find("HubUnderAttack").SetActive(false);
                active = true;
                timer = lifeTime;
                break;
        }
    }

    public List<ScrHub> FindNeedHubs(ScrHub startHub, int distance, int depth)
    {
        List<ScrHub> result = new List<ScrHub>();
        for(int i = 0; i < distance; i++)
        {
            string toHub = startHub.Links[Random.Range(0, startHub.Links.Count)];
            foreach (ScrHub h in allHubs)
                if (h.Id == toHub) startHub = h;
        }
        result.AddRange(NodeHubs(startHub, depth));        
        return result;
    }

    List<ScrHub> NodeHubs(ScrHub startHub, int depth)
    {
        List<ScrHub> result = new List<ScrHub>();
        if (depth == 0)
        {
            for (int i = 0; i < startHub.Links.Count - Random.Range(1, startHub.Links.Count); i++)
            {
                string toHub = startHub.Links[Random.Range(0, startHub.Links.Count)];
                foreach (ScrHub h in allHubs)
                    if (h.Id == toHub) result.Add(h);
            }
        }
        if(depth > 0)
        {
            result.Add(startHub);
            for (int i = 0; i < startHub.Links.Count - Random.Range(1, startHub.Links.Count); i++)
            {
                string toHub = startHub.Links[Random.Range(0, startHub.Links.Count)];
                foreach (ScrHub h in allHubs)
                    if (h.Id == toHub) result.AddRange(NodeHubs(h, depth - 1));
            }
            
        }
        return result;
    }

    public static void PieceFunctionOff(Piece p)
    {
        switch (p.id)
        {
            default:
                Debug.LogWarning("Не реализованый кирпичик: " + p.id);
                break;
          
        }
    }

    
}
