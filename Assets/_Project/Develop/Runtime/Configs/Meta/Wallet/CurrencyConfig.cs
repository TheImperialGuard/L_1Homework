using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Meta.Wallet
{

    [Serializable]
    public class CurrencyConfig
    {
        [field: SerializeField] public CurrencyTypes Type { get; private set; }
        [field: SerializeField] public int Value { get; private set; }
    }

}
