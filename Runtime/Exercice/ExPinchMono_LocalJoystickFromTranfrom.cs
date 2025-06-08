using UnityEngine;
using UnityEngine.Events;


namespace Eloi.PinchJoysticks
{

    [ExecuteInEditMode]
    public class ExPinchMono_LocalJoystickFromTranfrom : MonoBehaviour
    {
        public Transform m_pointToObserve;

        public float m_distanceForPercent = 0.3f;

        public Vector3 m_currentPosition;
        public Vector3 m_joystick;
        public UnityEvent<Vector3> m_onJoystickRelayed;
        public bool m_useClamp = true;
        public void Update()
        {
            if (m_pointToObserve == null)
            {
                return;
            }
            m_currentPosition = m_pointToObserve.position;  
            // Destination - Origine  sauf ici origine c est zero
            Vector3 localPosition = m_pointToObserve.position;
            m_joystick.x = localPosition.x / m_distanceForPercent;
            m_joystick.y = localPosition.y / m_distanceForPercent;
            m_joystick.z = localPosition.z / m_distanceForPercent;

            if (m_useClamp)
            {
                m_joystick.x = Mathf.Clamp(m_joystick.x, -1f, 1f);
                m_joystick.y = Mathf.Clamp(m_joystick.y, -1f, 1f);
                m_joystick.z = Mathf.Clamp(m_joystick.z, -1f, 1f);
            }
            m_onJoystickRelayed?.Invoke(m_joystick);
        }
    }
}