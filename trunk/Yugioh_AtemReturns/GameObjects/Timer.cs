using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Yugioh_AtemReturns.GameObjects
{
    public class Timer
    {
        public double TotalGameTime { get; private set; }
        public double ElapseGameTime { get; private set; }

        private float elapsetimeslice;
        private float stopWatch;
        public Timer()
        {
            /*
            TotalGameTime = 0.0f;
            ElapseGameTime = 0.0f;
            elapsetimeslice = 0.0f;
            stopWatch = 0.0f;
            */ //Giá trị mặc định
        }

        //Muốn sử dụng được thì phải gọi hàm này
        public void Update(GameTime _gametime)
        {
            this.ElapseGameTime = _gametime.ElapsedGameTime.TotalMilliseconds;
            this.TotalGameTime = _gametime.TotalGameTime.TotalMilliseconds;
        }

        //Thực hiện một việc nào đó mỗi _milisecond (lặp đi lặp lại)
        public bool TimeSlice(float _milisecond)
        {
            elapsetimeslice += (float)this.ElapseGameTime;
            if (elapsetimeslice >= _milisecond)
            {
                elapsetimeslice -= _milisecond;
                return true;
            }
            return false;
        }

        // thực hiện một việc nào đó một lần duy nhất sau _milisecond. Sử dụng lại thì phải gọi resetstopwatch
        public bool StopWatch(float _milisecond)
        {
            if (stopWatch == -1.0f)
                return false;
            stopWatch += (float)this.ElapseGameTime;
            if (stopWatch >= _milisecond)
            {
                stopWatch = -1.0f;
                return true;
            }
            return false;
        }

        public void ResetStopWatch()
        {
            stopWatch = 0.0f;
        }
    }
}
