using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using Yasai.Graphics;
using Yasai.Graphics.Shapes;
using Yasai.Structures.DI;
using Rectangle = System.Drawing.Rectangle;

namespace Pong.Components;

public sealed class Ball : Box
{
    private float Speed = 8f;
    
    enum Direction
    {
        Right = 1,
        Left = -1
    }

    private Direction direction = Direction.Right;

    private Random rand;
    
    public Ball()
    {
        Position = new Vector2(20);
        Size = new Vector2(69);
        rand = new Random();
    }

    private PlayerPaddle player;
    private OpponentPaddle opponent;

    private Vector2 velocity => new (Speed * (float) direction, rise);
    private float rise;

    private GameState state;

    public override void Load(DependencyContainer container)
    {
        base.Load(container);

        player = container.Resolve<PlayerPaddle>();
        opponent = container.Resolve<OpponentPaddle>();
        state = container.Resolve<GameState>();
    }

    public override void Update(FrameEventArgs args)
    {
        base.Update(args);
        
        // move the ball
        Position += velocity;

        // bounce on either side
        if (isColliding(opponent, this))
        {
            direction = Direction.Left;
            rise = (Y - (opponent.AbsoluteTransform.Position.Y + opponent.Height / 2)) / opponent.Height / 2 *
                   (10 + rand.Next(5));
        }
        else if (isColliding(player, this))
        {
            direction = Direction.Right;
            rise = (Y - (player.AbsoluteTransform.Position.Y + player.Height / 2)) / player.Height / 2 *
                   (10 + rand.Next(5));
        }
        
        // bounce on top and bottom walls
        if (Y + Height > 768 || Y < 0)
            rise *= -1;
        
        // determine game over
        if (X > 1366)
            state.Outcome.Value = Outcome.Win;
        else if (X < 0)
            state.Outcome.Value = Outcome.Lose;
    }

    bool isColliding(Drawable a, Drawable b)
    {
        var absA = a.AbsoluteTransform.Position;
        var absB = b.AbsoluteTransform.Position;

        var rectA = new Rectangle((int)absA.X, (int)absA.Y, (int)a.Width, (int)a.Height);
        var rectB = new Rectangle((int)absB.X, (int)absB.Y, (int)b.Width, (int)b.Height);

        return rectA.IntersectsWith(rectB);
    }
}