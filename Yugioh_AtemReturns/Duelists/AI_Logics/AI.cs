using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yugioh_AtemReturns.Cards;
using Yugioh_AtemReturns;
namespace Yugioh_AtemReturns.Duelists.AI_Logics
{
    interface AI
    {
        Card Summon(Player _player, Computer _computer);

        Card SetTrap(Player _player, Computer _computer);
        Card ActiveTrap(Player _player, Computer _computer);
        Card ActiveSpell(Player _player, Computer _computer);
        ePhase SelectPhase(Player _player, Computer _computer);
        //Battle....

        Card Tribute(Computer computer);

        IEnumerable<Card> Tribute(Computer computer, int tri);

        void SelectATK(BattlePhase _battlephase,Player _player, Computer _computer);


    }
}
