using System;
using UnityEngine;
using UnityEngine.Events;


namespace Eloi.PinchJoysticks
{
    public class ExPinchMono_AngleRangeToPercent11 : MonoBehaviour
{

    public float m_angleRange = 180f;
    public float m_percent = 0f;
    public bool m_clampTheAngle = true;
    public UnityEvent<float> m_onPercentRelayed;
     public bool m_inversePercent = false;
    

        public void PushIn(float angle)
    {
        float percent = angle / m_angleRange;
        if (m_clampTheAngle)
        {
            percent = Mathf.Clamp(percent, -1, 1);
        }
            if (m_inversePercent)
            {
                percent = -percent;
            }
            m_percent = percent;
        m_onPercentRelayed?.Invoke(m_percent);

    }
}
}