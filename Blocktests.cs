using Breakout;
using DIKUArcade.Entities;
using DIKUArcade.GUI;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System;
using Breakout.GameStates;

namespace BreakoutTests;

public class BlockTests
{
    private Block? Block;
    private Block? SndBlock;

    [SetUp]
    public void Setup()
    {
        DIKUArcade.GUI.Window.CreateOpenGLContext();

    }
    

    [Test]
    public void testBlockIsEntity()
    {
    }

    [Test]
    public void BlockHasHealthTest() 
    {

    } 

    [Test]
    public void testBlockHit() 
    {
    } 

    [Test]
    public void testBlockISDeleted() 
    {

    } 
}
