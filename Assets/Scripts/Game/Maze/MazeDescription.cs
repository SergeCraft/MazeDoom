using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game.Maze
{
    public class MazeDescription
    {
        public List<CellDescription> CellDescriptions;
        public int RowCount;
        public int ColumnCount;

        public MazeDescription(List<CellDescription> cells, int columnCont, int rowCount)
        {
            CellDescriptions = cells;
            ColumnCount = columnCont;
            RowCount = rowCount;
        }

    }

    public class CellDescription
    {
        private Borders _borders;
        public int RowPosition { get; set; }
        public int ColumnPosition { get; set; }
        public int ThreadId { get; set; } = -1;

        public Borders Borders
        {
            get { return _borders; }
            set 
            {
                _borders = value; 
                UpdateType(); 
            }
        }
        public CellTypes Type { get; private set; }

        public CellDescription(int col, int row, bool top, bool right, bool bot, bool left)
        {
            RowPosition = row;
            ColumnPosition = col;
            Borders = new Borders (top, right, bot, left);            
        }

        public void SwitchBorders(bool top = false, bool right = false, bool bot = false, bool left = false)
        {
            Borders.Top = top ? !Borders.Top : Borders.Top;
            Borders.Right = right ? !Borders.Right : Borders.Right;
            Borders.Bot = bot ? !Borders.Bot : Borders.Bot;
            Borders.Left = left ? !Borders.Left : Borders.Left;

            UpdateType();
        }

        public void SetBorders(bool top, bool right, bool bot, bool left)
        {
            Borders.Top = top;
            Borders.Right = right;
            Borders.Bot = bot;
            Borders.Left = left;

            UpdateType();
        }

        public void UpdateType()
        {
            switch (Borders.Top, Borders.Right, Borders.Bot, Borders.Left)
            {
                case  (false, false, false, false):
                    Type = CellTypes.None;
                    break;
                case (false, false, false, true):
                    Type = CellTypes.Left;
                    break;
                case (true, false, false, false):
                    Type = CellTypes.Top;
                    break;
                case (false, true, false, false):
                    Type = CellTypes.Right;
                    break;
                case (false, false, true, false):
                    Type = CellTypes.Bot;
                    break;
                case (true, false, false, true):
                    Type = CellTypes.LeftTop;
                    break;
                case (true, true, false, false):
                    Type = CellTypes.TopRight;
                    break;
                case (false, true, true, false):
                    Type = CellTypes.RightBot;
                    break;
                case (false, false, true, true):
                    Type = CellTypes.BotLeft;
                    break;
                case (true, true, true, false):
                    Type = CellTypes.TopRightBot;
                    break;
                case (false, true, true, true):
                    Type = CellTypes.RightBotLeft;
                    break;
                case (true, false, true, true):
                    Type = CellTypes.BotLeftTop;
                    break;
                case (true, true, false, true):
                    Type = CellTypes.LeftTopRight;
                    break;
                case (false, true, false, true):
                    Type = CellTypes.LeftRight;
                    break;
                case (true, false, true, false):
                    Type = CellTypes.TopBot;
                    break;
                case (true, true, true, true):
                    Type = CellTypes.All;
                    break;
            }
        }
    }

    public class Borders
    {
        public bool Top;
        public bool Right;
        public bool Bot;
        public bool Left;

        public Borders(bool top, bool right, bool bot, bool left)
        {
            Top = top;
            Right = right;
            Bot = bot;
            Left = left;
        }
    }

    public enum CellTypes
    {
        Undefined = 0,
        None = 1,
        Left = 2,
        Top = 3,
        Right = 4,
        Bot = 5,
        LeftTop = 6,
        BotLeft = 7,
        RightBot = 8,
        TopRight = 9,
        TopRightBot = 10,
        LeftTopRight = 11,
        BotLeftTop = 12,
        RightBotLeft = 13,
        LeftRight = 14,
        TopBot = 15,
        All = 16
    }

    public enum BorderTypes
    {
        Top = 0,
        Right = 1, 
        Bot = 2,
        Left = 3
    }
}
