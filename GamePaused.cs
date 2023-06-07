using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.State;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout.GameStates;
/// <Summary>
/// Skal gennem StateMachine og SwitchState() kunne ramme en overgang til
/// Game Paused, hvor at spillet således pauses, ændre fane til Game_Paused
/// hvor spillet enten kan fortsættes eller man kan tage hen til Main Menu
/// <param name="menuButtons"> Knapperne der vises, når man pauser spillet </param>
/// <param name="Instance"> Undersøge hvilken Instance spillet er i </param>
/// </Summary>
public class GamePaused : IGameState {
    private static GamePaused? instance = null;
    private Text[]? menuButtons;
    private int activeMenuButton;
    private int maxMenuButtons;
    public static GamePaused GetInstance() {
        if (GamePaused.instance == null) {
        GamePaused.instance = new GamePaused();
        GamePaused.instance.ResetState();
        }
    return GamePaused.instance;
    }
    public void ResetState(){
        Text continueButton = new Text("Continue", new Vec2F(0.4f, 0.4f), new Vec2F(0.3f, 0.3f));
        Text mainMenuButton = new Text("Main Menu", new Vec2F(0.4f, 0.2f), new Vec2F(0.3f, 0.3f));
        continueButton.SetColor(new Vec3F(0, 0, 0));
        mainMenuButton.SetColor(new Vec3F(0, 0, 0));
        menuButtons = new Text[]{continueButton, mainMenuButton};

        activeMenuButton = 0;
        maxMenuButtons = menuButtons.Length;
    }
    public void UpdateState() {
            BreakoutBus.GetBus().ProcessEventsSequentially();
    }
    public void RenderState() {
        foreach (Text i in menuButtons) {
            i.RenderText();
        }
        foreach(Text j in menuButtons) {
            if (activeMenuButton == 0) {
                menuButtons[0].SetColor(new Vec3F(1,0,0));
            } else {
                menuButtons[0].SetColor(new Vec3F(1,1,1));
            }

            if (activeMenuButton == 1) {
                menuButtons[1].SetColor(new Vec3F(1,0,0));
            } else {
                menuButtons[1].SetColor(new Vec3F(1,1,1));
            }
    
        }
    }
    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        switch (action) {
            case KeyboardAction.KeyPress:
                switch (key) {
                    case KeyboardKey.Up:
                        if (activeMenuButton == 0) {
                            break;
                        } else {
                            activeMenuButton--;
                            break;
                        }

                    case KeyboardKey.Down:
                        if (activeMenuButton == maxMenuButtons - 1) {
                            break;
                        } else {
                            activeMenuButton++;
                            break;
                        }
                    case KeyboardKey.Enter:
                        switch (activeMenuButton) {
                            case 0:
                                BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                                    EventType = GameEventType.GameStateEvent,
                                    Message = "SWITCH_GAME_STATE",
                                    StringArg1 = "GAME_IS_RUNNING"
                                });
                                break;
                            case 1:
                                BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                                    EventType = GameEventType.GameStateEvent,
                                    Message = "SWITCH_GAME_STATE",
                                    StringArg1 = "MAIN_MENU"
                                });
                                break;
                            default:
                                break;
                        }
                        break;
                    }
                break;
            default:
                break;
        }
    }

}
