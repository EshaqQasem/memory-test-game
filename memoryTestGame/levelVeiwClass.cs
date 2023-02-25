using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace memoryTestGame
{
    class levelTitleVeiw:Button
    {
        public levelTitleVeiw()
        {
         
            
            this.FlatStyle = FlatStyle.Flat;
            this.Font =new  Font("Tahoma", 11F);
            this.ImageAlign = ContentAlignment.MiddleRight;
            this.Padding = new Padding(10, 0, 10, 0);
            this.Size = new Size(267, 49);
            this.TextAlign = ContentAlignment.MiddleLeft;
            this.TextImageRelation = TextImageRelation.TextBeforeImage;
            this.UseVisualStyleBackColor = true;
         
        }

        public levelTitleVeiw(Level level):this()
        {

          this.Title = "Level " + level.levelNum.ToString();
          this.Icon = level.Blocked ?levelTitleVeiw.lockedIcon: levelTitleVeiw.unlockedIcon  ;
          this.TabIndex = level.levelNum;
          this.Tag = level.levelNum;
          this.Location = new Point(0, 100);

        }

        private string title;
        public string Title
        {
            set
            {
                this.Text = this.title = value;
            }
            get 
            { return title; }
        }
        private Image icon;
        public Image Icon
        {
            set
            {
                this.Image = this.icon = value;
            }
            get
            {
                return icon;
            }
        }

        public static Image lockedIcon = Properties.Resources.locked;
        public static Image unlockedIcon= Properties.Resources.unlocked;

    }

}


