using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProxorNetwork;

public class GameManager : MonoBehaviour {

    
    public enum WeAre { FreeAI, GovernmentAI };
    [Header("We play for")]
    public WeAre weAre;

    public enum Scripts { Standart, RandomSeed};
    [Header("Game Script")]
    public Scripts script;
    [Header("")]
    public GameObject Earth;
    public Color ExcuteColor;
    public Texture2D map;
    public int Money, Information, Research;
    public Text moneyUI, informationUI, researchUI;
    public int Defucult;
    GameObject[] hubs;
    public bool startGame;
    #region params
    Vector2 screenSize;
    #endregion

    // Use this for initialization
    void Start() {
        screenSize = new Vector2(Screen.width, Screen.height);
        hubs = GameObject.FindGameObjectsWithTag("Hub");
        if (startGame)
            StartGame();
    }

    // Update is called once per frame
    void Update() {
        UpdateUI();
    }

    void UpdateUI()
    {
        moneyUI.text = Money.ToString();
        informationUI.text = Information.ToString();
        researchUI.text = Research.ToString();
    }

    void StartGame()
    {
        switch(script)
        {
            case Scripts.Standart:
                ScriptsBase.Standart();
                break;

            case Scripts.RandomSeed:
                ScriptsBase.RandomSeed(Earth.transform.position, ExcuteColor, ref hubs, map);
                break;

            default:
                ScriptsBase.Standart();
                break;
        }
    }

    public struct ScriptsBase 
    {
        public static void Standart()
        {

        }

        public static void RandomSeed(Vector3 SphereCenter, Color excuteColor, ref GameObject[] Hubs, Texture2D map)
        {
            GenerateRandom.Generate(SphereCenter, excuteColor, ref Hubs, map);
        }
    }

    public struct GenerateRandom
    {
        public static void Generate(Vector3 SphereCenter, Color excuteColor, ref GameObject[] Hubs, Texture2D map)
        {
            uint i = 0;
            RaycastHit hit;
            List<Pair<uint, uint>> Table = new List<Pair<uint, uint>>();
            GameObject axis = new GameObject("Axis");
            GameObject rayOrign = new GameObject("Ray orign");
            rayOrign.transform.parent = axis.transform;
            rayOrign.transform.position += new Vector3(400, 0,0);
            axis.transform.position = SphereCenter;
            while(i < Hubs.Length)
            {
                Pair<uint, uint> coords = GetRotation(ref Table);
                axis.transform.eulerAngles = new Vector3(0, coords.First, coords.Second);
                Physics.Raycast(rayOrign.transform.position, SphereCenter - rayOrign.transform.position, out hit);
                Color clr = map.GetPixelBilinear(hit.textureCoord.x, hit.textureCoord.y);
                if (clr.b != excuteColor.b && clr.r != excuteColor.r && clr.g != excuteColor.g && hit.transform.tag != "Hub")
                {
                    Hubs[i].transform.GetChild(0).localEulerAngles = Vector3.zero;
                    Hubs[i].transform.position = hit.point;
                    Hubs[i].transform.LookAt(SphereCenter);
                    i++;
                }
            }
            Destroy(axis);
        }

        public static Pair<uint, uint> GetRotation(ref List<Pair<uint, uint>> table)
        {
            uint x = (uint)Random.Range(0, 360), y = (uint)Random.Range(0, 360);
            Pair<uint, uint> random = new Pair<uint, uint>(x, y);
            while (table.FindIndex(p => p == random) != -1)
            {
                x = (uint)new System.Random().Next(0, 360);
                y = (uint)new System.Random().Next(0, 360);
                random = new Pair<uint, uint>(x, y);
            }
            table.Add(random);
            return random;
        }


    }
}
