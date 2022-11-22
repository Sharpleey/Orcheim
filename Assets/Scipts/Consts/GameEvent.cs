// Задаем константу для пары сообщений о событиях, что позволяет систематизировать сообщения, одновременно избавляя от необходимости вводить строку 
// сообщения в разных местах.

public static class GameEvent
{
    public const string NEW_GAME_MODE_ORCCHEIM = "NEW_GAME_MODE_ORCCHEIM";
    public const string STARTING_NEW_GAME_MODE_ORCCHEIM = "STARTING_NEW_GAME_MODE_ORCCHEIM";

    public const string GAME_OVER = "GAME_OVER";
}