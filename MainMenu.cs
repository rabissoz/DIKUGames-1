using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.State;
using DIKUArcade.Math;
using DIKUArcade.Events;


namespace Breakout.GameStates;
/// <Summary>
/// Vha. Switchstate(), så skal StateMachine kunne ændre spillets State
/// Løbende, så den forbindes med spillerens 
/// Handling ift. GameRunning, GamePaused og MainMenu
/// <param name="bgImg"> baggrundsbilledet for main menu </param>
/// <param name="menuButtons"> Knapperne som skal fremvises på Main Menu </param>
/// </Summary>
    public class MainMenu : IGameState {
        private static MainMenu? instance = null;
        private Entity bgImg;
        private Text[] menuButtons;
        private int activeButtons;
        private int maxNumofButtons;
        public MainMenu() {
            ResetState();
        }
        public static MainMenu GetInstance() {
            if (MainMenu.instance == null) {
            MainMenu.instance = new MainMenu();
            MainMenu.instance.ResetState();
            }
        return MainMenu.instance;
        }

        public void ResetState(){
            bgImg = new Entity(
                new DynamicShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)), 
                new Image(Path.Combine("Assets", "Assets", "Images", "BreakoutTitleScreen.png")));

            Text newGameButton = new Text("New Game", new Vec2F(0.35f, 0.4f), new Vec2F(0.3f, 0.3f));
            Text quitButton = new Text("Quit", new Vec2F(0.4f, 0.2f), new Vec2F(0.3f, 0.3f));
            menuButtons = new Text[]{newGameButton, quitButton};

            activeButtons = 0;
            maxNumofButtons = menuButtons.Length;
        }

        public void UpdateState() {
            BreakoutBus.GetBus().ProcessEventsSequentially();
        }

        public void RenderState() {
            bgImg.RenderEntity();
            foreach (Text b in menuButtons) {
                b.RenderText();
            }
            foreach(Text i in menuButtons) {
                if (activeButtons == 0) {
                    menuButtons[0].SetColor(new Vec3F(1,0,0));
                } else {
                    menuButtons[0].SetColor(new Vec3F(1,1,1));
                }

                if (activeButtons == 1) {
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
                            if (activeButtons == 0) {
                                break;
                            } else {
                                activeButtons--;
                                break;
                            }

                        case KeyboardKey.Down:
                            if (activeButtons == maxNumofButtons - 1) {
                                break;
                            } else {
                                activeButtons++;
                                break;
                            }
                        case KeyboardKey.Enter:
                            switch (activeButtons) {
                                case 0:
                                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                                        EventType = GameEventType.GameStateEvent,
                                        Message = "SWITCH_GAME_STATE",
                                        StringArg1 = "GAME_IS_RUNNING"
                                    });
                                    break;
                                case 1:
                                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                                        EventType = GameEventType.WindowEvent,
                                        Message = "CLOSE_WINDOW",
                                        StringArg1 = "CLOSE_WINDOW"
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

