using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yugioh_AtemReturns.Database.CardSourceDataSetTableAdapters;
using System.Data;

namespace Yugioh_AtemReturns.Database
{
    class CardSource
    {
        private static CardSource instance;

        private CardSourceDataSet cardSource;
        private TableAdapterManager adapterManager;
        private DataSourceTableAdapter dataSourceAdapter;
        private MonsterTableAdapter monsterSourceAdapter;
        private TrapTableAdapter trapSourceAdapter;
        private SpellTableAdapter spellSourceAdapter;

        public CardSourceDataSet.DataSourceDataTable DataSourceTable
        {
            get { return cardSource.DataSource; }
        }
        public CardSourceDataSet.MonsterDataTable MonsterTable
        {
            get {
                return cardSource.Monster;
            }
        }
        public CardSourceDataSet.TrapDataTable TrapTable
        {
            get
            {
                return cardSource.Trap;
            }
        }
        public CardSourceDataSet.SpellDataTable SpellTable
        {
            get { return cardSource.Spell; }
        }

        private CardSource()
        {
            cardSource = new CardSourceDataSet();
            adapterManager = new TableAdapterManager();
            dataSourceAdapter = new DataSourceTableAdapter();
            monsterSourceAdapter = new MonsterTableAdapter();
            trapSourceAdapter = new TrapTableAdapter();
            spellSourceAdapter = new SpellTableAdapter();

            adapterManager.DataSourceTableAdapter = this.dataSourceAdapter;
            adapterManager.MonsterTableAdapter = this.monsterSourceAdapter;

            dataSourceAdapter.Fill(cardSource.DataSource);
            monsterSourceAdapter.Fill(cardSource.Monster);
            trapSourceAdapter.Fill(cardSource.Trap);
            spellSourceAdapter.Fill(cardSource.Spell);
        }

        public static CardSource GetInstance()
        {
            if (instance == null)
                instance = new CardSource();
            return instance;
        }

    }
}
