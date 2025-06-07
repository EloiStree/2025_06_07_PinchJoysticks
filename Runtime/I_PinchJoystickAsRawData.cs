using UnityEngine;

namespace Eloi.PinchJoysticks
{
    public interface I_PinchJoystickAsRawData
    {
        /// <summary>
        /// I am a methode that return the middle point of the two pinching points compare to it start point
        /// </summary>
        /// <param name="directionOfMiddlePoint"></param>
        public void GetLocalDirectionOfMiddlePoint(out Vector3 localDirectionOfMiddlePoint);
        public void GetWorldDirectionOfMiddlePoint(out Vector3 worldDirectionOfMiddlePoint);
        public void GetWorldStartPoint(out Vector3 worldStartPoint);
        public void GetAngleFrontYaw(out float angleFrontYaw);
        public void GetAngleHorizontalPitch(out float angleHorizontalPitch);
        public void GetAngleHorizontalRoll(out float angleHorizontalRoll);

    }


}
