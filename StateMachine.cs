using DIKUArcade.Events;
using DIKUArcade.State;


namespace Breakout.GameStates;
/// <Summary>
/// Vha. Switchstate(), så skal StateMachine kunne ændre spillets State
/// Løbende, så den forbindes med spillerens 
/// Handling ift. GameRunning, GamePaused og MainMenu
/// <param name="ActiveState"> Den aktuelle State som spillet er i </param>
/// </Summary>
    public class StateMachine : IGameEventProcessor {
        public IGameState ActiveState { get; private set; }

        public StateMachine() {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            ActiveState = MainMenu.GetInstance();
            GameRunning.GetInstance();
        }
        private void SwitchState(GameStateType stateType) {
            switch (stateType) {
                case (GameStateType.MainMenu):
                    ActiveState = MainMenu.GetInstance();
                    break;
                case (GameStateType.GameRunning):
                    ActiveState = GameRunning.GetInstance();
                    break;
                case (GameStateType.GamePaused):
                    ActiveState = GamePaused.GetInstance();
                    break;

            }
        }

        public void ProcessEvent(GameEvent gameEvent) {
                if (gameEvent.EventType == GameEventType.GameStateEvent && 
                    gameEvent.Message == "SWITCH_GAME_STATE") {
                    switch (gameEvent.StringArg1) {
                        case ("MAIN_MENU"):
                            SwitchState(GameStateType.MainMenu);
                            break;
                        case ("GAME_IS_RUNNING"):
                            SwitchState(GameStateType.GameRunning);
                            break;
                        case ("GAME_IS_PAUSED"):
                            SwitchState(GameStateType.GamePaused);
                            break;
                        
                    }
                if (gameEvent.EventType == GameEventType.WindowEvent){
                    switch (gameEvent.Message)
                    {
                        case "CLOSE_WINDOW":
                            //window.CloseWindow();
                            break;
                    }
                }
            }
        }
    }
