using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float m_NormalSpeed;
    [SerializeField] private float m_FastSpeed;
    [SerializeField] private float m_MovementSpeed;
    [SerializeField] private float m_MovementTime;
    [SerializeField] private float m_RotationAmount;
    [SerializeField] private Vector3 m_ZoomAmount;
    [SerializeField] private float m_FovMax;
    [SerializeField] private float m_FovMin;
    private Camera m_MainCamera;
    private Vector3 m_NewPosition;
    private Vector3 m_NewZoom;
    private Quaternion m_NewQuaternion;

    private Vector3 m_DragStartPos;
    private Vector3 m_DragCurPos;

    private float m_CurFOV;
    // Start is called before the first frame update
    void Start()
    {
       m_MainCamera = Camera.main;
       m_CurFOV = m_MainCamera.fieldOfView;
       m_NewPosition = m_MainCamera.transform.position;
       m_NewQuaternion = m_MainCamera.transform.rotation;
       m_NewZoom = m_MainCamera.transform.localPosition;
       
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseInput();
        HandleMovementInput();
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.forward, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float entry;
            if (plane.Raycast(ray, out entry))
            {
                m_DragStartPos = ray.GetPoint(entry);
            }
        }
        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.forward, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float entry;
            if (plane.Raycast(ray, out entry))
            {
                m_DragCurPos = ray.GetPoint(entry);
                m_NewPosition = m_MainCamera.transform.position + m_DragStartPos - m_DragCurPos;
            }
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            Plane plane = new Plane(Vector3.forward, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float entry;
            if (plane.Raycast(ray, out entry))
            {
                m_DragStartPos = ray.GetPoint(entry);
            }
        }
        if (Input.GetMouseButton(1))
        {
            Plane plane = new Plane(Vector3.forward, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float entry;
            if (plane.Raycast(ray, out entry))
            {
                m_DragCurPos = ray.GetPoint(entry);
                m_CurFOV += (m_DragCurPos.x - m_DragStartPos.x);
                Camera.main.fieldOfView = Mathf.Clamp(m_CurFOV, 45, 90);
            }
        }
    }

    void HandleMovementInput()
    {
        #region 移动

        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_MovementSpeed = m_FastSpeed;
        }
        else
        {
            m_MovementSpeed = m_NormalSpeed;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            m_NewPosition += (m_MainCamera.transform.forward * m_MovementSpeed);
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            m_NewPosition -= (m_MainCamera.transform.forward * m_MovementSpeed);
        }
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            m_NewPosition -= (m_MainCamera.transform.right * m_MovementSpeed);
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            m_NewPosition += (m_MainCamera.transform.right * m_MovementSpeed);
        }
        m_MainCamera.transform.position = Vector3.Lerp(m_MainCamera.transform.position, m_NewPosition,
            Time.deltaTime * m_MovementTime);
        #endregion
        
        #region 视角

        if (Input.GetKey(KeyCode.Q))
        {
            m_NewQuaternion *= Quaternion.Euler(Vector3.up * m_RotationAmount);   
        }
        if (Input.GetKey(KeyCode.E))
        {
            m_NewQuaternion *= Quaternion.Euler(Vector3.up * -m_RotationAmount);  
        }
        m_MainCamera.transform.rotation = Quaternion.Lerp(m_MainCamera.transform.rotation, m_NewQuaternion, Time.deltaTime *m_MovementTime);

        if (Input.GetKey(KeyCode.R))
        {
            m_NewZoom += m_ZoomAmount;
        }
        if (Input.GetKey(KeyCode.F))
        {
            m_NewZoom -= m_ZoomAmount;
        }
        m_MainCamera.transform.localPosition = Vector3.Lerp(m_MainCamera.transform.localPosition, m_NewZoom,
            Time.deltaTime * m_MovementTime);
        #endregion
        
        
        
        
    }
    
    
}
