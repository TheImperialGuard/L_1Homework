using Assets._Project.Develop.Runtime.Configs.Meta.Wallet;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Meta.RoundsScore
{
    [CreateAssetMenu(menuName = "Configs/Meta/Score/RoundsScoreResetConfig", fileName = "RoundsScoreResetConfig")]
    public class RoundsScoreResetConfig : ScriptableObject
    {
        [field: SerializeField] public CurrencyConfig ResetCost { get; private set; }
    }
}
