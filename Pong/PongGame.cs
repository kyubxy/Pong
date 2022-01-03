using System.Drawing;
using OpenTK.Mathematics;
using Pong.Components;
using Yasai;
using Yasai.Graphics;
using Yasai.Graphics.Text;
using Yasai.Resources.Stores;
using Yasai.Structures.DI;

namespace Pong
{
    public class PongGame : Game
    {
        private PlayerPaddle player;
        private OpponentPaddle opponent;
        private Ball ball;

        SpriteText message;

        public GameState State;

        public PongGame()
        {
            State = new GameState();
        }
            
        public override void Load(DependencyContainer dependencies)
        {
            FontStore fonts = new FontStore();
            fonts.LoadResource("font.fnt");
            
            Children = new IDrawable[]
            {
                player = new PlayerPaddle
                {
                    Position = new Vector2(20)
                },
                opponent = new OpponentPaddle
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    Position = new Vector2(-20, 20)
                },
                ball = new Ball
                {
                    Position = new Vector2(90,20)
                },
                message = new SpriteText("", fonts.GetResource("font"))
                {
                    Anchor = Anchor.Center
                }
            };
            
            State.Outcome.OnSet += outcome =>
            {
                switch (outcome)
                {
                    case Outcome.Win:
                        message.Text = "You win";
                        gameOver();
                        break;
                    case Outcome.Lose:
                        message.Text = "You lose";
                        gameOver();
                        break;
                }
            };
            
            dependencies.Register<PlayerPaddle>(player);
            dependencies.Register<OpponentPaddle>(opponent);
            dependencies.Register<Ball>(ball);
            dependencies.Register<GameState>(State);

            base.Load(dependencies);
        }

        void gameOver()
        {
            opponent.Enabled = false;
            ball.Enabled = false;
            player.Enabled = false;
        }
    }
}