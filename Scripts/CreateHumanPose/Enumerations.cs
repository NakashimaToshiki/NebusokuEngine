
namespace NebusokuEngine.CreateHumanPose
{
    /// <summary>
    /// ボーン名
    /// </summary>
    public enum BoneKey
    {
        Root,
        Spine,
        Chest,
        UpperChest,
        Neck,
        Head,
        ShoulderLeft,
        UpperArmLeft,
        LowerArmLeft,
        HandLeft,
        UpperLegLeft,
        LowerLegLeft,
        FootLeft,
        ToesLeft,
        ShoulderRight,
        UpperArmRight,
        LowerArmRight,
        HandRight,
        UpperLegRight,
        LowerLegRight,
        FootRight,
        ToesRight,

        ThumbLeft,
        IndexLeft,
        MiddleLeft,
        RingLeft,
        LittleLeft,
        ThumbRight,
        IndexRight,
        MiddleRight,
        RingRight,
        LittleRight,
    }

    /// <summary>
    /// アニメーションプロパティ
    /// </summary>
    public enum HumanMuscleKey
    {
        SpineFrontBack = 0,
        SpineLeftRight,
        SpineTwist,
        ChestFrontBack,
        ChestLeftRight,
        ChestTwist,
        UpperChestFrontBack,
        UpperChestLeftRight,
        UpperChestTwist,
        NeckDownUp,
        NeckLeftRight,
        NeckTurn,
        HeadDownUp,
        HeadTilt,
        HeadTurn,
        EyeDownUpLeft,
        EyeInOutLeft,
        EyeDownUpRight,
        EyeInOutRight,
        JawClose,
        JawLeftRight,

        UpperLegFrontBackLeft,
        UpperLegTwistLeft,
        LowerLegTwistLeft,
        LowerLegFrontBackLeft,
        FootTwistLeft,
        FootUpDownLeft,
        FootLeftRightLeft,
        ToesUpDownLeft,

        UpperLegFrontBackRight,
        UpperLegTwistRight,
        LowerLegTwistRight,
        LowerLegFrontBackRight,
        FootTwistRight,
        FootUpDownRight,
        FootLeftRightRight,
        ToesUpDownRight,

        ShoulderDownUpLeft,
        ShoulderFrontBackLeft,
        ArmDownUpLeft,
        ArmFrontBackLeft,
        ArmTwistLeft,
        ForearmStretchLeft,
        ForearmTwistLeft,
        HandDownUpLeft,
        HandInOutLeft,

        ShoulderDownUpRight,
        ShoulderFrontBackRight,
        ArmDownUpRight,
        ArmFrontBackRight,
        ArmTwistRight,
        ForearmStretchRight,
        ForearmTwistRight,
        HandDownUpRight,
        HandInOutRight,

        Thumb1StretchedLeft,
        ThumbSpreadLeft,
        Thumb2StretchedLeft,
        Thumb3StretchedLeft,

        Index1StretchedLeft,
        IndexSpreadLeft,
        Index2StretchedLeft,
        Index3StretchedLeft,

        Middle1StretchedLeft,
        MiddleSpreadLeft,
        Middle2StretchedLeft,
        Middle3StretchedLeft,

        Ring1StretchedLeft,
        RingSpreadLeft,
        Ring2StretchedLeft,
        Ring3StretchedLeft,

        Little1StretchedLeft,
        LittleSpreadLeft,
        Little2StretchedLeft,
        Little3StretchedLeft,

        Thumb1StretchedRight,
        ThumbSpreadRight,
        Thumb2StretchedRight,
        Thumb3StretchedRight,

        Index1StretchedRight,
        IndexSpreadRight,
        Index2StretchedRight,
        Index3StretchedRight,

        Middle1StretchedRight,
        MiddleSpreadRight,
        Middle2StretchedRight,
        Middle3StretchedRight,

        Ring1StretchedRight,
        RingSpreadRight,
        Ring2StretchedRight,
        Ring3StretchedRight,

        Little1StretchedRight,
        LittleSpreadRight,
        Little2StretchedRight,
        Little3StretchedRight,
    }
}