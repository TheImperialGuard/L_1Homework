﻿using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(SymbolsSetConfig symbolsSetConfig)
        {
            SymbolsSetConfig = symbolsSetConfig;
        }

        public SymbolsSetConfig SymbolsSetConfig { get; }
    }
}
