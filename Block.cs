using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout;
/// <Summary>
/// Standard Block som skal kunne overføres videre til special BlockTypes
/// Block skal være en Entity, have health og value.
/// <param name="hp"> Mægnden af Health som en block skal have i int </param>
/// <param name="val"> Mængden af point man får ved at ødelægge en block i int </param>
/// </Summary>
public class Block : Entity {
    private int hp;

    public int HP {
    get {return hp;}
    set {hp = value;} 
    }

    private int val;

    public int value {
    get {return val;}
    set {val = value;} 
    }


    public Block(DynamicShape shape, IBaseImage image) : base(shape, image) {
        this.hp = 1;
        this.value = 100;
    }
/// <Summary>
/// Hvis Hit() kalder skal blocken miste liv, såfremt den rammer 0 så skal den slettes
/// </Summary>
    public virtual void Hit() { 
        this.hp-=1;
        if (this.hp <= 0) { 
            this.DeleteEntity();
        }
    }
}