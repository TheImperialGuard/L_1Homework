using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(GameMods gameMode)
        {
            GameMode = gameMode;
        }

        public GameMods GameMode { get; }
    }
}
