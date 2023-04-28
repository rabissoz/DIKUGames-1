using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;


namespace Breakout;

public class Game  : DIKUGame, IGameEventProcessor {
    private Player player;
    private GameEventBus eventBus;
    
    public Game(WindowArgs windowArgs) : base(windowArgs){
        eventBus = new GameEventBus();
        eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.PlayerEvent});
        window.SetKeyEventHandler(KeyHandler);
        player = new Player(
            new DynamicShape(new Vec2F(0.375f, 0.02f), new Vec2F(0.25f, 0.03f)),
            new Image(Path.Combine("Assets", "Assets", "Images", "player.png")));
            eventBus.Subscribe(GameEventType.InputEvent, this);
            eventBus.Subscribe(GameEventType.PlayerEvent, player);
            
    }
        private void KeyPress(KeyboardKey key)
        {
            switch (key){
                case (KeyboardKey.Left):
                    eventBus.RegisterEvent(new GameEvent{
                        EventType = GameEventType.PlayerEvent,
                        Message = "PLAYER_MOVE_LEFT",
                    });
                    break;
                case (KeyboardKey.Right):
                    eventBus.RegisterEvent(new GameEvent{
                        EventType = GameEventType.PlayerEvent,
                        Message = "PLAYER_MOVE_RIGHT",
                    });
                    break;
                case (KeyboardKey.Escape):
                    window.CloseWindow();
                    break;
            }
        }
        private void KeyRelease(KeyboardKey key)
        {
            switch (key){
                case (KeyboardKey.Left):
                    eventBus.RegisterEvent(new GameEvent{
                        EventType = GameEventType.PlayerEvent,
                        Message = "PLAYER_STOP_MOVING_LEFT",
                    });
                    break;
                case (KeyboardKey.Right):
                    eventBus.RegisterEvent(new GameEvent{
                        EventType = GameEventType.PlayerEvent,
                        Message = "PLAYER_STOP_MOVING_RIGHT",
                    });
                    break;
                case (KeyboardKey.Space):
                    player.Render();
                    break;
            }
        }
        private void KeyHandler(KeyboardAction action, KeyboardKey key)
        {
            switch (action){
                case (KeyboardAction.KeyPress):
                    KeyPress(key);
                    break;
                case (KeyboardAction.KeyRelease):
                    KeyRelease(key);
                    break;
            }
        }
        public override void Render()
        {
            player.Render();
        }
    public override void Update() {
        eventBus.ProcessEventsSequentially();
        player.Move();
    }
        public void ProcessEvent(GameEvent gameEvent)
        {
            if (gameEvent.EventType == GameEventType.WindowEvent)
            {
                switch (gameEvent.Message)
                {
                    case "CLOSE_WINDOW":
                        window.CloseWindow();
                        break;
                }
            }

}
}
