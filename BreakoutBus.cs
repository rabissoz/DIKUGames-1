using DIKUArcade.Events;

namespace Breakout;
/// <Summary>
/// Breakoutbus skal kunne registere events i resten af GameState.
/// </Summary>
public static class BreakoutBus {
    private static GameEventBus? eventBus;
    public static GameEventBus GetBus() {
        return BreakoutBus.eventBus ?? (BreakoutBus.eventBus = new GameEventBus());
    }
}