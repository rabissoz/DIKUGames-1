using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.BlockTypes;
/// <Summary>
/// Hardened er en blockType som skal have dobbelt hp, dobbelt value og skifte 
/// billede, hvis den beskadiges
/// <param name="damagedBlock"> Billedet blocken skal have efter at blive beskadiget </param>
/// </Summary>

public class Hardened : Block {
    private Image? damagedBlock;
    public Hardened(DynamicShape shape, IBaseImage image) : base(shape, image) {
        this.HP = 2;
        this.value = 200;
    }
    public override void Hit() {
        this.HP-=1;
        if (this.HP == 1){
            damagedBlock = new Image(Path.Combine("Assets", "Assets", "Images", "green-block-damaged.png"));
            this.Image = damagedBlock;}
            
        if (this.HP <= 0){
            this.DeleteEntity();
        }
        
    }

}