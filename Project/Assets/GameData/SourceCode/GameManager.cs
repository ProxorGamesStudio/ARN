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
    public GameObject LinePrefab;

    [Header("")]
    public bool startGame;
    public int Money, Information, Research;
    public Text moneyUI, informationUI, researchUI;
    public int Defucult;
    GameObject[] hubs;
    
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
                ScriptsBase.RandomSeed(Earth.transform.position, ExcuteColor, ref hubs, map, LinePrefab);
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

        public static void RandomSeed(Vector3 SphereCenter, Color excuteColor, ref GameObject[] Hubs, Texture2D map, GameObject LinePrefab)
        {
            GenerateRandom.Generate(SphereCenter, excuteColor, ref Hubs, map, LinePrefab);
        }
    }

    public struct GenerateRandom
    {
        public static void Generate(Vector3 SphereCenter, Color excuteColor, ref GameObject[] Hubs, Texture2D map, GameObject LinePrefab)
        {
            uint i = 0;
            RaycastHit hit;
            List<Triple<uint, uint, GameObject>> Table = new List<Triple<uint, uint, GameObject>>();
            GameObject axisX = new GameObject("AxisX");
            GameObject axisY = new GameObject("AxisY");
            GameObject rayOrign = new GameObject("Ray orign");
            Destroy(GameObject.Find("AllLines"));
            rayOrign.transform.parent = axisX.transform;
            axisX.transform.parent = axisY.transform;
            rayOrign.transform.position += new Vector3(400, 0, 0);
            axisY.transform.position = SphereCenter;
            axisX.transform.position = SphereCenter;
            while (i < Hubs.Length)
            {
                Triple<uint, uint, GameObject> coords = GetRotation(ref Table);
                axisX.transform.eulerAngles = new Vector3(0, 0, coords.First);
                axisY.transform.eulerAngles = new Vector3(0, coords.Second, 0);
                Physics.Raycast(rayOrign.transform.position, SphereCenter - rayOrign.transform.position, out hit);
                Color clr = map.GetPixelBilinear(hit.textureCoord.x, hit.textureCoord.y);
                if (clr.b != excuteColor.b && clr.r != excuteColor.r && clr.g != excuteColor.g && hit.transform.tag != "Hub")
                {
                    Hubs[i].transform.GetChild(0).localEulerAngles = Vector3.zero;
                    Hubs[i].transform.position = hit.point + (rayOrign.transform.position - hit.point).normalized * 0.6f;
                    Hubs[i].transform.LookAt(SphereCenter);
                    Table[(int)i].Threeth = Hubs[i].gameObject;
                    i++;
                }
                else
                    Table.RemoveAt((int)i);
                Swap(ref Hubs[Random.Range(0, Hubs.Length)], ref Hubs[Random.Range(0, Hubs.Length)]);
            }
            GenerateLines(Table, LinePrefab);
            Destroy(axisY);
        }

        public static Triple<uint, uint, GameObject> GetRotation(ref List<Triple<uint, uint, GameObject>> table)
        {
            uint x = (uint)Random.Range(-55, 55), y = (uint)Random.Range(0, 360);
            Triple<uint, uint, GameObject> random = new Triple<uint, uint, GameObject>(x, y, null);
            while (table.FindIndex(p => p == random) != -1)
            {
                x = (uint)Random.Range(-55, 55);
                y = (uint)Random.Range(0, 360);
                random = new Triple<uint, uint, GameObject>(x, y, null);
            }
            table.Add(random);
            return random;
        }

        public static void Swap(ref GameObject first, ref GameObject second)
        {
            if(first != second)
            {
                Hub temp = second.GetComponent<Hub>();
                second.GetComponent<Hub>().Name = first.GetComponent<Hub>().Name;
                second.GetComponent<Hub>().lines = first.GetComponent<Hub>().lines;
                second.GetComponent<Hub>().links = first.GetComponent<Hub>().links;
                second.GetComponent<Hub>().Level = first.GetComponent<Hub>().Level;
                first.GetComponent<Hub>().Name = temp.Name;
                first.GetComponent<Hub>().lines = temp.lines;
                first.GetComponent<Hub>().links = temp.links;
                first.GetComponent<Hub>().Level = temp.Level;
            }
        }

        public static void GenerateLines(List<Triple<uint, uint, GameObject>> Table, GameObject linePrefab)
        {

            //Внимание! Данный участок кода требуется ПОЛНОСТЬЮ переделать
            //неэффективен по расходу памяти и времени работы!
            for (byte i = 0; i < Table.Count; i++)
            {
                SortedList<float, GameObject> mass = new SortedList<float, GameObject>(Table.Count);
                int targetCount = 3;
                for(byte k = 0; k < Table.Count; k++)
                {
                    if (i != k)
                    {
                        float distance = ProxorEngine.Math.arcDistance(90, Table[k].Threeth.transform.position, Table[i].Threeth.transform.position);
                        if(!mass.ContainsKey(distance))
                           mass.Add(distance, Table[k].Threeth);
                    }
                   
                }
                for (byte z = 0; z < targetCount; z++)
                    GenerateLine(Table[i].Threeth.transform.position, mass.Values[z].transform.position, linePrefab);

            }
        }

        public static void GenerateLine(Vector3 from, Vector3 to, GameObject linePrefab)
        {
            LineRenderer lineRenderer = Instantiate(linePrefab).GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, from);
            uint N = 2;
            if (Vector3.Angle(from, to) > 0.1f)
            {
                float Ang = Vector3.Angle(from, to);
                uint n = (uint)(Ang / 0.1f) - 1;
                N = n + 2;
                lineRenderer.positionCount = (int)N;
                for (int i = 1; i < n + 1; i++)
                {
                    Vector3 buf;
                    buf = Vector3.RotateTowards(from, to, 0.1f * i / 57, 0);
                    float k;
                    if (i < n - i)
                    {
                        k = Mathf.Sqrt(Mathf.Sqrt(n * 150 * i)) * 0.175f;
                    }
                    else
                    {
                        k = Mathf.Sqrt(Mathf.Sqrt(n * 150 * (n - i))) * 0.175f;
                    }
                    k -= 18.5f;
                    if (k > 2)
                    {
                        k = 2;
                    }
                    buf = buf.normalized * (190 + k);

                    lineRenderer.SetPosition(i, buf);
                }
            }
            lineRenderer.SetPosition((int)N - 1, to);
        }
    }
}
