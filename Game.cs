using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Events;
using Breakout.GameStates;


namespace Breakout;
public class Game : DIKUGame {
    private GameEventBus eventBus;
    private StateMachine stateMachine;

    public Game(WindowArgs windowArgs) : base(windowArgs) {
            eventBus = BreakoutBus.GetBus();
            eventBus.InitializeEventBus(new List<GameEventType> {
                GameEventType.InputEvent, GameEventType.PlayerEvent,
                GameEventType.WindowEvent, GameEventType.GameStateEvent
                }); 
            stateMachine = new StateMachine();
            window.SetKeyEventHandler(KeyHandler);
        }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) { 
            stateMachine.ActiveState.HandleKeyEvent(action, key);
        } 
    
    public override void Render()
    {
        stateMachine.ActiveState.RenderState();
    }

    public override void Update()
    {
        stateMachine.ActiveState.UpdateState();
    }


}