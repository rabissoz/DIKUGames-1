using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout;
public class Player : IGameEventProcessor{
    private Entity entity;
    private float moveLeft = 0.0f;
    private float moveRight = 0.0f;
    private const float MOVEMENT_SPEED = 0.01f;
    private DynamicShape shape;
    public Player(DynamicShape shape, IBaseImage image) {
        entity = new Entity(shape, image);
        this.shape = shape;
    }
    public void Render() {
        entity.RenderEntity();
    }
    public void Move() {
        shape.Move();
        if (shape.Position.X <= 0.0f) {
            shape.Position.X = 0.0f;
        }
        else if (shape.Position.X >= 0.75f){
            shape.Position.X = 0.75f;
        }
    }
    private void UpdateDirection() {
        float direction = moveLeft + moveRight;
        shape.Direction = new Vec2F(direction,0.0f);
    }
    public void SetMoveLeft(bool val) {
        if (val == true){
            moveLeft = moveLeft - MOVEMENT_SPEED;
        }
        else{
            moveLeft = 0;
        }
        UpdateDirection();
    }
    public void SetMoveRight(bool val) {
        if (val == true){
            moveRight = moveRight + MOVEMENT_SPEED;
        }
        else{
            moveRight = 0;
        }
        UpdateDirection();
    }
    public Vec2F GetPosition() {
        return shape.Position;
    }

    public void ProcessEvent(GameEvent gameEvent){
        if (gameEvent.EventType == GameEventType.PlayerEvent){
            switch (gameEvent.Message){
                case "PLAYER_MOVE_RIGHT":
                    SetMoveRight(true);
                    break;
            }
        }
        if (gameEvent.EventType == GameEventType.PlayerEvent){
            switch(gameEvent.Message){
                case "PLAYER_MOVE_LEFT":
                    SetMoveLeft(true);
                    break;
            }
        if (gameEvent.EventType == GameEventType.PlayerEvent){
            switch(gameEvent.Message){
                case "PLAYER_STOP_MOVING_RIGHT":
                    SetMoveRight(false);
                    break;
            }
        }
        if (gameEvent.EventType == GameEventType.PlayerEvent){
            switch(gameEvent.Message){
                case "PLAYER_STOP_MOVING_LEFT":
                    SetMoveLeft(false);
                    break;
            }
        }
                
        }
    }
}

