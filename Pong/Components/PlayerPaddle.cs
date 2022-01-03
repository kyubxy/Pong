using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Pong.Components
{
    public class PlayerPaddle : Paddle
    {
        private float speed = 10f;

        private Direction direction;
        
        public override void KeyDown(KeyboardKeyEventArgs args)
        {
            base.KeyDown(args);
            switch (args.Key)
            {
                case Keys.Up:
                    direction = Direction.Up;
                    break;
                case Keys.Down:
                    direction = Direction.Down;
                    break;
            }

            if (args.Key == Keys.Space)
                speed = 20f;
        }

        public override void KeyUp(KeyboardKeyEventArgs args)
        {
            base.KeyUp(args);
            
            if (args.Key == Keys.Space)
                speed = 10f;
            else
                direction = Direction.Idle;
        }

        public override void Update(FrameEventArgs args)
        {
            base.Update(args);
            Y = Clamp(Y + (float) direction * speed, 768 - Height, 0);
        }

        float Clamp(float x, float max, float min)
        {
            if (x >= max)
                return max;
            
            if (x <= min)
                return min;
            
            return x;
        }
    }
}