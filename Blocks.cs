using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout;

public class Blocks : Entity {
    
    private int hp;
    public int HP
    {
        get { return hp; }
        set { hp = value; }
    }
    private int value;
    
    public Blocks(DynamicShape shape, IBaseImage image) : base(shape, image){

    }
}