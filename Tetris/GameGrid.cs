using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class GameGrid
    {
        private readonly int[,] grid;//二维数组
        public int Rows { get; }
        public int Columns { get; }
        public int this[int r, int c] //indexer 允许一个对象可以像数组一样使用下标的方式来访问。
        {
            get=> grid[r,c];
            set=>grid[r,c] = value;
        }
        public GameGrid(int rows, int columns) //构造函数：无返回类型的方法
        {
            Rows = rows;
            Columns = columns;
            grid = new int[rows, columns]; //if value of grid[r,c] equals 0, the grid is empty
        }
        public bool IsInside(int r, int c)
        {
            return r>=0 &&r<Rows && c>=0 &&c<Columns;
        }

        public bool IsEmpty(int r, int c)
        {
            return IsInside(r,c) && grid[r,c]==0;//get value by a indexer
        }
        public bool IsRowFull(int r) 
        {
            for (int c=0;c<Columns;c++)
            {
                if (grid[r,c]==0)
                {
                    return false;
                }
                
            }
            return true;
        }//if row full, then this row should be cleared and the others be moved down by one row.

        public bool IsRowEmpty(int r) //if isRowEmpty, then this row above should be moved down
        {
            for (int c = 0;c<Columns;c++)
            {
                if (grid[r,c]!=0) {
                    return false;
                }
            }
            return true;
        }

        private void ClearRow(int r)
        {
            for (int c=0;c<Columns;c++)
            {
                grid[r, c] = 0;  //whole row will be 0
            }
        }

        private void MoveRowDown(int r, int numRows)
        {
            for (int c=0;c<Columns;c++)
            {
                grid[r + numRows, c] = grid[r, c];//the next row will be the same as above
                grid[r, c] = 0; //the above row will be emptied.
            }
        }

        public int ClearFullRows() //返回清除满格行的数量
        {
            int cleared = 0;
            for (int r=Rows-1;r>=0;r--) //from bottom to up
            {
                if (IsRowFull(r))
                {
                    ClearRow(r);
                    cleared++;
                }
                else if (cleared>0) 
                {
                MoveRowDown(r,cleared);
                }
            }
            return cleared;
        }

    }
}
