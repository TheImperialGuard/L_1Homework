using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.PurchaseService;
using Assets._Project.Develop.Runtime.Meta.Features.RoundsScore;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateResetScorePurchaseService);
        }

        public static ResetScorePurchaseService CreateResetScorePurchaseService(DIContainer c)
        {
            return new ResetScorePurchaseService(
                c.Resolve<ConfigsProviderService>(),
                c.Resolve<WalletService>(),
                c.Resolve<RoundsScoreService>()
            );
        }
    }
}
