using OpenTK.Mathematics;
using Yasai.Graphics.Shapes;

namespace Pong.Components
{
    public class Paddle : Box
    {
        public override Vector2 Size => new(20, 100);

        protected enum Direction
        {
            Up = -1,
            Down = 1,
            Idle = 0
        }
    }
}