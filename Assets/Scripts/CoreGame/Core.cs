public class Core {
  private static GameState _currentGameState;

  public static void SetGameStat(GameState gameState) {
    _currentGameState = gameState;
  }

  public static GameState GetGameState() {
    return _currentGameState;
  }
}