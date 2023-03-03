using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga;
public class Player {
    private Entity entity;
    private float moveLeft = 0.0f;
    private float moveRight = 0.0f;
    private const float MOVEMENT_SPEED = 0.1f;
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
        else if (shape.Position.X >= 1.0f){
            shape.Position.X = 0.9f;
        }
    }
    private void UpdateDirection(float direction) {
        shape.Direction.X = direction;
    }
    public void SetMoveLeft(bool val) {
        if (val == true){
            moveLeft = moveLeft - MOVEMENT_SPEED;
        }
        else{
            moveLeft = 0;
        }
        UpdateDirection(moveLeft);
    }
    public void SetMoveRight(bool val) {
        if (val == true){
            moveRight = moveRight + MOVEMENT_SPEED;
        }
        else{
            moveRight = 0;
        }
        UpdateDirection(moveRight);
    }
    public Vec2F GetPosition() {
        return shape.Position;
    }
}

