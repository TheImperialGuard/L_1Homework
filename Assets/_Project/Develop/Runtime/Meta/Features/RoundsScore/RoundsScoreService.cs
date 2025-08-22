using Assets._Project.Develop.Runtime.Configs.Meta.RoundsScore;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;

namespace Assets._Project.Develop.Runtime.Meta.Features.RoundsScore
{
    public class RoundsScoreService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        ConfigsProviderService _configsProviderService;

        public RoundsScoreService(PlayerDataProvider playerDataProvider, ConfigsProviderService configsProviderService)
        {
            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);

            _configsProviderService = configsProviderService;
        }

        public int RoundsPlayedNumber {  get; private set; }
        public int RoundsWonNumber { get; private set; }
        public int RoundsLostNumber { get; private set; }

        public void IncreaseWins()
        {
            IncreasePlayedRounds();
            RoundsWonNumber++;
        }

        public void IncreaseLosses()
        { 
            IncreasePlayedRounds();
            RoundsLostNumber++;
        }

        public void ResetProgress()
        {
            RoundsPlayedNumber = 0;
            RoundsWonNumber = 0;
            RoundsLostNumber = 0;
        }

        public void ReadFrom(PlayerData data)
        {
            RoundsPlayedNumber = data.RoundsPlayedNumber;
            RoundsWonNumber = data.RoundsWonNumber;
            RoundsLostNumber = data.RoundsLostNumber;
        }

        public void WriteTo(PlayerData data)
        {
            data.RoundsPlayedNumber = RoundsPlayedNumber;
            data.RoundsWonNumber = RoundsWonNumber;
            data.RoundsLostNumber = RoundsLostNumber;
        }

        private void IncreasePlayedRounds() => RoundsPlayedNumber++;
    }
}
