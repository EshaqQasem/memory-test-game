using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace memoryTestGame
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        
            levels = new List<Level>();
        }

        public List<Level> levels;
        private void mainForm_Load(object sender, EventArgs e)
        {

            readAndShowLevels();
          
        }


        public int LastOpenedLevelNum = 0;
        void readAndShowLevels()
        {
            StreamReader levelsInfoFile = new StreamReader(Directory.GetCurrentDirectory() + "\\levelsinfo.txt");
            string allFile = levelsInfoFile.ReadToEnd();
            levelsInfoFile.Close();

            string[] levelsInfo = allFile.Split('$');
            int YLoction=0;
            foreach (string s in levelsInfo)
            {
                string[] levelInfo = s.Split('#');

                int num = Convert.ToInt32(levelInfo[0]);
                int order = Convert.ToInt32(levelInfo[1]);
                int count = Convert.ToInt32(levelInfo[2]);
                bool state = !Convert.ToBoolean(Convert.ToInt32(levelInfo[3]));

                Level newLevel = new Level(num, order, count, state);
                levels.Add(newLevel);
                if (!state)
                    LastOpenedLevelNum = num;
                levelTitleVeiw veiw = new levelTitleVeiw(newLevel);
                veiw.Location = new Point(0, YLoction);
               
                veiw.Click += veiw_Click;
                YLoction += veiw.Height;
                this.panLevelsVeiw.Controls.Add(veiw);

            }
        }

        void veiw_Click(object sender, EventArgs e)
        {
            levelTitleVeiw clickedLevel = ((levelTitleVeiw)sender);
            if (levels[(int)clickedLevel.Tag - 1].Blocked)
            {
                MessageBox.Show(clickedLevel.Title + " is Locked! you have to pass the preveous levels");
            }
            else
            {
                playingForm CourtForm = new playingForm(this, (int)clickedLevel.Tag - 1);
                CourtForm.Show();
            }
        }

       

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StreamWriter levelsInfoFile = new StreamWriter("levelsinfo.txt");
            foreach (Level l in levels)
            {
                string level = l.levelNum.ToString() + "#" + l.Order.ToString() + "#"
                    + l.rememberedSquareNums.ToString()
                    + "#";
                level += l.Blocked ? "0" : "1";
                levelsInfoFile.WriteLine(level + "$");
            }
            levelsInfoFile.Close();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            playingForm CourtForm = new playingForm(this, LastOpenedLevelNum-1);
            CourtForm.Show();

        }
    }
}
