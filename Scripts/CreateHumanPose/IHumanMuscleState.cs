namespace NebusokuEngine.CreateHumanPose
{
    public interface IHumanMuscleState
    {
        int Id { get; }

        IHumanMuscleState MateState { get; }
    }

    public abstract class SingletonHumanMuscleStateBase<T> : IHumanMuscleState where T : IHumanMuscleState, new()
    {
        public abstract int Id { get; }
        public abstract IHumanMuscleState MateState { get; }

        private readonly static T singleton = new T();
        protected SingletonHumanMuscleStateBase() { }
        public static T GetInstance() => singleton;
    }

    public class NullState : SingletonHumanMuscleStateBase<NullState>
    {
        override public int Id => -1;
        override public IHumanMuscleState MateState => GetInstance();
    }

    public class SpineFrontBackState : SingletonHumanMuscleStateBase<SpineFrontBackState>
    {
        override public int Id => 0;
        override public IHumanMuscleState MateState => NullState.GetInstance();
    }

    public class SpineLeftRightState : SingletonHumanMuscleStateBase<SpineLeftRightState>
    {
        override public int Id => 1;
        override public IHumanMuscleState MateState => GetInstance();
    }

    public class SpineTwistState : SingletonHumanMuscleStateBase<SpineTwistState>
    {
        override public int Id => 2;
        override public IHumanMuscleState MateState => GetInstance();
    }

    public class ChestFrontBackState : SingletonHumanMuscleStateBase<ChestFrontBackState>
    {
        override public int Id => 3;
        override public IHumanMuscleState MateState => NullState.GetInstance();
    }

    public class ChestLeftRightState : SingletonHumanMuscleStateBase<ChestLeftRightState>
    {
        override public int Id => 4;
        override public IHumanMuscleState MateState => GetInstance();
    }

    public class ChestTwistState : SingletonHumanMuscleStateBase<ChestTwistState>
    {
        override public int Id => 5;
        override public IHumanMuscleState MateState => GetInstance();
    }

    public class UpperChestFrontBackState : SingletonHumanMuscleStateBase<UpperChestFrontBackState>
    {
        override public int Id => 6;
        override public IHumanMuscleState MateState => NullState.GetInstance();
    }

    public class UpperChestLeftRightState : SingletonHumanMuscleStateBase<UpperChestLeftRightState>
    {
        override public int Id => 7;
        override public IHumanMuscleState MateState => GetInstance();
    }

    public class UpperChestTwistRightState : SingletonHumanMuscleStateBase<UpperChestTwistRightState>
    {
        override public int Id => 8;
        override public IHumanMuscleState MateState => GetInstance();
    }

    public class NeckDownUpState : SingletonHumanMuscleStateBase<NeckDownUpState>
    {
        override public int Id => 9;
        override public IHumanMuscleState MateState => NullState.GetInstance();
    }

    public class NeckLeftRightState : SingletonHumanMuscleStateBase<NeckLeftRightState>
    {
        override public int Id => 10;
        override public IHumanMuscleState MateState => GetInstance();
    }

    public class NeckTurnState : SingletonHumanMuscleStateBase<NeckTurnState>
    {
        override public int Id => 11;
        override public IHumanMuscleState MateState => GetInstance();
    }

    public class HeadDownUpState : SingletonHumanMuscleStateBase<HeadDownUpState>
    {
        override public int Id => 12;
        override public IHumanMuscleState MateState => NullState.GetInstance();
    }

    public class HeadTiltState : SingletonHumanMuscleStateBase<HeadTiltState>
    {
        override public int Id => 13;
        override public IHumanMuscleState MateState => GetInstance();
    }

    public class HeadTurnState : SingletonHumanMuscleStateBase<HeadTurnState>
    {
        override public int Id => 14;
        override public IHumanMuscleState MateState => GetInstance();
    }

    public class EyeDownUpLeftState : SingletonHumanMuscleStateBase<EyeDownUpLeftState>
    {
        override public int Id => 15;
        override public IHumanMuscleState MateState => NullState.GetInstance();
    }

    public class EyeInOutLeftState : SingletonHumanMuscleStateBase<EyeInOutLeftState>
    {
        override public int Id => 16;
        override public IHumanMuscleState MateState => GetInstance();
    }

    public class EyeDownUpRightState : SingletonHumanMuscleStateBase<EyeDownUpRightState>
    {
        override public int Id => 17;
        override public IHumanMuscleState MateState => NullState.GetInstance();
    }

    public class EyeInOutRightState : SingletonHumanMuscleStateBase<EyeInOutRightState>
    {
        override public int Id => 18;
        override public IHumanMuscleState MateState => GetInstance();
    }

    public class JawCloseState : SingletonHumanMuscleStateBase<JawCloseState>
    {
        override public int Id => 19;
        override public IHumanMuscleState MateState => NullState.GetInstance();
    }

    public class JawLeftRightState : SingletonHumanMuscleStateBase<JawLeftRightState>
    {
        override public int Id => 20;
        override public IHumanMuscleState MateState => GetInstance();
    }



    public class UpperLegFrontBackLeftState : SingletonHumanMuscleStateBase<UpperLegFrontBackLeftState>
    {
        override public int Id => 21;
        override public IHumanMuscleState MateState => UpperLegFrontBackRightState.GetInstance();
    }

    public class UpperLegTwistLeftState : SingletonHumanMuscleStateBase<UpperLegTwistLeftState>
    {
        override public int Id => 22;
        override public IHumanMuscleState MateState => UpperLegTwistRightState.GetInstance();
    }

    public class LowerLegTwistLeftState : SingletonHumanMuscleStateBase<LowerLegTwistLeftState>
    {
        override public int Id => 23;
        override public IHumanMuscleState MateState => LowerLegTwistRightState.GetInstance();
    }

    public class LowerLegFrontBackLeftState : SingletonHumanMuscleStateBase<LowerLegFrontBackLeftState>
    {
        override public int Id => 24;
        override public IHumanMuscleState MateState => LowerLegFrontBackRightState.GetInstance();
    }

    public class FootTwistLeftState : SingletonHumanMuscleStateBase<FootTwistLeftState>
    {
        override public int Id => 25;
        override public IHumanMuscleState MateState => FootTwistRightState.GetInstance();
    }

    public class FootUpDownLeftState : SingletonHumanMuscleStateBase<FootUpDownLeftState>
    {
        override public int Id => 26;
        override public IHumanMuscleState MateState => FootUpDownRightState.GetInstance();
    }

    public class FootLeftRightLeftState : SingletonHumanMuscleStateBase<FootLeftRightLeftState>
    {
        override public int Id => 27;
        override public IHumanMuscleState MateState => GetInstance();
    }

    public class ToesUpDownLeftState : SingletonHumanMuscleStateBase<ToesUpDownLeftState>
    {
        override public int Id => 28;
        override public IHumanMuscleState MateState => FootUpDownLeftState.GetInstance();
    }

    public class UpperLegFrontBackRightState : SingletonHumanMuscleStateBase<UpperLegFrontBackRightState>
    {
        override public int Id => 29;
        override public IHumanMuscleState MateState => UpperLegFrontBackLeftState.GetInstance();
    }

    public class UpperLegTwistRightState : SingletonHumanMuscleStateBase<UpperLegTwistRightState>
    {
        override public int Id => 30;
        override public IHumanMuscleState MateState => UpperLegTwistLeftState.GetInstance();
    }

    public class LowerLegTwistRightState : SingletonHumanMuscleStateBase<LowerLegTwistRightState>
    {
        override public int Id => 31;
        override public IHumanMuscleState MateState => LowerLegTwistLeftState.GetInstance();
    }

    public class LowerLegFrontBackRightState : SingletonHumanMuscleStateBase<LowerLegFrontBackRightState>
    {
        override public int Id => 32;
        override public IHumanMuscleState MateState => LowerLegFrontBackLeftState.GetInstance();
    }

    public class FootTwistRightState : SingletonHumanMuscleStateBase<FootTwistRightState>
    {
        override public int Id => 33;
        override public IHumanMuscleState MateState => FootTwistLeftState.GetInstance();
    }

    public class FootUpDownRightState : SingletonHumanMuscleStateBase<FootUpDownRightState>
    {
        override public int Id => 34;
        override public IHumanMuscleState MateState => FootUpDownLeftState.GetInstance();
    }

    public class FootLeftRightRightState : SingletonHumanMuscleStateBase<FootLeftRightRightState>
    {
        override public int Id => 35;
        override public IHumanMuscleState MateState => FootLeftRightLeftState.GetInstance();
    }

    public class ToesUpDownRightState : SingletonHumanMuscleStateBase<ToesUpDownRightState>
    {
        override public int Id => 36;
        override public IHumanMuscleState MateState => ToesUpDownLeftState.GetInstance();
    }


    public class ShoulderDownUpLeftState : SingletonHumanMuscleStateBase<ShoulderDownUpLeftState>
    {
        override public int Id => 37;
        override public IHumanMuscleState MateState => ShoulderDownUpRightState.GetInstance();
    }

    public class ShoulderFrontBackLeftState : SingletonHumanMuscleStateBase<ShoulderFrontBackLeftState>
    {
        override public int Id => 38;
        override public IHumanMuscleState MateState => ShoulderFrontBackRightState.GetInstance();
    }

    public class ArmDownUpLeftState : SingletonHumanMuscleStateBase<ArmDownUpLeftState>
    {
        override public int Id => 39;
        override public IHumanMuscleState MateState => ArmDownUpRightState.GetInstance();
    }

    public class ArmFrontBackLeftState : SingletonHumanMuscleStateBase<ArmFrontBackLeftState>
    {
        override public int Id => 40;
        override public IHumanMuscleState MateState => ArmFrontBackRightState.GetInstance();
    }

    public class ArmTwistLeftState : SingletonHumanMuscleStateBase<ArmTwistLeftState>
    {
        override public int Id => 41;
        override public IHumanMuscleState MateState => ArmTwistRightState.GetInstance();
    }

    public class ForearmStretchLeftState : SingletonHumanMuscleStateBase<ForearmStretchLeftState>
    {
        override public int Id => 42;
        override public IHumanMuscleState MateState => ForearmStretchRightState.GetInstance();
    }

    public class ForearmTwistLeftState : SingletonHumanMuscleStateBase<ForearmTwistLeftState>
    {
        override public int Id => 43;
        override public IHumanMuscleState MateState => ForearmTwistRightState.GetInstance();
    }

    public class HandDownUpLeftState : SingletonHumanMuscleStateBase<HandDownUpLeftState>
    {
        override public int Id => 44;
        override public IHumanMuscleState MateState => NullState.GetInstance();
    }

    public class HandInOutLeftState : SingletonHumanMuscleStateBase<HandInOutLeftState>
    {
        override public int Id => 45;
        override public IHumanMuscleState MateState => HandInOutRightState.GetInstance();
    }


    public class ShoulderDownUpRightState : SingletonHumanMuscleStateBase<ShoulderDownUpRightState>
    {
        override public int Id => 46;
        override public IHumanMuscleState MateState => ShoulderDownUpLeftState.GetInstance();
    }

    public class ShoulderFrontBackRightState : SingletonHumanMuscleStateBase<ShoulderFrontBackRightState>
    {
        override public int Id => 47;
        override public IHumanMuscleState MateState => ShoulderFrontBackLeftState.GetInstance();
    }

    public class ArmDownUpRightState : SingletonHumanMuscleStateBase<ArmDownUpRightState>
    {
        override public int Id => 48;
        override public IHumanMuscleState MateState => ArmDownUpLeftState.GetInstance();
    }

    public class ArmFrontBackRightState : SingletonHumanMuscleStateBase<ArmFrontBackRightState>
    {
        override public int Id => 49;
        override public IHumanMuscleState MateState => ArmFrontBackLeftState.GetInstance();
    }

    public class ArmTwistRightState : SingletonHumanMuscleStateBase<ArmTwistRightState>
    {
        override public int Id => 50;
        override public IHumanMuscleState MateState => ArmTwistLeftState.GetInstance();
    }

    public class ForearmStretchRightState : SingletonHumanMuscleStateBase<ForearmStretchRightState>
    {
        override public int Id => 51;
        override public IHumanMuscleState MateState => ForearmStretchLeftState.GetInstance();
    }

    public class ForearmTwistRightState : SingletonHumanMuscleStateBase<ForearmTwistRightState>
    {
        override public int Id => 52;
        override public IHumanMuscleState MateState => ForearmTwistLeftState.GetInstance();
    }

    public class HandDownUpRightState : SingletonHumanMuscleStateBase<HandDownUpRightState>
    {
        override public int Id => 53;
        override public IHumanMuscleState MateState => HandDownUpLeftState.GetInstance();
    }

    public class HandInOutRightState : SingletonHumanMuscleStateBase<HandInOutRightState>
    {
        override public int Id => 54;
        override public IHumanMuscleState MateState => HandInOutLeftState.GetInstance();
    }


    public class Thumb1StretchedLeftState : SingletonHumanMuscleStateBase<Thumb1StretchedLeftState>
    {
        override public int Id => 55;
        override public IHumanMuscleState MateState => Thumb1StretchedRightState.GetInstance();
    }

    public class ThumbSpreadLeftState : SingletonHumanMuscleStateBase<ThumbSpreadLeftState>
    {
        override public int Id => 56;
        override public IHumanMuscleState MateState => ThumbSpreadRightState.GetInstance();
    }

    public class Thumb2StretchedLeftState : SingletonHumanMuscleStateBase<Thumb2StretchedLeftState>
    {
        override public int Id => 57;
        override public IHumanMuscleState MateState => Thumb2StretchedRightState.GetInstance();
    }

    public class Thumb3StretchedLeftState : SingletonHumanMuscleStateBase<Thumb3StretchedLeftState>
    {
        override public int Id => 58;
        override public IHumanMuscleState MateState => Thumb3StretchedRightState.GetInstance();
    }


    public class Index1StretchedLeftState : SingletonHumanMuscleStateBase<Index1StretchedLeftState>
    {
        override public int Id => 59;
        override public IHumanMuscleState MateState => Index1StretchedRightState.GetInstance();
    }

    public class IndexSpreadLeftState : SingletonHumanMuscleStateBase<IndexSpreadLeftState>
    {
        override public int Id => 60;
        override public IHumanMuscleState MateState => IndexSpreadRightState.GetInstance();
    }

    public class Index2StretchedLeftState : SingletonHumanMuscleStateBase<Index2StretchedLeftState>
    {
        override public int Id => 61;
        override public IHumanMuscleState MateState => Index2StretchedRightState.GetInstance();
    }

    public class Index3StretchedLeftState : SingletonHumanMuscleStateBase<Index3StretchedLeftState>
    {
        override public int Id => 62;
        override public IHumanMuscleState MateState => Index3StretchedRightState.GetInstance();
    }


    public class Middle1StretchedLeftState : SingletonHumanMuscleStateBase<Middle1StretchedLeftState>
    {
        override public int Id => 63;
        override public IHumanMuscleState MateState => Middle1StretchedRightState.GetInstance();
    }

    public class MiddleSpreadLeftState : SingletonHumanMuscleStateBase<MiddleSpreadLeftState>
    {
        override public int Id => 64;
        override public IHumanMuscleState MateState => MiddleSpreadRightState.GetInstance();
    }

    public class Middle2StretchedLeftState : SingletonHumanMuscleStateBase<Middle2StretchedLeftState>
    {
        override public int Id => 65;
        override public IHumanMuscleState MateState => Middle2StretchedRightState.GetInstance();
    }

    public class Middle3StretchedLeftState : SingletonHumanMuscleStateBase<Middle3StretchedLeftState>
    {
        override public int Id => 66;
        override public IHumanMuscleState MateState => Middle3StretchedRightState.GetInstance();
    }


    public class Ring1StretchedLeftState : SingletonHumanMuscleStateBase<Ring1StretchedLeftState>
    {
        override public int Id => 67;
        override public IHumanMuscleState MateState => Ring1StretchedRightState.GetInstance();
    }

    public class RingSpreadLeftState : SingletonHumanMuscleStateBase<RingSpreadLeftState>
    {
        override public int Id => 68;
        override public IHumanMuscleState MateState => RingSpreadRightState.GetInstance();
    }

    public class Ring2StretchedLeftState : SingletonHumanMuscleStateBase<Ring2StretchedLeftState>
    {
        override public int Id => 69;
        override public IHumanMuscleState MateState => Ring2StretchedRightState.GetInstance();
    }

    public class Ring3StretchedLeftState : SingletonHumanMuscleStateBase<Ring3StretchedLeftState>
    {
        override public int Id => 70;
        override public IHumanMuscleState MateState => Ring3StretchedRightState.GetInstance();
    }


    public class Little1StretchedLeftState : SingletonHumanMuscleStateBase<Little1StretchedLeftState>
    {
        override public int Id => 71;
        override public IHumanMuscleState MateState => Little1StretchedRightState.GetInstance();
    }

    public class LittleSpreadLeftState : SingletonHumanMuscleStateBase<LittleSpreadLeftState>
    {
        override public int Id => 72;
        override public IHumanMuscleState MateState => LittleSpreadRightState.GetInstance();
    }

    public class Little2StretchedLeftState : SingletonHumanMuscleStateBase<Little2StretchedLeftState>
    {
        override public int Id => 73;
        override public IHumanMuscleState MateState => Little2StretchedRightState.GetInstance();
    }

    public class Little3StretchedLeftState : SingletonHumanMuscleStateBase<Little3StretchedLeftState>
    {
        override public int Id => 74;
        override public IHumanMuscleState MateState => Little3StretchedRightState.GetInstance();
    }


    public class Thumb1StretchedRightState : SingletonHumanMuscleStateBase<Thumb1StretchedRightState>
    {
        override public int Id => 75;
        override public IHumanMuscleState MateState => Thumb1StretchedLeftState.GetInstance();
    }

    public class ThumbSpreadRightState : SingletonHumanMuscleStateBase<ThumbSpreadRightState>
    {
        override public int Id => 76;
        override public IHumanMuscleState MateState => ThumbSpreadLeftState.GetInstance();
    }

    public class Thumb2StretchedRightState : SingletonHumanMuscleStateBase<Thumb2StretchedRightState>
    {
        override public int Id => 77;
        override public IHumanMuscleState MateState => Thumb2StretchedLeftState.GetInstance();
    }

    public class Thumb3StretchedRightState : SingletonHumanMuscleStateBase<Thumb3StretchedRightState>
    {
        override public int Id => 78;
        override public IHumanMuscleState MateState => Thumb3StretchedLeftState.GetInstance();
    }


    public class Index1StretchedRightState : SingletonHumanMuscleStateBase<Index1StretchedRightState>
    {
        override public int Id => 79;
        override public IHumanMuscleState MateState => Index1StretchedLeftState.GetInstance();
    }

    public class IndexSpreadRightState : SingletonHumanMuscleStateBase<IndexSpreadRightState>
    {
        override public int Id => 80;
        override public IHumanMuscleState MateState => IndexSpreadLeftState.GetInstance();
    }

    public class Index2StretchedRightState : SingletonHumanMuscleStateBase<Index2StretchedRightState>
    {
        override public int Id => 81;
        override public IHumanMuscleState MateState => Index2StretchedLeftState.GetInstance();
    }

    public class Index3StretchedRightState : SingletonHumanMuscleStateBase<Index3StretchedRightState>
    {
        override public int Id => 82;
        override public IHumanMuscleState MateState => Index3StretchedLeftState.GetInstance();
    }


    public class Middle1StretchedRightState : SingletonHumanMuscleStateBase<Middle1StretchedRightState>
    {
        override public int Id => 83;
        override public IHumanMuscleState MateState => Middle1StretchedLeftState.GetInstance();
    }

    public class MiddleSpreadRightState : SingletonHumanMuscleStateBase<MiddleSpreadRightState>
    {
        override public int Id => 84;
        override public IHumanMuscleState MateState => MiddleSpreadLeftState.GetInstance();
    }

    public class Middle2StretchedRightState : SingletonHumanMuscleStateBase<Middle2StretchedRightState>
    {
        override public int Id => 85;
        override public IHumanMuscleState MateState => Middle2StretchedLeftState.GetInstance();
    }

    public class Middle3StretchedRightState : SingletonHumanMuscleStateBase<Middle3StretchedRightState>
    {
        override public int Id => 86;
        override public IHumanMuscleState MateState => Middle3StretchedLeftState.GetInstance();
    }


    public class Ring1StretchedRightState : SingletonHumanMuscleStateBase<Ring1StretchedRightState>
    {
        override public int Id => 87;
        override public IHumanMuscleState MateState => Ring1StretchedLeftState.GetInstance();
    }

    public class RingSpreadRightState : SingletonHumanMuscleStateBase<RingSpreadRightState>
    {
        override public int Id => 88;
        override public IHumanMuscleState MateState => RingSpreadLeftState.GetInstance();
    }

    public class Ring2StretchedRightState : SingletonHumanMuscleStateBase<Ring2StretchedRightState>
    {
        override public int Id => 89;
        override public IHumanMuscleState MateState => Ring2StretchedLeftState.GetInstance();
    }

    public class Ring3StretchedRightState : SingletonHumanMuscleStateBase<Ring3StretchedRightState>
    {
        override public int Id => 90;
        override public IHumanMuscleState MateState => Ring3StretchedLeftState.GetInstance();
    }


    public class Little1StretchedRightState : SingletonHumanMuscleStateBase<Little1StretchedRightState>
    {
        override public int Id => 91;
        override public IHumanMuscleState MateState => Little1StretchedLeftState.GetInstance();
    }

    public class LittleSpreadRightState : SingletonHumanMuscleStateBase<LittleSpreadRightState>
    {
        override public int Id => 92;
        override public IHumanMuscleState MateState => LittleSpreadLeftState.GetInstance();
    }

    public class Little2StretchedRightState : SingletonHumanMuscleStateBase<Little2StretchedRightState>
    {
        override public int Id => 93;
        override public IHumanMuscleState MateState => Little2StretchedLeftState.GetInstance();
    }

    public class Little3StretchedRightState : SingletonHumanMuscleStateBase<Little3StretchedRightState>
    {
        override public int Id => 94;
        override public IHumanMuscleState MateState => Little3StretchedLeftState.GetInstance();
    }
}