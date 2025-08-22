using Assets._Project.Develop.Runtime.Configs.Meta.Wallet;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/SymbolSequenceGameConfig", fileName = "SymbolSequenceGameConfig")]
    public class SymbolSequenceGameConfig : ScriptableObject
    {
        [SerializeField] private List<SymbolsSetConfig> _configsList;
        [SerializeField] private List<CurrencyConfig> _winRewardsList;
        [SerializeField] private List<CurrencyConfig> _loseFineList;

        public IReadOnlyList<SymbolsSetConfig> ConfigsList => _configsList;
        public IReadOnlyList<CurrencyConfig> WinRewardsList => _winRewardsList;
        public IReadOnlyList<CurrencyConfig> LoseFineList => _loseFineList;

        public SymbolsSetConfig GetConfigBy(GameMods gameMod)
        {
            SymbolsSetConfig config = _configsList.Where((config) => config.GameMode == gameMod).FirstOrDefault();

            if (config == null)
                throw new ArgumentException("Данный режим игры не поддерживается", nameof(gameMod));

            return config;
        }
    }
}
