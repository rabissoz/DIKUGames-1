namespace Breakout.GameStates;
/// <Summary>
/// GameStateType er blot de forskellige States spillet kan være i
/// skal bruges til reference i GameState.
/// </Summary>
public enum GameStateType {
    MainMenu,
    GameRunning,
    GamePaused,
}
/// <Summary>
/// StateTransformer skal hjælpe med overgangen mellem States ved at kommunikere med StateMachine
/// TransformStringToState transformere string til State, hvor
/// TransformStateToString gør det modsatte, hvilket gør StateMachine kan kommunikere mellem de to
/// </Summary>
public class StateTransformer {
    public static GameStateType TransformStringToState (string state) {
        switch (state) {
            case "MAIN_MENU":
                return GameStateType.MainMenu;
            case "GAME_IS_RUNNING":
                return GameStateType.GameRunning;
            case "GAME_IS_PAUSED":
                return GameStateType.GamePaused;
            default:
                throw new System.ArgumentException("Invalid Argument");
        }
    }
    public static string TransformStateToString (GameStateType state) {
        switch (state) {
            case GameStateType.MainMenu:
                return "MAIN_MENU";
            case GameStateType.GameRunning:
                return "GAME_IS_RUNNING";
            case GameStateType.GamePaused:
                return "GAME_IS_PAUSED";
            default:
                throw new System.ArgumentException("Invalid Argument");
        }
    }
}


