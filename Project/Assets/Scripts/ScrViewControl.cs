using UnityEngine;
using System.Collections;

public class ScrViewControl : MonoBehaviour {

    public static ScrViewControl inst;

    public float ZoomSpeed = 5f; //скорость приближения/отдаления
    public float RotationSpeed = 0.1f; //скорость вращения
    public float MinZoom = -100f; //минимальное расстояние до центра
    public float MaxZoom = -50f; //максимальное расстояние до центра
    public float NorthBorder = 85f;
    public float SouthBorder = 85f;

    public Camera cam;
    public Transform RotAxis;
    public Transform PointOfView;
    public Transform SecondAxis;
    public float LerpRotationSpeed = 2;
    Rect rect = new Rect(0, Screen.height - Screen.height / 4, Screen.width / 4, Screen.height / 4);
    bool deScroll;

    public void DeScrool(bool _deScroll)
    {
        deScroll = _deScroll;
    }

    void Awake()
    {
        inst = this;
    }

	void Start () {
        if(cam==null)
        {
            cam = Camera.main;
        }
        if(PointOfView == null)
        {
            PointOfView = transform;
        }
        if(SecondAxis == null)
        {
            SecondAxis = PointOfView.FindChild("SecondAxis");
            if(SecondAxis == null)
            {
                Debug.LogError("Ошибка инициализации контроля камеры");
                return;
            }
        }


        RotAxis.transform.localPosition = new Vector3(0, 0, (MinZoom + MaxZoom) / 2);
    }

    void Update()
    {
        if (Time.timeScale != 0  && !deScroll)
        {
            RotAxis.transform.LookAt(Vector3.zero);

            if (Input.GetMouseButton(1))
            {
                PointOfView.Rotate(0, Input.GetAxis("Mouse X") * RotationSpeed, 0, Space.Self);
                float YR = -Input.GetAxis("Mouse Y") * RotationSpeed;
                if ((YR > 0 && Vector3.Angle(Vector3.up, RotAxis.transform.position) > 90 - NorthBorder) || (YR < 0 && Vector3.Angle(Vector3.down, RotAxis.transform.position) > 90 - SouthBorder))
                    /*PointOfView.Rotate(YR, 0, 0, Space.Self);*/
                    SecondAxis.Rotate(YR, 0, 0, Space.Self);
            }
            if (!rect.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
            {
                RotAxis.transform.Translate(0, 0, Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed);
                if (RotAxis.transform.localPosition.z < MinZoom) RotAxis.transform.localPosition = new Vector3(0, 0, MinZoom);
                if (RotAxis.transform.localPosition.z > MaxZoom) RotAxis.transform.localPosition = new Vector3(0, 0, MaxZoom);
            }
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, RotAxis.rotation, LerpRotationSpeed * Time.deltaTime);
            cam.transform.position = Vector3.Lerp(cam.transform.position, RotAxis.position, LerpRotationSpeed * Time.deltaTime);
        }
    }

    public void ShowPoint(Vector3 trgt)
    {
        RotAxis.transform.localPosition = new Vector3(0, 0, MaxZoom);
        //PointOfView.LookAt(-trgt);
        //PointOfView.rotation = Quaternion.Euler(0, PointOfView.rotation.eulerAngles.y, 0);
        SecondAxis.LookAt(-trgt);
        //SecondAxis.rotation = Quaternion.Euler(SecondAxis.rotation.eulerAngles.x,0,0);
    }
}
