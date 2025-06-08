using UnityEngine;
using UnityEngine.Events;


namespace Eloi.PinchJoysticks
{

    public class ExPinchMono_TwoPointsFocusState: MonoBehaviour
    {
        public Transform m_pointLeft;
        public Transform m_pointRight;

        public bool m_currentFocusState = false;
        public STRUCT_CartesianTwoPoints m_onFocusStart;
        public STRUCT_CartesianTwoPoints m_onFocusStay;
        public STRUCT_CartesianTwoPoints m_onFocusEnd;

        public Events m_events = new Events();

        [System.Serializable]
        public class Events
        {
            [Header("Basic")]
            public UnityEvent<bool> m_onFocusStateChanged;
            public UnityEvent<STRUCT_CartesianTwoPoints> m_onFocusStart ;
            public UnityEvent<STRUCT_CartesianTwoPoints> m_onFocusStay  ;
            public UnityEvent<STRUCT_CartesianTwoPoints> m_onFocusEnd;

            [Header("Double")]
            public UnityEvent<STRUCT_CartesianTwoPoints, STRUCT_CartesianTwoPoints> m_onFocusStartStayUpdated;
            public UnityEvent<STRUCT_CartesianTwoPoints, STRUCT_CartesianTwoPoints> m_onFocusStartEnd;

        }

        public void GetCartesianOf(out STRUCT_CartesianTwoPoints state) {

            PinchJoystickUtility.GetCartesianOf(m_pointLeft, m_pointRight, out state);
        }

        public void ClearData()
        {
            m_onFocusStart = new STRUCT_CartesianTwoPoints();
            m_onFocusStay = new STRUCT_CartesianTwoPoints();
            m_onFocusEnd = new STRUCT_CartesianTwoPoints();
        }


        [ContextMenu("Set Focus As On")]
        public void SetFocusAsOn()
        {
            SetFocusStateTo(true);
        }
        [ContextMenu("Set Focus As Off")]
        public void SetFocusAsOff()
        {
            SetFocusStateTo(false);
        }


        public void SetFocusStateTo(bool newFocusState)
        {
            if (m_currentFocusState == newFocusState)
            {
                return;
            }
            m_currentFocusState = newFocusState;
            if (m_currentFocusState)
            {
                ClearData();
                GetCartesianOf(out m_onFocusStart);
                m_onFocusStay = m_onFocusStart;
                m_events.m_onFocusStart?.Invoke(m_onFocusStart);
                m_events.m_onFocusStartStayUpdated?.Invoke(m_onFocusStart, m_onFocusStay);
            }
            else
            {
                GetCartesianOf(out m_onFocusEnd);
                m_events.m_onFocusEnd?.Invoke(m_onFocusEnd);
                m_events.m_onFocusStartEnd?.Invoke(m_onFocusStart, m_onFocusEnd);
            }
            m_events.m_onFocusStateChanged?.Invoke(m_currentFocusState);
        }


        public void Update()
        {

            if (m_currentFocusState)
            {
                STRUCT_CartesianTwoPoints currentState;
                GetCartesianOf(out currentState);
                m_onFocusStay = currentState;
                m_events.m_onFocusStay?.Invoke(m_onFocusStay);
                m_events.m_onFocusStartStayUpdated?.Invoke(m_onFocusStart, m_onFocusStay);
            }

        }

  


    }
}