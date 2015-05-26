using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace GetCardData
{
    public partial class Form1 : Form
    {
        private Dictionary<string, string> _cardDataTable = new Dictionary<string, string>();
        private string _cardFolderPath;
        private bool _isEditing;
        public Form1()
        {
            InitializeComponent();

            dataSourceTableAdapter.Fill(cdbDataSet.DataSource);
            monsterTableAdapter.Fill(cdbDataSet.Monster);
            spellTableAdapter.Fill(cdbDataSet.Spell);
            trapTableAdapter.Fill(cdbDataSet.Trap);

            _cardFolderPath = "E:\\Vinh\\HOC TAP\\UIT\\Lap Trinh Truc Quan\\Do An\\Yugioh_AtemReturnsContent\\card";

            LoadCardListItem(_cardFolderPath);
            _isEditing = false;
        }

        private void LoadCardListItem(string path)
        {
            //cardList.Items.Clear();

            //foreach (var file in Directory.GetFiles(path, "*.jpg"))
            //{
            //    var str = file.Remove(0, path.Length + 1).Replace(".jpg", "");
            //    cardListBox.Items.Add(str);
            //}

            //foreach (var file in Directory.GetFiles(path, "*.png"))
            //{
            //    var str = file.Remove(0, path.Length + 1).Replace(".png", "");
            //    cardListBox.Items.Add(str);
            //}

            //foreach (var file in Directory.GetFiles(path, "*.bmp"))
            //{
            //    var str = file.Remove(0, path.Length + 1).Replace(".bmp", "");
            //    cardListBox.Items.Add(str);
            //}

            cardList.Rows.Clear();
            foreach (var file in Directory.GetFiles(path, "*.jpg"))
            {
                var str = file.Remove(0, path.Length + 1).Replace(".jpg", "");
                var row = cdbDataSet.DataSource.FindById(str);
                cardList.Rows.Add(str, row != null);
            }

            foreach (var file in Directory.GetFiles(path, "*.png"))
            {
                var str = file.Remove(0, path.Length + 1).Replace(".png", "");
                var row = cdbDataSet.DataSource.FindById(str);
                cardList.Rows.Add(str, row != null);
            }

            foreach (var file in Directory.GetFiles(path, "*.bmp"))
            {
                var str = file.Remove(0, path.Length + 1).Replace(".bmp", "");
                var row = cdbDataSet.DataSource.FindById(str);
                cardList.Rows.Add(str, row != null);
            }
        }

        private void GetCardData(string cardId)
        {
            _cardDataTable.Clear();

            var row = cdbDataSet.DataSource.FindById(cardId);
            if (row != null)
            {
                var mess = MessageBox.Show("You already had this card. Get this card again?", "Hey", MessageBoxButtons.YesNo);
                if (mess == DialogResult.No)
                {
                    return;
                }
                
            }

            HtmlAgilityPack.HtmlDocument htmlDocument = null;
            try
            {
                var htmlWeb = new HtmlWeb();

                string cardlink = "http://yugioh.wikia.com/wiki/" + cardId;

                //TEST VALUE
                //var htmlDocument = htmlWeb.Load("http://yugioh.wikia.com/wiki/87796900");         //Monster
                //var htmlDocument = htmlWeb.Load("http://yugioh.wikia.com/wiki/Kuriboh");          //Monster with Effect
                //var htmlDocument = htmlWeb.Load("http://yugioh.wikia.com/wiki/Hino-Kagu-Tsuchi");
                //htmlDocument = htmlWeb.Load("http://yugioh.wikia.com/wiki/Sun_Dragon_Inti");
                //var htmlDocument = htmlWeb.Load("http://yugioh.wikia.com/wiki/44095762");         //Spell
                //var htmlDocument = htmlWeb.Load("http://yugioh.wikia.com/wiki/83764718");         //Trap

                htmlDocument = htmlWeb.Load(cardlink);

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            if (htmlDocument == null) return;

            var content = htmlDocument.GetElementbyId("mw-content-text");

            if (content != null)
            {
                //Check page not found
                if(content.SelectSingleNode("//div[@class='noarticletext']") != null)
                {
                    //MessageBox.Show(cardList.SelectedRows[0].Cells[0].Value + " not found!");
                    MessageBox.Show(cardId + " not found!");
                    return;
                }

                //ID
                var idnode = content.SelectSingleNode("//table[@class='cardtable']//tr[th='Card Number']");
                if (idnode != null)
                    _cardDataTable["Id"] = idnode.SelectSingleNode("td").InnerText.Replace("\n", "").Replace(" ", "");

                //NAME
                var namenode = content.SelectSingleNode("//table[@class='cardtable']//tr[th='English']");
                if (namenode != null)
                    _cardDataTable["Name"] = namenode.SelectSingleNode("td").InnerText.Replace("\n", "");


                //DESCRIPTION
                var descnode = content.SelectSingleNode("//table[@class='cardtable']//tr/td[b='Card descriptions']/table/tr/td/table/tr/td[@class='navbox-list']");
                if (descnode != null)
                    _cardDataTable["Description"] = descnode.InnerText.Replace("\n", "");

                //TYPE
                var typenode = content.SelectSingleNode("//table[@class='cardtable']//tr[th='Type']");
                if (typenode == null)
                    typenode = content.SelectSingleNode("//table[@class='cardtable']//tr[th='Types']");

                if (typenode != null)
                    _cardDataTable["CardType"] = typenode.SelectSingleNode("td").InnerText.Replace("\n", "").Replace(" ", "");

                switch (_cardDataTable["CardType"].ToString())
                {
                    case "SpellCard":
                    {
                        _cardDataTable["CardType"] = "SPE";

                        //Property (SpellType)
                        var spellnode = content.SelectSingleNode("//table[@class='cardtable']//tr[th='Property']");
                        if (spellnode != null)
                            _cardDataTable["SpellType"] = spellnode.SelectSingleNode("td").InnerText.Replace("\n", "").Replace(" ", "");

                        break;
                    }
                    case "TrapCard":
                    {
                        _cardDataTable["CardType"] = "TRA";

                        //Property (TrapType)
                        var trapnode = content.SelectSingleNode("//table[@class='cardtable']//tr[th='Property']");
                        if (trapnode != null)
                            _cardDataTable["TrapType"] = trapnode.SelectSingleNode("td").InnerText.Replace("\n", "").Replace(" ", "");

                        break;
                    }
                    //    case "XYZ":
                    //    {
                    //        //Attribute

                    //        //Rank

                    //        //ATK/DEF

                    //        //Materials

                    //        break;
                    //    }
                    default:
                    {
                        var typeTemp = _cardDataTable["CardType"];
                        _cardDataTable["CardType"] = "MON";
                        _cardDataTable["IsEffect"] = "0";
                        _cardDataTable["Ability"] = "NORMAL";

                        //Nếu có 2 loại trong Types (cách bởi dấu "/")
                        if (typeTemp.IndexOf("/", StringComparison.Ordinal) != -1)
                        {
                            //Tách nó ra
                            var arrType = typeTemp.Split('/');
                                    
                            //Cái đầu tiên luôn là MonsterType
                            _cardDataTable["MonsterType"] = arrType[0];

                            //Nếu có 3 loại thì cái thứ 2 là Ability
                            if (arrType.Length > 2)
                            {
                                _cardDataTable["Ability"] = arrType[1];
                            }

                            //Các cuối luôn là Effect
                            if (arrType[arrType.Length - 1] == "Effect")
                            {
                                _cardDataTable["IsEffect"] = "1";
                            }
                            else  //Nếu ko phải thì nó là Ability
                            {
                                _cardDataTable["IsEffect"] = "0";
                                _cardDataTable["Ability"] = arrType[1];
                            }
                        }
                        else  //Một cái luôn là Monstertype
                        {
                            _cardDataTable["MonsterType"] = typeTemp;
                        }

                        //LEVEL
                        var levelnode = content.SelectSingleNode("//table[@class='cardtable']//tr[th='Level']");
                        if (levelnode != null)
                            _cardDataTable["Level"] = levelnode.SelectSingleNode("td").InnerText.Replace("\n", "").Replace(" ", "");

                        //ATK/DEF
                        var atknode = content.SelectSingleNode("//table[@class='cardtable']//tr[th='ATK/DEF']");
                        if (atknode != null)
                        {
                            var str = atknode.SelectSingleNode("td").InnerText.Replace("\n", "").Replace(" ", "");
                            _cardDataTable["ATK"] = String.Copy(str).Substring(0, str.IndexOf("/"));
                            _cardDataTable["DEF"] = String.Copy(str).Substring(str.IndexOf("/") + 1, str.Length - _cardDataTable["ATK"].Length - 1);
                        }

                        //Attribute
                        var attnode = content.SelectSingleNode("//table[@class='cardtable']//tr[th='Attribute']");
                        if (attnode != null)
                            _cardDataTable["Attribute"] = attnode.SelectSingleNode("td").InnerText.Replace("\n", "").Replace(" ", "");

                        _cardDataTable["Rank"] = null;
                        _cardDataTable["PendulumScale"] = null;

                        break;
                    }
                }
            }

            if (_cardDataTable.Count == 0) return;

            SaveCardData();
        }

        private void SaveCardData()
        {
            if (_cardDataTable.Count == 0) return;

            var rowedit = cdbDataSet.DataSource.FindById(_cardDataTable["Id"]);
            if (rowedit != null)
            {
                if (MessageBox.Show("Override?", "Hey", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SaveEditCard(_cardDataTable["Id"]);
                }
                return;
            }

            var newcard = cdbDataSet.DataSource.NewDataSourceRow();
            newcard.Id = _cardDataTable["Id"];
            newcard.Name = _cardDataTable["Name"];
            newcard.Description = _cardDataTable["Description"];
            newcard.CardType = _cardDataTable["CardType"].Substring(0, 3).ToUpper();

            cdbDataSet.DataSource.Rows.Add(newcard);
            cdbDataSet.AcceptChanges();

            switch (_cardDataTable["CardType"])
            {
                case "MON":
                {
                    var newmonster = cdbDataSet.Monster.NewMonsterRow();
                    newmonster.Id = _cardDataTable["Id"];
                    newmonster.IsEffect = _cardDataTable["IsEffect"] != "0";
                    newmonster.MonsterType = _cardDataTable["MonsterType"].ToUpper();
                    newmonster.Ability = _cardDataTable["Ability"].ToUpper();
                    newmonster.ATK = int.Parse(_cardDataTable["ATK"]);
                    newmonster.DEF = int.Parse(_cardDataTable["DEF"]);
                    newmonster.Level = byte.Parse(_cardDataTable["Level"]);
                    newmonster.Attribute = _cardDataTable["Attribute"].Substring(0, 3).ToUpper();

                    cdbDataSet.Monster.Rows.Add(newmonster);
                    cdbDataSet.AcceptChanges();

                    break;
                }
                case "TRA":
                {
                    var newtrap = cdbDataSet.Trap.NewTrapRow();
                    newtrap.Id = _cardDataTable["Id"];
                    newtrap.TrapType = _cardDataTable["TrapType"].ToUpper();

                    cdbDataSet.Trap.Rows.Add(newtrap);
                    cdbDataSet.AcceptChanges();

                    break;
                }

                case "SPE":
                {
                    var newspell = cdbDataSet.Spell.NewSpellRow();
                    newspell.Id = _cardDataTable["Id"];
                    newspell.SpellType = _cardDataTable["SpellType"].ToUpper();

                    cdbDataSet.Spell.Rows.Add(newspell);
                    cdbDataSet.AcceptChanges();
                    break;
                }
            }

            foreach (DataGridViewRow row in cardList.Rows)
            {
                if (row.Cells[0].Value.ToString() == newcard.Id)
                {
                    row.Cells[1].Value = true;
                    break;
                }
            }

            //FillCardInfo(cardDataTable["Id"]);
        }

        private void cardListBox_SelectedIndexChanged()
        {
            try
            {
                //Clear
                ClearAllText();
                DisableAllControl();

                //Fill
                FillCardInfo(cardList.SelectedRows[0].Cells[0].Value.ToString());
            }
            catch (Exception exception)
            {
                //MessageBox.Show("Can't find " + exception.Message);
            }
        }

        private void ClearAllText()
        {
            foreach (var control in cardInfoGroup.Controls)
            {
                if (control is TextBox)
                {
                    var textbox = control as TextBox;
                    textbox.Text = "";
                }
                else if (control is RichTextBox)
                {
                    var rich = control as RichTextBox;
                    rich.Text = "";
                }
                else if (control is CheckBox)
                {
                    var check = control as CheckBox;
                    check.Checked = false;
                }
            }

            richTextBox.Text = "";
        }

        private void DisableAllControl()
        {
            foreach (var control in cardInfoGroup.Controls)
            {
                if (control is TextBox)
                {
                    var textbox = control as TextBox;
                    textbox.ReadOnly = true;
                }
                else if (control is RichTextBox)
                {
                    var rich = control as RichTextBox;
                    rich.ReadOnly = true;
                }
                else if (control is CheckBox)
                {
                    var check = control as CheckBox;
                    check.Enabled = false;
                }
            }
        }

        private void FillCardInfo(string cardId)
        {

            try
            {
                cardPicture.Image = Image.FromFile(_cardFolderPath + "\\" + cardId + ".jpg");
            }
            catch (Exception)
            {
                try
                {
                    cardPicture.Image = Image.FromFile(_cardFolderPath + "\\" + cardId + ".bmp");
                }
                catch (Exception)
                {
                    cardPicture.Image = Image.FromFile(_cardFolderPath + "\\" + cardId + ".png");
                }
            }

            idText.Text = cardId;

            var card = cdbDataSet.DataSource.FindById(cardId);
            if (card == null) return;

            nameText.Text = card.Name;
            idText.Text = card.Id;
            descriptionText.Text = card.Description;
            typeText.Text = card.CardType;

            switch (card.CardType)
            {
                case "MON":
                    {
                        var monster = cdbDataSet.Monster.FindById(card.Id);
                        cardTypeText.Text = monster.MonsterType;
                        abilityText.Text = monster.Ability;
                        attackText.Text = monster.ATK.ToString();
                        defenceText.Text = monster.DEF.ToString();
                        isEffectChk.Checked = monster.IsEffect;
                        levelText.Text = monster.Level.ToString();
                        attibuteText.Text = monster.Attribute;

                        //rankText.Text = monster.Rank.ToString();
                        //pendulumText.Text = monster.PendulumScale.ToString();

                        break;
                    }
                case "TRA":
                    {
                        foreach (var trap in cdbDataSet.Trap)
                        {
                            if (trap.Id == card.Id)
                            {
                                cardTypeText.Text = trap.TrapType;
                            }
                        }
                        break;
                    }
                case "SPE":
                    {
                        foreach (var spell in cdbDataSet.Spell)
                        {
                            if (spell.Id == card.Id)
                            {
                                cardTypeText.Text = spell.SpellType;
                            }
                        }
                        break;
                    }
            }
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            if (_isEditing == false)
            {
                foreach (var control in cardInfoGroup.Controls)
                {
                    if (control is TextBox)
                    {
                        var textbox = control as TextBox;
                        textbox.ReadOnly = false;
                    }
                    else if (control is RichTextBox)
                    {
                        var rich = control as RichTextBox;
                        rich.ReadOnly = false;
                    }
                    else if (control is CheckBox)
                    {
                        var check = control as CheckBox;
                        check.Enabled = true;
                    }
                }
                _isEditing = true;
            }
            else
            {
                _isEditing = false;
                DisableAllControl();
            }
        }

        private void cardBrowserBtn_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowDialog();
            _cardFolderPath = folderBrowserDialog.SelectedPath;
            cardFolderPathText.Text = _cardFolderPath;

            LoadCardListItem(_cardFolderPath);
        }

        private void cardList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cardListBox_SelectedIndexChanged();
        }

        private void getAllBtn_Click(object sender, EventArgs e)
        {
            Thread newThread = new Thread(GetAllCard);
            newThread.Start();
        }

        private void GetAllCard()
        {
            //saveBtn.Enabled = false;
            //editBtn.Enabled = false;
            //cardBrowserBtn.Enabled = false;
            //cardFolderPathText.Enabled = false;

            foreach (DataGridViewRow row in cardList.Rows)
            {
                var datarow = cdbDataSet.DataSource.FindById(row.Cells[0].Value.ToString());
                if (datarow != null) continue;

                GetCardData(row.Cells[0].Value.ToString());
            }

            MessageBox.Show("Finished!");
            //saveBtn.Enabled = true;
            //editBtn.Enabled = true;
            //cardBrowserBtn.Enabled = true;
            //cardFolderPathText.Enabled = true;
        }

        private void getCardBtn_Click(object sender, EventArgs e)
        {
            GetCardData(cardList.SelectedRows[0].Cells[0].Value.ToString());
            
            richTextBox.Text = "";
            foreach (var item in _cardDataTable)
            {
                richTextBox.Text += item.Key + ": " + item.Value + "\n";
            }

            FillCardInfo(cardList.SelectedRows[0].Cells[0].Value.ToString());
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (_isEditing == true)
            {
                _cardDataTable["Name"] = nameText.Text;
                _cardDataTable["Id"] = idText.Text;
                _cardDataTable["Description"] = descriptionText.Text;
                _cardDataTable["CardType"] = typeText.Text;

                switch (_cardDataTable["CardType"])
                {
                    case "MON":
                    {
                        _cardDataTable["IsEffect"] = isEffectChk.Checked == true ? "1" : "0";
                        _cardDataTable["MonsterType"] = cardTypeText.Text;
                        _cardDataTable["Ability"] = abilityText.Text;
                        _cardDataTable["ATK"] = attackText.Text;
                        _cardDataTable["DEF"] = defenceText.Text;
                        _cardDataTable["Level"] = levelText.Text;
                        _cardDataTable["Attribute"] = attibuteText.Text;
                        break;
                    }
                    case "TRA":
                    {
                        _cardDataTable["TrapType"] = cardTypeText.Text;
                        break;
                    }

                    case "SPE":
                    {
                        _cardDataTable["TrapType"] = cardTypeText.Text;
                        break;
                    }
                }

                DisableAllControl();
                
                SaveEditCard(_cardDataTable["Id"]);
                _isEditing = false;
            }
        }

        private void SaveEditCard(string cardId)
        {
            var cardRow = cdbDataSet.DataSource.FindById(cardId);
            cardRow.BeginEdit();

            cardRow.Name = _cardDataTable["Name"];
            cardRow.Id = _cardDataTable["Id"];
            cardRow.Description = _cardDataTable["Description"];
            cardRow.CardType = _cardDataTable["CardType"].Substring(0, 3).ToUpper();

            cardRow.EndEdit();
            cdbDataSet.DataSource.AcceptChanges();

            switch (_cardDataTable["CardType"])
            {
                case "MON":
                {
                    var monsterCard = cdbDataSet.Monster.FindById(cardRow.Id);
                    monsterCard.BeginEdit();
                    monsterCard.IsEffect =  _cardDataTable["IsEffect"] == "1";
                    monsterCard.MonsterType = _cardDataTable["MonsterType"].ToUpper();
                    monsterCard.Ability = _cardDataTable["Ability"].ToUpper();
                    monsterCard.ATK = int.Parse(_cardDataTable["ATK"]);
                    monsterCard.DEF = int.Parse(_cardDataTable["DEF"]);
                    monsterCard.Level = byte.Parse(_cardDataTable["Level"]);
                    monsterCard.Attribute = _cardDataTable["Attribute"].Substring(0, 3).ToUpper();
                    monsterCard.EndEdit();
                    cdbDataSet.Monster.AcceptChanges();
                    break;
                }
                case "TRA":
                    {
                        foreach (var trap in cdbDataSet.Trap)
                        {
                            if (trap.Id == _cardDataTable["Id"])
                            {
                                trap.BeginEdit();

                                trap.Id = _cardDataTable["Id"];
                                trap.TrapType = _cardDataTable["TrapType"].ToUpper();

                                trap.EndEdit();
                                cdbDataSet.Trap.AcceptChanges();
                            }
                        }
                        
                        break;
                    }

                case "SPE":
                    {
                        foreach (var spell in cdbDataSet.Spell)
                        {
                            if (spell.Id == _cardDataTable["Id"])
                            {
                                spell.BeginEdit();

                                spell.Id = _cardDataTable["Id"];
                                spell.SpellType = _cardDataTable["SpellType"].ToUpper();

                                spell.EndEdit();
                                cdbDataSet.Spell.AcceptChanges();
                            }
                        }
                        break;
                    }
            }

        }
    }
}
