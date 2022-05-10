// Задаем константу для пары сообщений о событиях, что позволяет систематизировать сообщения, одновременно избавляя от необходимости вводить строку 
// сообщения в разных местах.
public static class GameEvent
{
    public const string ENEMY_HIT = "ENEMY_HIT";
    public const string SPEED_CHANGED = "SPEED_CHANGED";
    public const string WEATHER_UPDATED = "WEATHER_UPDATED";
}
