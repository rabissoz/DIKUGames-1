using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.BlockTypes;
/// <Summary>
/// Unbreakable skal have ingen value, og ikke kunne beskadiges.
/// </Summary>
public class Unbreakable : Block {
    
    public Unbreakable(DynamicShape shape, IBaseImage image) : base(shape, image) {
        this.value = 0;
    }
    public override void Hit() {

        }
}