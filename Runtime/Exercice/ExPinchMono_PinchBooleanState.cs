using UnityEngine;
using UnityEngine.Events;


namespace Eloi.PinchJoysticks
{



    public class ExPinchMono_PinchBooleanState : MonoBehaviour
    {
        public bool m_leftPinch = false;
        public bool m_rightPinch = false;
        public bool m_isBothPinch = false;
        public bool m_isOnePinch = false;
        public UnityEvent<bool> m_onPinchingChanged;
        public UnityEvent m_onPinchingEnter;
        public UnityEvent m_onPinchingExit;


        private void OnValidate()
        {
            UpdateState();
        }
        public void SetAsPinchingLeft(bool isPinching)
        {
            m_leftPinch = isPinching;
            UpdateState();
        }
        public void SetAsPinchingRight(bool isPinching)
        {
            m_rightPinch = isPinching;
            UpdateState();
        }

        private void UpdateState()
        {
            bool previousBothPinch = m_isBothPinch;
            bool actualBothPinch = m_leftPinch && m_rightPinch;
            bool changed = previousBothPinch != actualBothPinch;

            m_isBothPinch = actualBothPinch;
            m_isOnePinch = m_leftPinch || m_rightPinch;

            if (changed)
            {
                m_onPinchingChanged?.Invoke(m_isBothPinch);
                if (m_isBothPinch)
                {
                    m_onPinchingEnter?.Invoke();
                }
                else
                {
                    m_onPinchingExit?.Invoke();
                }
            }
        }
    }
}