using System.Collections.Generic;

namespace NebusokuEngine.CreateHumanPose
{
    /// <summary>
    /// コピー方法
    /// </summary>
    public enum HumanBoneCopyType
    {
        /// <summary>
        /// 反転コピー
        /// </summary>
        Copy,
        /// <summary>
        /// 入れ替え
        /// </summary>
        Trade,
    }

    /// <summary>
    /// Humanoidのボーン
    /// </summary>
    public interface IHumanBone
    {
        int Id { get; }

        /// <summary>
        /// 同じボーンに属するキー
        /// </summary>
        ICollection<IHumanMuscleState> Muscles { get; }

        /// <summary>
        /// 体の末端に向かって連結するボーン
        /// </summary>
        ICollection<IHumanBone> Trees { get; }

        /// <summary>
        /// コピー方法
        /// </summary>
        HumanBoneCopyType CopyType { get; }

        void Mirror(float[] values);

        void Mirror(float[] values, HumanBoneCopyType copyType);
    }

    public abstract class SingletonHumanBoneStateBase<T> : IHumanBone where T : IHumanBone, new()
    {

        private readonly static T singleton = new T();
        protected SingletonHumanBoneStateBase() { }
        public static T GetInstance() => singleton;

        abstract public int Id { get; }

        abstract public ICollection<IHumanMuscleState> Muscles { get; }

        abstract public ICollection<IHumanBone> Trees { get; }

        abstract public HumanBoneCopyType CopyType { get; }

        /// <summary>
        /// ミラーコピー
        /// </summary>
        public void Mirror(float[] values)
        {
            Mirror(values, CopyType);
        }

        /// <summary>
        /// ミラーコピー
        /// </summary>
        public void Mirror(float[] values, HumanBoneCopyType copyType)
        {
            foreach (var muscle in Muscles)
            {
                if (muscle.MateState != NullState.GetInstance())
                {
                    switch (copyType)
                    {
                        case HumanBoneCopyType.Copy:
                            values[muscle.MateState.Id] = values[muscle.Id];
                            break;
                        case HumanBoneCopyType.Trade:
                            if (muscle == muscle.MateState)
                            {
                                values[muscle.Id] = -values[muscle.Id];
                            }
                            else
                            {
                                float tmp = values[muscle.MateState.Id];
                                values[muscle.MateState.Id] = values[muscle.Id];
                                values[muscle.Id] = tmp;
                            }
                            break;
                    }
                }
            }

            foreach (var tree in Trees)
            {
                tree.Mirror(values, copyType);
            }
        }

    }

    public class NullBone : SingletonHumanBoneStateBase<NullBone>
    {
        public override int Id => -1;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Trade;
    }

    public class RootBone : SingletonHumanBoneStateBase<RootBone>
    {
        public override int Id => 0;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
            SpineBone.GetInstance(),
            UpperLegLeftBone.GetInstance()
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Trade;
    }

    public class SpineBone : SingletonHumanBoneStateBase<SpineBone>
    {
        public override int Id => 1;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            SpineFrontBackState.GetInstance(),
            SpineLeftRightState.GetInstance(),
            SpineTwistState.GetInstance()
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
            UpperChestBone.GetInstance()
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Trade;
    }

    public class ChestBone : SingletonHumanBoneStateBase<ChestBone>
    {
        public override int Id => 2;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            ChestFrontBackState.GetInstance(),
            ChestLeftRightState.GetInstance(),
            ChestTwistState.GetInstance()
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
            UpperChestBone.GetInstance()
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Trade;
    }


    public class UpperChestBone : SingletonHumanBoneStateBase<UpperChestBone>
    {
        public override int Id => 3;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            UpperChestFrontBackState.GetInstance(),
            UpperChestLeftRightState.GetInstance(),
            UpperChestTwistRightState.GetInstance()
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
            NeckBone.GetInstance(),
            ShoulderLeftBone.GetInstance()
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Trade;
    }

    public class NeckBone : SingletonHumanBoneStateBase<NeckBone>
    {
        public override int Id => 4;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            NeckDownUpState.GetInstance(),
            NeckLeftRightState.GetInstance(),
            NeckTurnState.GetInstance()
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
            HeadBone.GetInstance()
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Trade;
    }

    public class HeadBone : SingletonHumanBoneStateBase<HeadBone>
    {
        public override int Id => 5;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            HeadDownUpState.GetInstance(),
            HeadTiltState.GetInstance(),
            HeadTurnState.GetInstance()
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Trade;
    }

    public class ShoulderLeftBone : SingletonHumanBoneStateBase<ShoulderLeftBone>
    {
        public override int Id => 6;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            ShoulderDownUpLeftState.GetInstance(),
            ShoulderFrontBackLeftState.GetInstance(),
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
            UpperArmLeftBone.GetInstance()
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Copy;
    }

    public class UpperArmLeftBone : SingletonHumanBoneStateBase<UpperArmLeftBone>
    {
        public override int Id => 7;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            ArmDownUpLeftState.GetInstance(),
            ArmFrontBackLeftState.GetInstance()
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
            LowerArmLeftBone.GetInstance()
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Copy;
    }

    public class LowerArmLeftBone : SingletonHumanBoneStateBase<LowerArmLeftBone>
    {
        public override int Id => 8;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            ForearmStretchLeftState.GetInstance(),
            ForearmTwistLeftState.GetInstance(),
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
            HandLeftBone.GetInstance()
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Copy;
    }

    public class HandLeftBone : SingletonHumanBoneStateBase<HandLeftBone>
    {
        public override int Id => 9;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            HandDownUpLeftState.GetInstance(),
            HandInOutLeftState.GetInstance()
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
            ThumbLeftBone.GetInstance(),
            IndexLeftBone.GetInstance(),
            MiddleLeftBone.GetInstance(),
            RingLeftBone.GetInstance(),
            LittleLeftBone.GetInstance()
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Copy;
    }

    public class UpperLegLeftBone : SingletonHumanBoneStateBase<UpperLegLeftBone>
    {
        public override int Id => 10;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            UpperLegFrontBackLeftState.GetInstance(),
            UpperLegTwistLeftState.GetInstance(),
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
            LowerLegLeftBone.GetInstance()
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Copy;
    }

    public class LowerLegLeftBone : SingletonHumanBoneStateBase<LowerLegLeftBone>
    {
        public override int Id => 11;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            LowerLegFrontBackLeftState.GetInstance(),
            LowerLegTwistLeftState.GetInstance()
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
            FootLeftBone.GetInstance()
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Copy;
    }

    public class FootLeftBone : SingletonHumanBoneStateBase<FootLeftBone>
    {
        public override int Id => 12;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            FootLeftRightLeftState.GetInstance(),
            FootUpDownLeftState.GetInstance(),
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
            ToesLeftBone.GetInstance()
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Copy;
    }

    public class ToesLeftBone : SingletonHumanBoneStateBase<ToesLeftBone>
    {
        public override int Id => 13;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            ToesUpDownLeftState.GetInstance()
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Copy;
    }

    public class ShoulderRightBone : SingletonHumanBoneStateBase<ShoulderRightBone>
    {
        public override int Id => 14;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            ShoulderDownUpRightState.GetInstance(),
            ShoulderFrontBackRightState.GetInstance()
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
            UpperArmRightBone.GetInstance()
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Copy;
    }

    public class UpperArmRightBone : SingletonHumanBoneStateBase<UpperArmRightBone>
    {
        public override int Id => 15;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            ArmDownUpRightState.GetInstance(),
            ArmFrontBackRightState.GetInstance(),
            ArmTwistRightState.GetInstance()
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
            LowerLegRightBone.GetInstance()
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Copy;
    }

    public class LowerArmRightBone : SingletonHumanBoneStateBase<LowerArmRightBone>
    {
        public override int Id => 16;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            ForearmStretchRightState.GetInstance(),
            ForearmTwistRightState.GetInstance()
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
            HandRightBone.GetInstance()
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Copy;
    }

    public class HandRightBone : SingletonHumanBoneStateBase<HandRightBone>
    {
        public override int Id => 17;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            HandDownUpRightState.GetInstance(),
            HandInOutRightState.GetInstance()
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
            ThumbRightBone.GetInstance(),
            IndexRightBone.GetInstance(),
            MiddleRightBone.GetInstance(),
            RingRightBone.GetInstance(),
            LittleRightBone.GetInstance()
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Copy;
    }

    public class UpperLegRightBone : SingletonHumanBoneStateBase<UpperLegRightBone>
    {
        public override int Id => 18;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            UpperLegFrontBackRightState.GetInstance(),
            UpperLegTwistRightState.GetInstance()
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
            LowerLegRightBone.GetInstance()
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Copy;
    }

    public class LowerLegRightBone : SingletonHumanBoneStateBase<LowerLegRightBone>
    {
        public override int Id => 19;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            LowerLegFrontBackRightState.GetInstance(),
            LowerLegTwistRightState.GetInstance()
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
            FootRightBone.GetInstance()
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Copy;
    }

    public class FootRightBone : SingletonHumanBoneStateBase<FootRightBone>
    {
        public override int Id => 20;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            FootLeftRightRightState.GetInstance(),
            FootTwistRightState.GetInstance(),
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
            ToesRightBone.GetInstance()
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Copy;
    }

    public class ToesRightBone : SingletonHumanBoneStateBase<ToesRightBone>
    {
        public override int Id => 21;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            ToesUpDownRightState.GetInstance()
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Copy;
    }


    public class ThumbLeftBone : SingletonHumanBoneStateBase<ThumbLeftBone>
    {
        public override int Id => 22;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            ThumbSpreadLeftState.GetInstance(),
            Thumb1StretchedLeftState.GetInstance(),
            Thumb2StretchedLeftState.GetInstance(),
            Thumb3StretchedLeftState.GetInstance(),
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
            //IndexLeftBone.GetInstance()
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Copy;
    }

    public class IndexLeftBone : SingletonHumanBoneStateBase<IndexLeftBone>
    {
        public override int Id => 23;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            IndexSpreadLeftState.GetInstance(),
            Index1StretchedLeftState.GetInstance(),
            Index2StretchedLeftState.GetInstance(),
            Index3StretchedLeftState.GetInstance(),
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
            //MiddleLeftBone.GetInstance()
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Copy;
    }

    public class MiddleLeftBone : SingletonHumanBoneStateBase<MiddleLeftBone>
    {
        public override int Id => 24;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            MiddleSpreadLeftState.GetInstance(),
            Middle1StretchedLeftState.GetInstance(),
            Middle2StretchedLeftState.GetInstance(),
            Middle3StretchedLeftState.GetInstance(),
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
            //RingLeftBone.GetInstance()
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Copy;
    }

    public class RingLeftBone : SingletonHumanBoneStateBase<RingLeftBone>
    {
        public override int Id => 25;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            RingSpreadLeftState.GetInstance(),
            Ring1StretchedLeftState.GetInstance(),
            Ring2StretchedLeftState.GetInstance(),
            Ring3StretchedLeftState.GetInstance(),
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
            //LittleLeftBone.GetInstance()
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Copy;
    }

    public class LittleLeftBone : SingletonHumanBoneStateBase<LittleLeftBone>
    {
        public override int Id => 26;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            LittleSpreadLeftState.GetInstance(),
            Little1StretchedLeftState.GetInstance(),
            Little2StretchedLeftState.GetInstance(),
            Little3StretchedLeftState.GetInstance(),
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
            //ThumbLeftBone.GetInstance()
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Copy;
    }

    public class ThumbRightBone : SingletonHumanBoneStateBase<ThumbRightBone>
    {
        public override int Id => 27;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            ThumbSpreadRightState.GetInstance(),
            Thumb1StretchedRightState.GetInstance(),
            Thumb2StretchedRightState.GetInstance(),
            Thumb3StretchedRightState.GetInstance(),
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
            //IndexRightBone.GetInstance()
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Copy;
    }

    public class IndexRightBone : SingletonHumanBoneStateBase<IndexRightBone>
    {
        public override int Id => 28;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            IndexSpreadRightState.GetInstance(),
            Index1StretchedRightState.GetInstance(),
            Index2StretchedRightState.GetInstance(),
            Index3StretchedRightState.GetInstance(),
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
            //MiddleRightBone.GetInstance()
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Copy;
    }

    public class MiddleRightBone : SingletonHumanBoneStateBase<MiddleRightBone>
    {
        public override int Id => 29;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            MiddleSpreadRightState.GetInstance(),
            Middle1StretchedRightState.GetInstance(),
            Middle2StretchedRightState.GetInstance(),
            Middle3StretchedRightState.GetInstance(),
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
            //RingRightBone.GetInstance()
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Copy;
    }

    public class RingRightBone : SingletonHumanBoneStateBase<RingRightBone>
    {
        public override int Id => 30;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            RingSpreadRightState.GetInstance(),
            Ring1StretchedRightState.GetInstance(),
            Ring2StretchedRightState.GetInstance(),
            Ring3StretchedRightState.GetInstance(),
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
            //LittleRightBone.GetInstance()
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Copy;
    }

    public class LittleRightBone : SingletonHumanBoneStateBase<LittleRightBone>
    {
        public override int Id => 31;
        public override ICollection<IHumanMuscleState> Muscles => new IHumanMuscleState[] {
            LittleSpreadRightState.GetInstance(),
            Little1StretchedRightState.GetInstance(),
            Little2StretchedRightState.GetInstance(),
            Little3StretchedLeftState.GetInstance(),
        };
        public override ICollection<IHumanBone> Trees => new IHumanBone[] {
        };
        public override HumanBoneCopyType CopyType => HumanBoneCopyType.Copy;
    }


}