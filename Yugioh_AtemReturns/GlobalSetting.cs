using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yugioh_AtemReturns
{
    #region Enum

    public enum SpriteID
    {
        pha_b_ba, pha_b_dr, pha_b_en, pha_b_m1, pha_b_m2, pha_b_st,
        pha_r_ba, pha_r_dr, pha_r_en, pha_r_m1, pha_r_m2, pha_r_st,
        pha_s_ba, pha_s_dr, pha_s_en, pha_s_m1, pha_s_m2, pha_s_st,

        eff_y_circle, atk_sword,
        lp_bar, lp_extract,
        font_68_whitenum, font_710_blacknum, font_710_bluenum,
        font_710_rednum, font_710_whitenum,font_710_yellownum,
        font_812_whitenum, font_1640_damage,font_2634_dorangenum,

        C1002,
        C1003,
        C3005,
        C83764719,//test

        //Card
        CBackSide,
        C11549357, C13039848, C25652259, C28279543, C38033122,
        C39256679, C40640057, C41392891, C46986415, C52077741,
        C57116034, C5818798, C64788463, C65240384, C70781052,
        C71413901, C72989439, C75347539, C78193831, C87796900,
        C90876561, C91152256, C95727991, C99785935,

        // Big Card
        B83764719,
        BBackSide,
        B11549357, B13039848, B25652259, B28279543, B38033122,
        B39256679, B40640057, B41392891, B46986415, B52077741,
        B57116034, B5818798, B64788463, B65240384, B70781052,
        B71413901, B72989439, B75347539, B78193831, B90876561,
        B91152256, B95727991, B99785935, B87796900,

    }

    public enum eSoundId
    {
        start,
        m_menu,
        m_duel1,
        battle_turn,
        attack,
        card_move,
        card_open,
        damage,
        life_up,
        turn_change
    }

    public enum ID
    {
        CARD,
        BATTLE_SWORD,
        HEALTH_BAR,
        LP_CHANGE,
        NUM
    }

    public enum STATUS
    {
        
        NORMAL,
        DESTROY,
        MOUSEON,
        //DEF,
        //ATK,  
        TRIBUTE,
        SWORD_FULL,
        SWORD_HALF
    }
    #endregion

    internal sealed partial class GlobalSetting
    {

    }
}
