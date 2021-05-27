using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA6Game.Helpers;

namespace GTA6Game.Pages.HaircutMinigame
{
    public class GameEndPayload : PropertyChangeNotifier, IDisposable
    {
        public int EarnedMoney { get; }

        public int FailPercent { get; }

        public string Message { get; }

        public bool IsFailed { get; }

        public string Hairstyle { get; }

        public GameEndPayload(int earnedMoney, int failPercent, string message, bool isFailed, string hairstyle)
        {
            EarnedMoney = earnedMoney;
            FailPercent = failPercent;
            Message = message;
            IsFailed = isFailed;
            Hairstyle = hairstyle;
        }

        public void Dispose()
        {
            DisposePropertyChangedEvent();
        }
    }
}
