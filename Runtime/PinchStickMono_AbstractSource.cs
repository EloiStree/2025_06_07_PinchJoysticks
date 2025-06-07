using UnityEngine;

namespace Eloi.PinchJoysticks
{


    public abstract class PinchStickMono_AbstractSource : MonoBehaviour, I_PinchStickSource
{
    public abstract void GetHeadCenterInfo(out Vector3 headCenter, out Quaternion headRotation);
    public abstract void GetLeftAnchorPoint(out Vector3 leftAnchorPoint, out Quaternion leftAnchorRotation);
    public abstract void GetRightAnchorPoint(out Vector3 rightAnchorPoint, out Quaternion rightAnchorRotation);
}

}