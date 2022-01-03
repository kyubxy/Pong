using System.Drawing;
using OpenTK.Windowing.Common;
using Yasai.Structures.DI;

namespace Pong.Components
{
    public class OpponentPaddle : Paddle
    {
        public override Color Colour => Color.DarkGray;

        private Ball ball;
        private Random rand;

        public OpponentPaddle()
        {
            rand = new Random();
        }

        public override void Load(DependencyContainer container)
        {
            base.Load(container);
            ball = container.Resolve<Ball>();
        }


        public override void Update(FrameEventArgs args)
        {
            base.Update(args);

            // have the opponent lag behind the ball
            Y += 0.1f * (ball.Y - Y);
        }
    }
}