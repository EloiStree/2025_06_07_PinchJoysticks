using UnityEngine;
using UnityEngine.Events;


namespace Eloi.PinchJoysticks
{
    public class ExPinchMono_IsTwoPointsNearEnough : MonoBehaviour
    {

        public Transform m_pointA;
        public Transform m_pointB;
        public float m_distanceThreshold = 0.02f;
        public UnityEvent<bool> m_onNearEnoughChanged;
        public bool m_isNearEnough = false;
        public float m_currentDistance;


        public void Awake()
        {
            ComputeInfoAndPushEvent();
               }

        public void Update()
        {
            ComputeInfoAndPushEvent();
        }

        private void ComputeInfoAndPushEvent()
        {

            if (m_pointA == null || m_pointB == null)
            {
              
                return;
            }

            bool previousState = m_isNearEnough;
            m_currentDistance = Vector3.Distance(m_pointA.position, m_pointB.position);
            m_isNearEnough = Mathf.Abs(m_currentDistance) <= m_distanceThreshold;
            if (previousState != m_isNearEnough)
            {
                m_onNearEnoughChanged?.Invoke(m_isNearEnough);
            }
        }
    }
}