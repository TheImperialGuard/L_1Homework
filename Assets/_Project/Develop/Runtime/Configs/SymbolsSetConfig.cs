using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/SymbolsSetConfig", fileName = "SymbolsSetConfig")]
    public class SymbolsSetConfig : ScriptableObject
    {
        [SerializeField] private List<KeyCode> _symbols = new()
        {
            KeyCode.A,
            KeyCode.B, 
            KeyCode.C, 
            KeyCode.D, 
            KeyCode.E, 
            KeyCode.F, 
            KeyCode.G,
        };

        [field: SerializeField] public GameMods GameMode;

        public IReadOnlyList<KeyCode> Symbols => _symbols;
    }
}