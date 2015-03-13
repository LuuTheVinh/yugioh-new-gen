using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yugioh_AtemReturns.Database;
using Yugioh_AtemReturns.Cards.Monsters;
using Yugioh_AtemReturns.Cards.Traps;
using Yugioh_AtemReturns.Cards.Spells;
namespace Yugioh_AtemReturns.Cards
{
    class CardProvider
    {
        private static CardProvider instance;
        private List<CardData> listCardSource;
        private CardSourceDataSet.DataSourceDataTable cardTable;


        private CardProvider()
        {

            cardTable = CardSource.GetInstance().DataSourceTable;

            listCardSource = new List<CardData>(cardTable.Count);
            listCardSource.AddRange((IEnumerable<CardData>)this.loadMonsterSource());
            listCardSource.AddRange((IEnumerable<CardData>)this.loadTrapSource());
            listCardSource.AddRange((IEnumerable<CardData>)this.loadSpellSource());
        }

        public CardData GetCardById(string _id)
        {

            foreach (var item in listCardSource)
            {
                if (item.Id == this.getFormatIdString(_id.ToString()).PadRight(10))
                    return item;
            }
            throw new Exception("Can not match any card data with Id: \'" + _id + "\'");
        }

        public static CardProvider GetInstance()
        {
            if (instance == null)
                instance = new CardProvider();
            return instance;
        }

        private List<CardData> loadMonsterSource()
        {
            CardSourceDataSet.MonsterDataTable _monsterTable = CardSource.GetInstance().MonsterTable;
            List<CardData> _listmonsterCard = new List<CardData>(_monsterTable.Count);
            Yugioh_AtemReturns.Database.CardSourceDataSet.DataSourceRow temp;
            for (int i = 0; i < _monsterTable.Count; i++)
            {
                temp = cardTable.FindById(_monsterTable[i].Id);
                _listmonsterCard.Add(new MonsterCardData(
                    _monsterTable[i].Id,
                    temp.Name,
                    temp.Description,
                    _monsterTable[i].IsEffect,
                    _monsterTable[i].Attribute,
                    _monsterTable[i].MonsterType,
                    _monsterTable[i].Ability,
                    _monsterTable[i].ATK,
                    _monsterTable[i].DEF,
                    _monsterTable[i].Level,
                    _monsterTable[i].Rank,
                    _monsterTable[i].PendulumScale));
            }
            return _listmonsterCard;
        }
        private List<CardData> loadTrapSource()
        {
            CardSourceDataSet.TrapDataTable _trapTable = CardSource.GetInstance().TrapTable;
            List<CardData> _listtrapCard = new List<CardData>(_trapTable.Count);
            Yugioh_AtemReturns.Database.CardSourceDataSet.DataSourceRow temp;

            for (int i = 0; i < _trapTable.Count; i++)
            {
                temp = cardTable.FindById(_trapTable[i].Id);
                _listtrapCard.Add(new TrapCardData(
                    temp.Id,
                    temp.Name,
                    temp.Description,
                    _trapTable[i].TrapType));
            }
            return _listtrapCard;
        }
        private List<CardData> loadSpellSource()
        {
            CardSourceDataSet.SpellDataTable _spellTable = CardSource.GetInstance().SpellTable;
            List<CardData> _listSpellCard = new List<CardData>(_spellTable.Count);
            Yugioh_AtemReturns.Database.CardSourceDataSet.DataSourceRow temp;

            for (int i = 0; i < _spellTable.Count; i++)
            {
                temp = cardTable.FindById(_spellTable[i].Id);
                _listSpellCard.Add(new SpellCardData(
                    temp.Id,
                    temp.Name,
                    temp.Description,
                    _spellTable[i].SpellType));
            }
            return _listSpellCard;
        }
        private string getFormatIdString(string _id)
        {
            if (_id.ToString().StartsWith("C"))
            {
                return (_id.ToString().Substring(1));
            }
            return _id.ToString();
        }
    }
}
