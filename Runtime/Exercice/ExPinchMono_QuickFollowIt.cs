using UnityEngine;


namespace Eloi.PinchJoysticks
{
    public class ExPinchMono_QuickFollowIt: MonoBehaviour
    {
        public Transform m_target;
        public Transform m_whatToMove;
        public bool m_useUpdate            = false;
        public bool m_useLateUpdate            = true;


        private void Reset()
        {
            m_whatToMove = transform;
        }
        private void LateUpdate()
        {
            if (!m_useLateUpdate)
                return;
            if (m_target != null)
            {
                m_whatToMove.position = m_target.position;
                m_whatToMove.rotation = m_target.rotation;
            }
        }
        private void Update()
        {
            if (!m_useUpdate)
                return;
            if (m_target != null)
            {
                m_whatToMove.position = m_target.position;
                m_whatToMove.rotation = m_target.rotation;
            }
        }
    }
}