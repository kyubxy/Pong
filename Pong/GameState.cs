using Yasai.Structures;

namespace Pong;

public class GameState
{
    public Bindable<Outcome> Outcome;

    public GameState()
    {
        Outcome = new Bindable<Outcome>(Pong.Outcome.Ongoing);
    }
}