using UnityEngine;
using UnityEngine.Events;


namespace Eloi.PinchJoysticks
{
    public class ExPinchMono_UsePercentToMoveInPercentValue : MonoBehaviour
    {

        public float m_percentToMove = 0;
        public float m_percentState11 = 0f;
        public float m_movePercentPerSeconds = 0.5f;
        public float m_maxPercentValue = 1f;
        public float m_minPercentValue = 0f;

        public UnityEvent<float> m_onPercentState11Changed;

        public void SetPercentToMove(float percent11)
        {
            m_percentToMove = percent11;
        }

        void Update()
        {
            float deltaTime = Time.deltaTime;
            m_percentState11 += m_percentToMove * m_movePercentPerSeconds * deltaTime;
            m_percentState11 = Mathf.Clamp(m_percentState11, m_minPercentValue, m_maxPercentValue);
            m_onPercentState11Changed?.Invoke(m_percentState11);
        }

    }
}