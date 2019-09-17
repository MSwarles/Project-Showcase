using SuperMetroid.PlayerStates;

namespace SuperMetroid.ActorComponents
{
    /// <summary>
    /// Interface for Graphics Components.
    /// </summary>
    public interface IGraphicsComponent : IComponent
    {
        string CurrentAnimationKey { get; }
        int? CurrentFrameIndex { get; }
        bool? IsDoneAnimating { get; }
        IState State { get; set; }

        void ResetAnimation();
        void ChangeAnimation(string animationName);
        void AdjustAnimation(string animationName);
    }
}
