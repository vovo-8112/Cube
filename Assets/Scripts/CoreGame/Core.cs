using System;

public class Core {
  private static GameState CurrentGameState;

  public static void SetGameStat(GameState gameState) {
    CurrentGameState = gameState;
  }

  public static GameState GetGameState() {
    return CurrentGameState;
  }
}