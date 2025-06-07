using UnityEngine;

namespace Eloi.PinchJoysticks
{
    public interface I_PinchStickSource { 

    public void GetHeadCenterInfo(out Vector3 headCenter, out Quaternion headRotation);
    public void GetLeftAnchorPoint(out Vector3 leftAnchorPoint, out Quaternion leftAnchorRotation);
    public void GetRightAnchorPoint(out Vector3 rightAnchorPoint, out Quaternion rightAnchorRotation);

    }

}