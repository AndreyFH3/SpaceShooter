
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        public bool isFirstSession = true;
        public string language = "ru";
        public bool feedbackDone;
        public bool promptDone;

        // Ваши сохранения
        public ulong money = 0;
        public int premiumMoney = 0;
        public int maxLevel = 1;
        public ulong maxScore;
        //====================
        public int shootingSpeed = 1;
        public int ShootingDamage = 1;

    }
}
