using DIKUArcade.Math;
using DIKUArcade.Input;
using DIKUArcade.State;
using DIKUArcade.Events;
using DIKUArcade.Graphics;

namespace Breakout.GameStates;
/// <Summary>
/// Skal gennem StateMachine og SwitchState() kunne ramme en overgang til
/// Game Running, hvor at spillet således implementeres og Render/Updater løbende
/// mens spilleren spiller
/// <param name="fstLvL"> Level1.txt fra ASCII-Filerne under Assets </param>
/// <param name="Instance"> Undersøge hvilken Instance spillet er i </param>
/// </Summary>
public class GameRunning : IGameState {
    private static GameRunning? instance;
    private GameEventBus eventBus;
    private PlayerHP playerHp;
    private Player player;
    private Ball ball;
    private Block block;
    private playerImage playerImg;
    private BlockImages blockImg;
    private Levels levels;
    private Score points;
    private int fstlvl = 1;
    public static GameRunning GetInstance() {
        if (GameRunning.instance == null) {
            GameRunning.instance = new GameRunning();
            GameRunning.instance.ResetState();
        }
        return GameRunning.instance;
    }

public GameRunning() {
    eventBus = BreakoutBus.GetBus();      
    playerImg = new playerImage();
    blockImg = new BlockImages();
    levels = new Levels();
    player = new Player(playerImg.playerShape, playerImg.playerImg);
    ball =  new Ball(new Vec2F(0.3f, 0.1f), 
            new Image(Path.Combine("Assets", "Assets","Images", "ball.png")));
    eventBus.Subscribe(GameEventType.PlayerEvent, player);
    levels.LvlCreation(fstlvl);
    points = new Score();
    playerHp = new PlayerHP();
}

public void IterateBalls() {
    ball.Shape.Move();
    if (ball.Shape.Position.X >= 0.95f || ball.Shape.Position.X <= 0.0f) {
        ball.Direction.X *= -1;
    }

    if (ball.Shape.Position.Y >= 0.95f) {
        ball.Direction.Y *=  -1;
    }

    if (ball.Shape.Position.Y <= 0.0f){
        ball =  new Ball(new Vec2F(0.3f, 0.1f), 
                new Image(Path.Combine("Assets", "Assets","Images", "ball.png")));
        playerHp.LoseHealth();
    }

    var playerCollision = DIKUArcade.Physics.CollisionDetection.Aabb
        (ball.Shape.AsDynamicShape(), player.shape);
        if (playerCollision.Collision) {
            ball.Direction.Y *= -1;
        }
    
    levels.blockList.Iterate(block => {
        var blockColission = DIKUArcade.Physics.CollisionDetection.Aabb
            (ball.Shape.AsDynamicShape(), block.Shape);
        if (blockColission.Collision){
            block.Hit();
            if (block.IsDeleted()){
                points.UpdateScore(block.value);
            }
            ball.Direction.Y *=  -1;
        }
    });
}

public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        switch (action) {
            case (KeyboardAction.KeyPress):
                switch(key){
                    case (KeyboardKey.Right):
                        eventBus.RegisterEvent (new GameEvent {
                            EventType = GameEventType.PlayerEvent,
                            Message = "PLAYER_MOVE_RIGHT",
                        });
                        break;
                    case (KeyboardKey.Left):
                        eventBus.RegisterEvent (new GameEvent {
                            EventType = GameEventType.PlayerEvent,
                            Message = "PLAYER_MOVE_LEFT",
                        });
                        break;
                    case (KeyboardKey.Escape):
                            eventBus.RegisterEvent(new GameEvent {
                                EventType = GameEventType.GameStateEvent,
                                Message = "SWITCH_GAME_STATE",
                                StringArg1 = "CLOSE_WINDOW"
                            });
                            break;
                    case (KeyboardKey.P):
                            eventBus.RegisterEvent(new GameEvent {
                                EventType = GameEventType.GameStateEvent,
                                Message = "SWITCH_GAME_STATE",
                                StringArg1 = "GAME_IS_PAUSED"
                            });
                            break;
                    case (KeyboardKey.C):
                        fstlvl++;
                        levels.blockList.ClearContainer();
                        levels.LvlCreation(fstlvl);
                        break;
                }
            break;
        }
        switch (action) {
            case(KeyboardAction.KeyRelease):
                switch(key) {
                    case (KeyboardKey.Right):
                        eventBus.RegisterEvent (new GameEvent {
                            EventType = GameEventType.PlayerEvent,
                            Message = "PLAYER_STOP_MOVING_RIGHT",
                        });
                        break;
                    case (KeyboardKey.Left):
                        eventBus.RegisterEvent (new GameEvent {
                            EventType = GameEventType.PlayerEvent,
                            Message = "PLAYER_STOP_MOVING_LEFT",
                        });
                        break;
                }
            break;
        }
    }

public void ResetState() {
    }

    public void UpdateState() {
        eventBus.ProcessEventsSequentially();
        player.Move();
        IterateBalls();
    }

    public void RenderState() {
        player.Render();
        levels.blockList.RenderEntities();
        ball.RenderEntity();
        points.Render();
        playerHp.RenderHealth();
    }
}