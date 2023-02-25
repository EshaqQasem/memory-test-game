using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memoryTestGame
{
    public class Level
    {
        public Level() : this(0, 0, 0, false) { }
        public Level(int num, int order, int count, bool blocked)
        {
            this.levelNum = num;
            this.Order = order;
            this.rememberedSquareNums = count;
            this.Blocked = blocked;
        }




        public int Order { set; get; }
        public int rememberedSquareNums { set; get; }
        public int levelNum { set; get; }
        public bool Blocked { set; get; }

        public void Open()
        {
            this.Blocked = false;
        }

        public void Close()
        {
            this.Blocked = true;
        }
    }
}
