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
        C1002,
        C1003,
        C3005,
        C83764719,
        CBackSide,

        BBackSide
        pha_b_ba, pha_b_dr, pha_b_en, pha_b_m1, pha_b_m2,pha_b_st,
        pha_r_ba, pha_r_dr, pha_r_en, pha_r_m1, pha_r_m2, pha_r_st,
        pha_s_ba, pha_s_dr, pha_s_en, pha_s_m1, pha_s_m2, pha_s_st,
        detail
    }

    public enum ID
    {
        CARD
    }

    public enum STATUS
    {
        NORMAL,
        DESTROY,
        MOUSEON,
        DEF,
        ATK,      
    }
    #endregion

    internal sealed partial class GlobalSetting
    {

    }
}
