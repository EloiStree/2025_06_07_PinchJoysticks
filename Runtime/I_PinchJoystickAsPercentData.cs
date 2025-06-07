namespace Eloi.PinchJoysticks
{
    public interface I_PinchJoystickAsPercentData
    {

        public void GetPercentLeftToRight(out float leftToRightPercent11);
        public void GetPercentDownToUp(out float downToUpPercent11);
        public void GetPercentBackToFront(out float backToFrontPercent11);
        public void GetPercentYawLeftToRight(out float rotatingLeftToRightPercent11);
        public void GetPercentHorizontalPitchBackToFront(out float pitchingBackToFrontPercent11);
        public void GetPercentHorizontalRollingLeftToRight(out float rollingLeftToRightPercent11);

    }
}
