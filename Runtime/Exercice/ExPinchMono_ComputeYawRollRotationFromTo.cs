using UnityEngine;


namespace Eloi.PinchJoysticks
{
    public class ExPinchMono_ComputeYawRollRotationFromTo : MonoBehaviour
    {
        public STRUCT_CartesianTwoPoints m_root;
        public STRUCT_CartesianTwoPoints m_current;

        public float m_rootRoll = 0f;
        public float m_givenRoll = 0f;
        public float m_deltaRoll = 0f;
        public float m_deltaFlatYaw = 0f;
        public Quaternion m_rotationFromTo = Quaternion.identity;
        public Vector3 m_translationFromTo = Vector3.zero;

        public void PushIn(STRUCT_CartesianTwoPoints root, STRUCT_CartesianTwoPoints given)
        { 
            m_root = root;
            m_current = given;

            m_translationFromTo = m_current.m_worldPosition - m_root.m_worldPosition;
            Vector3 forwardOfRoot = m_root.m_worldRotation * Vector3.forward;
            Vector3 forwardOfCurrent = m_current.m_worldRotation * Vector3.forward;
            Vector3 flatForwardOfRoot = new Vector3(forwardOfRoot.x, 0f, forwardOfRoot.z).normalized;
            Vector3 flatForwardOfCurrent = new Vector3(forwardOfCurrent.x, 0f, forwardOfCurrent.z).normalized;
            m_rotationFromTo = Quaternion.FromToRotation(forwardOfRoot, forwardOfCurrent);
            m_deltaFlatYaw = Vector3.SignedAngle(flatForwardOfRoot, flatForwardOfCurrent, Vector3.up);
            PinchJoystickUtility.GetLocalRollOf(root.m_leftWorldPosition, root.m_rightWorldPosition, out m_rootRoll);
            PinchJoystickUtility.GetLocalRollOf(given.m_leftWorldPosition, given.m_rightWorldPosition, out m_givenRoll);
            m_deltaRoll = m_givenRoll - m_rootRoll;


        }
    }
}