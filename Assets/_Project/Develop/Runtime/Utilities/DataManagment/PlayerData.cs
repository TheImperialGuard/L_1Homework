﻿using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagment
{
    public class PlayerData : ISaveData
    {
        public Dictionary<CurrencyTypes, int> WalletData;

        public int RoundsPlayedNumber;
        public int RoundsWonNumber;
        public int RoundsLostNumber;
    }
}
