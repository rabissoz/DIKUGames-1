using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.BlockTypes;

namespace Breakout;
/// <Summary>
/// BlockImages skal sætte størrelsen og billedet for block, når det aflæses fra ASCII-Filen
/// </Summary>

public class BlockImages {

    public EntityContainer<Block>? blocks {get;}

/// <Summary>
/// blockImg() finder vej til billedet for blocken
/// </Summary>
    public Image blockImg(string blockType) {
        return new Image(Path.Combine("Assets", "Assets", "Images", blockType));
    }
/// <Summary>
/// returnere hvilken form for Block det skal være ud fra matchende blocktype
/// </Summary>
    public Block getBlockImg(float fstVec, float sndVec, string blockTypeName, BlockType type) {
        if (type == BlockType.Hardened){
            return new Hardened(new DynamicShape(new Vec2F(fstVec, sndVec), new Vec2F(0.1f, 0.03f)), blockImg(blockTypeName));
        }
        if (type == BlockType.Unbreakable){
            return new Unbreakable(new DynamicShape(new Vec2F(fstVec, sndVec), new Vec2F(0.1f, 0.03f)), blockImg(blockTypeName));
        }        
        else 
            return new Normal(new DynamicShape(new Vec2F(fstVec, sndVec), new Vec2F(0.1f, 0.03f)), blockImg(blockTypeName));
    }
}