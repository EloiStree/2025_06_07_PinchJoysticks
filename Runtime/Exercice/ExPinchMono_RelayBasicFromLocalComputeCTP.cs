using UnityEngine;
using UnityEngine.Events;


namespace Eloi.PinchJoysticks
{
    public class ExPinchMono_RelayBasicFromLocalComputeCTP : MonoBehaviour
    {
        public ExPinchMono_LocalComputeForCartesianTwoPoints m_observed;

        public float m_yaw;
        public float m_roll;
        public float m_scalePercent11;
        public float m_scaleDistance;
        public float m_distanceFromCenter;
        public Vector3 m_joystickDirection;
        public Vector3 m_joystickDirectionPercent11;
        public float m_distanceToPercentRatio = 0.3f; 

        public UnityEvent<float> m_onYawRelayed;
        public UnityEvent<float> m_onRollRelayed;
        public UnityEvent<float> m_onScalePercent11Relayed;
        
        public UnityEvent<float> m_onScaleDistanceRelayed;
        public UnityEvent<float> m_onDistanceFromCenterRelayed;
        public UnityEvent<Vector3> m_onJoystickDirectionRelayed;
        public UnityEvent<Vector3> m_onJoystickDirectionPercent11Relayed;
        

        public void Update()
        {
            if (m_observed == null) 
                return;

            m_observed.GetLocalYawTo(out m_yaw);
            m_onYawRelayed?.Invoke(m_yaw);

            m_observed.GetLocalRollTo(out m_roll);
            m_onRollRelayed?.Invoke(m_roll);

            m_observed.GetRollDelta(out float rollDelta);
            m_onRollRelayed?.Invoke(rollDelta);

            m_observed.GetScalePercent11(out m_scalePercent11);
            m_onScalePercent11Relayed?.Invoke(m_scalePercent11);


            m_observed.GetLocalDirectionDistance(out m_distanceFromCenter);
            m_onDistanceFromCenterRelayed?.Invoke(m_distanceFromCenter);


            m_observed.GetScaleDistance(out m_scaleDistance);
            m_onScaleDistanceRelayed?.Invoke(m_scaleDistance);

            m_observed.GetJoystickFromMiddlePoint(m_distanceToPercentRatio, true, out m_joystickDirection);
            m_onJoystickDirectionRelayed?.Invoke(m_joystickDirection);

            m_observed.GetLocalDirection(out m_joystickDirectionPercent11);
            m_onJoystickDirectionPercent11Relayed?.Invoke(m_joystickDirectionPercent11);


        }




    }
}