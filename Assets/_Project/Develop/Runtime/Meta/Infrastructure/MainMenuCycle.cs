using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuCycle
    {
        private DIContainer _container;
        private SceneSwitcherService _sceneSwitcherService;
        private ICoroutinesPerformer _coroutinesPerformer;
        private ConfigsProviderService _configsProviderService;

        public MainMenuCycle(DIContainer container)
        {
            _container = container;

            _sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
            _coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
            _configsProviderService = _container.Resolve<ConfigsProviderService>();
        }

        public void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SwitchToGameplayWith(GameMods.Numbers);

            if (Input.GetKeyDown(KeyCode.Alpha2))
                SwitchToGameplayWith(GameMods.Letters);
        }

        private void SwitchToGameplayWith(GameMods gameMode)
        {
            SymbolsSetsListConfig configsList = _configsProviderService.GetConfig<SymbolsSetsListConfig>();
            SymbolsSetConfig config = configsList.GetConfigBy(gameMode);

            _coroutinesPerformer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(Scenes.Gameplay, new GameplayInputArgs(config)));
        }
    }
}
