using UnityEngine;
using System.Collections.Generic;

using SQLite;

public enum Language
{
    Non = 0,
    Russian = 1,
    English = 2,
}

[System.Serializable]
public class LocalizationStringDB
{
    [PrimaryKey, Unique]
    public string Id { get; set; }

    public string Description { get; set; }

    public string Russian { get; set; }
    public string English { get; set; }
}

[System.Serializable]
public class LocalizationString
{
    public string Id;
    public string String;

    public LocalizationString()
    {
        Id = "";
        String = "";
    }

    public LocalizationString(LocalizationStringDB db)
    {
        Id = db.Id;
        String = db.Description;
    }

    public LocalizationString(LocalizationStringDB db, Language l)
    {
        Id = db.Id;
        switch(l)
        {
            case Language.Russian:
                String = db.Russian;
                break;
            case Language.English:
                String = db.English;
                break;
            case Language.Non:
            default:
                String = db.Description;
                break;
        }
    }
}

public class ScrLocalizationSystem : MonoBehaviour
{
    public static ScrLocalizationSystem inst;

    public Language lang;

    public char Key = '#';

    [Header("Ключи локализазии")]
    [Tooltip("сообщение о нехватке ресурсов")]
    public string msg_defuse_res = "#msg_insuf_res";
    [Tooltip("сообщение о нештатном завершении работы трояна")]
    public string msg_usdt = "#msg_ustd";
    [Tooltip("сообщение о попытке удалить троян")]
    public string msg_failed_usdt = "#msg_failed_usdt";
    [Tooltip("сообщение о повышении уровня угрозы")]
    public string msgThreatLvlUp = "#msg_threat_lvl_up";
    [Tooltip("сообщение о достижении максимального уровня угрозы")]
    public string msgThreatLvlMax = "#msg_threat_lvl_max";
    [Tooltip("сообщение об обнаружении вражеским ИИ нашего ИИ")]
    public string msgHubAnderAttack = "#msg_hub_under_attack";
    [Tooltip("сообщение об аткеи вражеским ИИ ядра нашего ИИ")]
    public string msgCoreAnderAttack = "#msg_base_under_attack";
    [Tooltip("сообщение о максимальном количестве работающих троянов")]
    public string msgMaxTrojans = "#msg_max_count_trojan";
    [Tooltip("сообщение о провале захвата узла")]
    public string msgGrabHubFail = "#msg_grab_hub_fail";
    [Tooltip("сообщение о том, что создаваемый вирус похож на уже существующий")]
    public string msgSimilarVirus = "#msg_similar_virus";
    [Tooltip("сообщение о деактивации вируса")]
    public string msgDeactivateVirus = "#msg_deactivate_virus";
    [Tooltip("сообщение о потере узла")]
    public string msgUngrabHub = "#msg_ungrab_hub";

    [Tooltip("типы узлов/серверов")]
    public string[] idsLocaltype = { "#type_common", "#type_comerc", "#type_science", "#type_military" };
    [Tooltip("типы троянов")]
    public string[] idsLocaltrojantype = { "#trojan_type_resourse" };
    [Tooltip("типы защит узлов")]
    public string[] idsHubProtectType = { "#hub_protect_type_antivirus", "#hub_protect_type_firewall", "hub_protect_type_admincontroll" };

    [Tooltip("Надпись - таймер сканирования ВИИ")]
    public string TimerEAIScan = "#lbl_eai_scan_timer";
    [Tooltip("Надпись на кнопке активации эвента")]
    public string ActivatePosEvent = "#Btn_Activate_Positive_Event";
    [Tooltip("Надпись на кнопке деактивации эвента")]
    public string DeactivNegEvent = "#Btn_Deactivate_Negative_Event";
    [Tooltip("Надпись - таймер жизни неактивированого эвента")]
    public string TimeToDieEvent = "#btn_event_timer";
    [Tooltip("Универсальная надпись 'тип'")]
    public string LblType = "#lbl_type";
    [Tooltip("Универсальная надпись 'уровень'")]
    public string LblLvl = "#lbl_lvl";
    [Tooltip("Универсальная надпись 'заметность'")]
    public string LblDisq = "#lbl_disq";
    [Tooltip("Универсальная надпись 'стоимость'")]
    public string LblCost = "#lbl_cost";
    [Tooltip("Универсальная надпись 'неизвестно'")]
    public string LblUnknow = "#lbl_unknow";
    [Tooltip("Надпись - индикатор местонахождения ядра ИИ")]
    public string AICoreHere = "#lbl_AIcore_here";
    [Tooltip("Надпись - индикатор местонахождения ядра ВИИ")]
    public string EAICoreHere = "#lbl_EAIcore_here";
    [Tooltip("Надпись - таймер перемещения ИИ")]
    public string TimerAICoreRemove = "#lbl_aicore_remove";
    [Tooltip("Надпись - таймер перезарядки перемещения ИИ")]
    public string TimerAICoreRemoveCooldown = "#lbl_aicore_remove_cd";
    [Tooltip("Надпись - 'состояние' для серверов")]
    public string LblState = "#lbl_serv_state";
    [Tooltip("Надпись - индикатор состояния(работает) для серверов")]
    public string IdStNorm = "#serv_state_norm";
    [Tooltip("Надпись - индикатор состояния(заражен) для серверов")]
    public string IdStInfe = "#serv_state_infected";
    [Tooltip("Надпись - индикатор состояния(перезагружается) для серверов")]
    public string IdStCD = "#serv_state_cd";
    [Tooltip("Универсальная надпись 'Время захвата'")]
    public string LblGrabTime = "#lbl_grab_time";
    [Tooltip("Универсальная надпись 'угроза'")]
    public string LblThreat = "#lbl_threat";
    [Tooltip("надпись 'дополнительные захватываемые узлы' для вирусов")]
    public string LblAddGrabHub = "#lbl_addgrabhub";

    [Tooltip("Надпись на кнопке сканирования узла")]
    public string LblBtnScan = "#btn_scan";
    [Tooltip("Надпись на кнопке захвата узла")]
    public string LblBtnGrab = "#btn_grab";
    [Tooltip("Надпись на кнопке атаки на узел ВИИ")]
    public string LblBtnAttack = "#btn_attack";
    [Tooltip("Надпись на кнопке показа установленного вируса")]
    public string LblBtnVirus = "#btn_virus";

    [Tooltip("Ресурсный кирпичик")]
    public string trojan_res_ability = "#trojan_res_ability";
    [Tooltip("Обноружаемый кирпичик")]
    public string trojan_found_ability = "#trojan_found_ability";
    [Tooltip("Выдачный кирпичик")]
    public string trojan_trick_ability = "#trojan_trick_ability";
    [Tooltip("Живучий кирпичик")]
    public string trojan_hook_ability = "#trojan_hook_ability";
    [Tooltip("Скоростной кирпичик")]
    public string trojan_time_ability = "#trojan_time_ability";
    [Tooltip("Угрозный кирпичик")]
    public string trojan_threat_ability = "#trojan_threat_ability";

    [Tooltip("Троян добытчик")]
    public string trojan_resources = "#trojan_resources";
    [Tooltip("Троян протектор")]
    public string trojan_protector = "#trojan_protector";
    [Tooltip("Троян трикер")]
    public string trojan_tricker = "#trojan_tricker";
    [Tooltip("Троян хукер")]
    public string trojan_hooker = "#trojan_hooker";
    [Tooltip("Троян шустрый")]
    public string trojan_speedy = "#trojan_speedy";

    public static string L_trojan_res_ability;
    public static string L_trojan_found_ability;
    public static string L_trojan_trick_ability;
    public static string L_trojan_hook_ability;
    public static string L_trojan_time_ability;
    public static string L_trojan_threat_ability;

    public static string L_trojan_resources;
    public static string L_trojan_protector;
    public static string L_trojan_tricker;
    public static string L_trojan_hooker;
    public static string L_trojan_speedy;

    public static string L_msg_defuse_res;
    public static string L_msg_usdt;
    public static string L_msg_failed_usdt;
    public static string L_msgThreatLvlUp;
    public static string L_msgThreatLvlMax;
    public static string L_msgHubAnderAttack;
    public static string L_msgCoreAnderAttack;
    public static string L_msgMaxTrojans;
    public static string L_msgGrabHubFail;
    public static string L_msgSimilarVirus;
    public static string L_msgDeactivateVirus;
    public static string L_msgUngrabHub;

    public static string[] L_idsLocaltype;
    public static string[] L_idsLocaltrojantype;
    public static string[] L_idsHubProtectType;

    public static string L_TimerEAIScan;
    public static string L_ActivatePosEvent;
    public static string L_DeactivNegEvent;
    public static string L_TimeToDieEvent;
    public static string L_LblType;
    public static string L_LblCost;
    public static string L_LblLvl;
    public static string L_LblDisq;
    public static string L_LblUnknow;
    public static string L_AICoreHere;
    public static string L_EAICoreHere;
    public static string L_TimerAICoreRemove;
    public static string L_TimerAICoreRemoveCooldown;
    public static string L_LblState;
    public static string L_IdStNorm;
    public static string L_IdStInfe;
    public static string L_IdStCD;
    public static string L_LblGrabTime;
    public static string L_LblThreat;
    public static string L_LblAddGrabHub;

    public static string L_LblBtnScan;
    public static string L_LblBtnGrab;
    public static string L_LblBtnAttack;
    public static string L_LblBtnVirus;

    private List<LocalizationString> localization;

    public void CreateSQLdb()//Одноразовая функция, для создания базы данных.
    {
        var db = new SQLiteConnection(Application.dataPath + "/Resources/DataBase.db");

        db.CreateTable<LocalizationStringDB>();

        db.Dispose();
    }

    void Awake()
    {
        inst = this;

    }

    public void LoadDB()
    {
        var db = new SQLiteConnection(Application.dataPath + "/DataBase.db");

        var LDB = db.Table<LocalizationStringDB>();
        localization = new List<LocalizationString>();

        int c = 0;
        foreach (var ls in LDB)
        {
            localization.Add(new LocalizationString(ls, lang));
            if (localization[c].String == null || localization[c].String == "")
            {
                Debug.LogWarning("Пустая строка в таблице локализации!! Номер строки: " + (c + 1).ToString() + ", индентификатор: " + ls.Id + ", язык: " + lang.ToString());
            }
            c++;
        }

        db.Dispose();
    }

    public void AfterLoadDB()
    {
        L_LblGrabTime = GetLocalString(LblGrabTime);
        L_LblThreat = GetLocalString(LblThreat);
        L_LblCost = GetLocalString(LblCost);
        L_LblDisq = GetLocalString(LblDisq);
        L_LblAddGrabHub = GetLocalString(LblAddGrabHub);
        L_msgThreatLvlUp = GetLocalString(msgThreatLvlUp);
        L_msgThreatLvlMax = GetLocalString(msgThreatLvlMax);
        L_msgSimilarVirus = GetLocalString(msgSimilarVirus);
        L_msgDeactivateVirus = GetLocalString(msgDeactivateVirus);
        L_msgUngrabHub = GetLocalString(msgUngrabHub);
        L_idsHubProtectType = new string[3];
        L_idsHubProtectType[0] = GetLocalString(idsHubProtectType[0]);
        L_idsHubProtectType[1] = GetLocalString(idsHubProtectType[1]);
        L_idsHubProtectType[2] = GetLocalString(idsHubProtectType[2]);
    }

    public static string GetLocalString(string id)
    {
        if(inst.localization != null)
        if (!inst.localization.Exists(x => x.Id == id))
        {
            Debug.LogWarning("Было обращение к отсутствующему в базе данных индентификатору локализации: "+ id);
            return id;
        }
        if (inst.localization != null)
            return inst.localization.Find(x => x.Id == id).String;
        else return "";
    }
}
