using Assets._Project.Develop.Runtime.Configs.Meta.RoundsScore;
using Assets._Project.Develop.Runtime.Configs.Meta.Wallet;
using Assets._Project.Develop.Runtime.Meta.Features.RoundsScore;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;

namespace Assets._Project.Develop.Runtime.Meta.Features.PurchaseService
{
    public class ResetScorePurchaseService
    {
        private ConfigsProviderService _configsProviderService;
        private WalletService _walletService;
        private RoundsScoreService _roundsScoreService;

        public ResetScorePurchaseService(
            ConfigsProviderService configsProviderService, 
            WalletService walletService, 
            RoundsScoreService roundsScoreService)
        {
            _configsProviderService = configsProviderService;
            _walletService = walletService;
            _roundsScoreService = roundsScoreService;
        }

        public bool TryPurchase(out CurrencyConfig cost)
        {
            RoundsScoreResetConfig resetConfig = _configsProviderService.GetConfig<RoundsScoreResetConfig>();

            cost = resetConfig.ResetCost;

            if (_walletService.Enough(resetConfig.ResetCost.Type, resetConfig.ResetCost.Value))
            {
                _walletService.Spend(resetConfig.ResetCost.Type, resetConfig.ResetCost.Value);
                _roundsScoreService.ResetProgress();
                return true;
            }

            return false;
        }
    }
}
