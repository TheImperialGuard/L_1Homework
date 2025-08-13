using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/SymbolsSetsListConfig", fileName = "SymbolsSetsListConfig")]
    public class SymbolsSetsListConfig : ScriptableObject
    {
        [SerializeField] private List<SymbolsSetConfig> _configsList;

        public IReadOnlyList<SymbolsSetConfig> ConfigsList => _configsList;

        public SymbolsSetConfig GetConfigBy(GameMods gameMod)
        {
            SymbolsSetConfig config = _configsList.Where((config) => config.GameMode == gameMod).FirstOrDefault();

            if (config == null)
                throw new ArgumentException("Данный режим игры не поддерживается", nameof(gameMod));

            return config;
        }
    }
}
