using Assets._Project.Develop.Runtime.Configs.Meta.Wallet;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.PurchaseService;
using Assets._Project.Develop.Runtime.Meta.Features.RoundsScore;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuNavigator
    {
        private DIContainer _container;
        private SceneSwitcherService _sceneSwitcherService;
        private ICoroutinesPerformer _coroutinesPerformer;
        private RoundsScoreService _roundsScore;
        private WalletService _wallet;
        private ResetScorePurchaseService _resetScorePurchaseService;

        public MainMenuNavigator(DIContainer container)
        {
            _container = container;

            _sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
            _coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
            _roundsScore = _container.Resolve<RoundsScoreService>();
            _wallet = _container.Resolve<WalletService>();
            _resetScorePurchaseService = _container.Resolve<ResetScorePurchaseService>();
        }

        public void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SwitchToGameplayWith(GameMods.Numbers);

            if (Input.GetKeyDown(KeyCode.Alpha2))
                SwitchToGameplayWith(GameMods.Letters);

            if (Input.GetKeyDown(KeyCode.S))
                Debug.Log(
                    $"Played rounds: {_roundsScore.RoundsPlayedNumber}, " +
                    $"Won Rounds: {_roundsScore.RoundsWonNumber}, " +
                    $"Lost Rounds: {_roundsScore.RoundsLostNumber}, " +
                    $"Gold Balance: {_wallet.GetCurrency(CurrencyTypes.Gold).Value}"
                );

            if (Input.GetKeyDown(KeyCode.R))
                PurchaseResetProgress();
        }

        private void PurchaseResetProgress()
        {
            if (_resetScorePurchaseService.TryPurchase(out CurrencyConfig cost))
            {
                Debug.Log($"Прогресс успешно сброшен за {cost.Value} {cost.Type}");

                PlayerDataProvider dataProvider = _container.Resolve<PlayerDataProvider>();

                _coroutinesPerformer.StartPerform(dataProvider.Save());
            }
            else
            {
                Debug.Log($"Вам не хватает {cost.Type}");
            }
        }

        private void SwitchToGameplayWith(GameMods gameMode)
        {
            _coroutinesPerformer
                .StartPerform(_sceneSwitcherService
                .ProcessSwitchTo(Scenes.Gameplay, new GameplayInputArgs(gameMode)));
        }
    }
}
