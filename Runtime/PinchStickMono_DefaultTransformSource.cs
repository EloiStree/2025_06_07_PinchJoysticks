using UnityEngine;

namespace Eloi.PinchJoysticks
{
    public class PinchStickMono_DefaultTransformSource : PinchStickMono_AbstractSource
{
    public Transform headCenter;
    public Transform leftAnchor;
    public Transform rightAnchor;
    public override void GetHeadCenterInfo(out Vector3 headCenter, out Quaternion headRotation)
    {
        headCenter = this.headCenter.position;
        headRotation = this.headCenter.rotation;
    }
    public override void GetLeftAnchorPoint(out Vector3 leftAnchorPoint, out Quaternion leftAnchorRotation)
    {
        leftAnchorPoint = this.leftAnchor.position;
        leftAnchorRotation = this.leftAnchor.rotation;
    }
    public override void GetRightAnchorPoint(out Vector3 rightAnchorPoint, out Quaternion rightAnchorRotation)
    {
        rightAnchorPoint = this.rightAnchor.position;
        rightAnchorRotation = this.rightAnchor.rotation;
    }
}

}