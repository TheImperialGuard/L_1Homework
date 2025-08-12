using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gameplay/SymbolsSetConfig", fileName = "SymbolsSetConfig")]
public class SymbolsSetConfig : ScriptableObject
{
    [SerializeField] List<char> symbols;
}
