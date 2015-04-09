using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Yugioh_AtemReturns.GameObjects
{
    public class Animation
    {
        public float Time { get; set; }
        public bool IsDone { get; set; }
        public bool IsDoing { get; set; }

        public Animation(float time)
        {
            Time = time;
            IsDone = false;
            IsDoing = false;
        }
    }

    public class MoveTo : Animation
    {
        public Vector2 NextPosition { get; set; }

        public MoveTo(float time, Vector2 nextposition) : base(time)
        {
            NextPosition = nextposition;
        }
    }

    public class ScaleTo : Animation
    {
        public Vector2 NextScale { set; get; }

        public ScaleTo(float time, Vector2 nextscale) : base(time)
        {
            NextScale = nextscale;
        }
    }

    public class RotateTo : Animation
    {
        public float NextRotation { set; get; }

        public RotateTo(float time, float nextrotation)
            : base(time)
        {
            NextRotation = nextrotation;
        }
    }
}
