using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yugioh_AtemReturns.Cards;
using Yugioh_AtemReturns.Database.CardSourceDataSetTableAdapters;
using Microsoft.Xna.Framework.Content;
using Yugioh_AtemReturns.Cards.Monsters;
using Yugioh_AtemReturns.Cards.Traps;
using Yugioh_AtemReturns.Cards.Spells;

namespace Yugioh_AtemReturns.Database
{
    class LoadDeck
    {
        private static LoadDeck instance;
        private LinkedList<Card> _listcard;

        private CardSourceDataSet dataset;
        private TableAdapterManager adapterManager;
        private DeckInfoTableAdapter deckInfoTableAdapter;
        private DataSourceTableAdapter dataSourceTableAdapter;

        private Yugioh_AtemReturns.Database.CardSourceDataSet.DeckInfoDataTable deckInfo;

        private LoadDeck()
        {
            _listcard = new LinkedList<Card>();
            this.dataset = new CardSourceDataSet();
            this.adapterManager = new TableAdapterManager();
            this.deckInfoTableAdapter = new DeckInfoTableAdapter();
            this.dataSourceTableAdapter = new DataSourceTableAdapter();

            this.adapterManager.DeckInfoTableAdapter = this.deckInfoTableAdapter;
            this.adapterManager.DataSourceTableAdapter = this.dataSourceTableAdapter;

            this.deckInfoTableAdapter.Fill(dataset.DeckInfo);
            this.dataSourceTableAdapter.Fill(dataset.DataSource);
        }
        public static LoadDeck GetInstance()
        {
            if (instance == null)
                instance = new LoadDeck();
            return instance;
        }

        public LinkedList<Card> GetDeck(ContentManager _content, string _deckId)
        {
            
            this.deckInfo = this.deckInfoTableAdapter.FindByDeckId(_deckId);
            Yugioh_AtemReturns.Database.CardSourceDataSet.DataSourceRow temp;

            for (int i = 0; i < deckInfo.Rows.Count; i++)
            {
                temp = dataset.DataSource.FindById(Convert.ToString(deckInfo[i].CardId));
                for (int j = 0; j < Convert.ToInt32(deckInfo[i].Count); j++)
                {
                    switch (temp.CardType)
                    {
                        case "MON":
                            this._listcard.AddLast(new Monster(_content,Convert.ToString(deckInfo[i].CardId)));
                            break;
                        case "TRA":
                            this._listcard.AddLast(new Trap(_content, Convert.ToString(deckInfo[i].CardId)));
                            break;
                        case "SPE":
                            this._listcard.AddLast(new Spell(_content, Convert.ToString(deckInfo[i].CardId)));
                            break;
                        case "XYZ":
                            this._listcard.AddLast(new XYZMonster(_content, Convert.ToString(deckInfo[i].CardId)));
                            break;
                        case "SYN":
                            this._listcard.AddLast(new SynchroMonster(_content, Convert.ToString(deckInfo[i].CardId)));
                            break;
                        case "FUS":
                            this._listcard.AddLast(new FusionMonster(_content, Convert.ToString(deckInfo[i].CardId)));
                            break;
                        default:
                            break;
                    } // Switch
                } // For j
            }// For i
            return _listcard;
        }
    }
}
