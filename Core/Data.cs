using System;


namespace SudokuData
{
    public class Block
    {
        public Block()
        {
            numbers = new int[10];
        }
        public int id;
        public int row;
        public int column;
        public int area;
        public int[] numbers;
        public int poss;
    }

    public class Row
    {
        public Row()
        {
            blockids = new int[9];
            need = new int[10];
            count = new int[10];
        }
        public int id;
        public int[] blockids;
        public int[] need;
        public int[] count;
    }

    public class Column
    {
        public Column()
        {
            blockids = new int[9];
            need = new int[10];
            count = new int[10];
        }
        public int id;
        public int[] blockids;
        public int[] need;
        public int[] count;
    }

    public class Area
    {
        public Area()
        {
            blockids = new int[9];
            need = new int[10];
            count = new int[10];
        }
        public int id;
        public int[] blockids;
        public int[] need;
        public int[] count;
    }





    public class Table
    {
        Block[] blocks;
        Row[] rows;
        Column[] columns;
        Area[] areas;

        int tryi;
        int[] tryn;

        public Table()
        {
            this.blocks = new Block[81];
            this.rows = new Row[9];
            this.columns = new Column[9];
            this.areas = new Area[9];
            tryi = 0;
            tryn = new int[2];
            int i, j;
            //初始化所有格子
            for (i = 0; i < 81; i++)
            {
                this.blocks[i] = new Block();
                this.blocks[i].id = i;
                this.blocks[i].row = i / 9;
                this.blocks[i].column = i % 9;
                this.blocks[i].area = i / 9 / 3 * 3 + (i % 9) / 3;
                this.blocks[i].numbers[0] = 0;
                for (j = 1; j < 10; j++)
                {
                    this.blocks[i].numbers[j] = 1;
                }
                this.blocks[i].poss = 9;
            }
            //初始化行信息
            for (i = 0; i < 9; i++)
            {
                this.rows[i] = new Row();
                this.rows[i].id = i;
                for (j = 0; j < 9; j++)
                {
                    this.rows[i].blockids[j] = i * 9 + j;
                    this.rows[i].need[j + 1] = 1;
                    this.rows[i].count[j + 1] = 9;
                }
            }
            //初始化列信息
            for (i = 0; i < 9; i++)
            {
                this.columns[i] = new Column();
                this.columns[i].id = i;
                for (j = 0; j < 9; j++)
                {
                    this.columns[i].blockids[j] = j * 9 + i;
                    this.columns[i].need[j + 1] = 1;
                    this.columns[i].count[j + 1] = 9;
                }
            }
            //初始化块信息
            for (i = 0; i < 9; i++)
            {
                this.areas[i] = new Area();
                this.areas[i].id = i;
                for (j = 0; j < 9; j++)
                {
                    this.areas[i].blockids[j] = (i / 3 * 3 + j / 3) * 9 + (i % 3 * 3 + j % 3);
                    this.areas[i].need[j + 1] = 1;
                    this.areas[i].count[j + 1] = 9;
                }
            }
        }

        public void copy(Table t)
        {
            for (int i = 0; i < 81; i++)
            {
                if (t.blocks[i].numbers[0] != 0)
                {
                    this.fill(i, t.blocks[i].numbers[0]);
                }
            }
        }

        public void creat(int[,] puzzle)
        {
            int i, j;
            for (i = 0; i < 9; i++)
            {
                for (j = 0; j < 9; j++)
                {
                    if (puzzle[i, j] > 0)
                    {
                        this.fill(i * 9 + j, puzzle[i, j]);
                    }
                }
            }
        }

        //第i个格子填n
        public Table fill(int i, int n)
        {
            this.blocks[i].numbers[0] = n;
            int j;
            for (j = 1; j < 10; j++)
            {
                this.blocks[i].numbers[j] = 0;
            }
            this.blocks[i].poss = 0;
            this.rows[this.blocks[i].row].need[n] = 0;
            for (j = 0; j < 9; j++)
            {
                if (this.blocks[this.rows[this.blocks[i].row].blockids[j]].numbers[n] == 1)
                {
                    this.blocks[this.rows[this.blocks[i].row].blockids[j]].numbers[n] = 0;
                    if (this.blocks[this.rows[this.blocks[i].row].blockids[j]].poss > 0)
                    {
                        this.blocks[this.rows[this.blocks[i].row].blockids[j]].poss--;
                    }
                }
            }

            this.columns[this.blocks[i].column].need[n] = 0;
            for (j = 0; j < 9; j++)
            {
                if (this.blocks[this.columns[this.blocks[i].column].blockids[j]].numbers[n] == 1)
                {
                    this.blocks[this.columns[this.blocks[i].column].blockids[j]].numbers[n] = 0;
                    if (this.blocks[this.columns[this.blocks[i].column].blockids[j]].poss > 0)
                    {
                        this.blocks[this.columns[this.blocks[i].column].blockids[j]].poss--;
                    }
                }
            }

            this.areas[this.blocks[i].area].need[n] = 0;
            for (j = 0; j < 9; j++)
            {
                if (this.blocks[this.areas[this.blocks[i].area].blockids[j]].numbers[n] == 1)
                {
                    this.blocks[this.areas[this.blocks[i].area].blockids[j]].numbers[n] = 0;
                    if (this.blocks[this.areas[this.blocks[i].area].blockids[j]].poss > 0 &&
                        this.blocks[this.areas[this.blocks[i].area].blockids[j]].row != this.blocks[i].row &&
                        this.blocks[this.areas[this.blocks[i].area].blockids[j]].column != this.blocks[i].column)
                    {
                        this.blocks[this.areas[this.blocks[i].area].blockids[j]].poss--;
                    }
                }
            }

            return this;
        }

        public void getcount()
        {
            int i, j, k;
            for (i = 0; i < 9; i++)
            {
                for (j = 1; j < 10; j++)
                {
                    this.rows[i].count[j] = 0;
                    this.columns[i].count[j] = 0;
                    this.areas[i].count[j] = 0;
                    for (k = 0; k < 9; k++)
                    {
                        if (this.blocks[this.rows[i].blockids[k]].numbers[j] == 1)
                        {
                            this.rows[i].count[j]++;
                        }
                        if (this.blocks[this.columns[i].blockids[k]].numbers[j] == 1)
                        {
                            this.columns[i].count[j]++;
                        }
                        if (this.blocks[this.areas[i].blockids[k]].numbers[j] == 1)
                        {
                            this.areas[i].count[j]++;
                        }
                    }
                }
            }
        }

        public int scanblock()
        {
            int i, j;
            for (i = 0; i < 81; i++)
            {
                if (this.blocks[i].poss == 1)
                {
                    for (j = 1; j < 10; j++)
                    {
                        if (this.blocks[i].numbers[j] == 1)
                        {
                            //cout << "第" << i/ 9 + 1 << "行 " << i% 9 + 1 << "列填" << j << "\n";
                            this.fill(i, j);
                            return 1;
                        }
                    }
                }
                else if (this.blocks[i].poss == 0 && this.blocks[i].numbers[0] == 0)
                {
                    //Console.WriteLine("第" + (i / 9 + 1) + "行 "+(i % 9 + 1) + "列矛盾");
                    return -1;
                }
            }
            return 0;
        }

        public int scan()
        {
            int i, j, k;
            for (i = 0; i < 9; i++)
            {
                for (j = 1; j < 10; j++)
                {
                    if (this.rows[i].count[j] == 1)
                    {
                        for (k = 0; k < 9; k++)
                        {
                            if (this.blocks[this.rows[i].blockids[k]].numbers[j] == 1)
                            {
                                //cout << "第" << this.rows[i].blockids[k] / 9+1<< "行 " << this.rows[i].blockids[k] % 9+1 << "列填" << j << "\n";
                                fill(this.rows[i].blockids[k], j);
                                return 1;
                            }
                        }
                    }
                }
            }
            for (i = 0; i < 9; i++)
            {
                for (j = 1; j < 10; j++)
                {
                    if (this.columns[i].count[j] == 1)
                    {
                        for (k = 0; k < 9; k++)
                        {
                            if (this.blocks[this.columns[i].blockids[k]].numbers[j] == 1)
                            {
                                //cout << "第" << this.columns[i].blockids[k] / 9+1 << "行 " << this.columns[i].blockids[k] % 9+1 << "列填" << j << "\n";
                                fill(this.columns[i].blockids[k], j);
                                return 1;
                            }
                        }
                    }
                }
            }
            for (i = 0; i < 9; i++)
            {
                for (j = 1; j < 10; j++)
                {
                    if (this.areas[i].count[j] == 1)
                    {
                        for (k = 0; k < 9; k++)
                        {
                            if (this.blocks[this.areas[i].blockids[k]].numbers[j] == 1)
                            {
                                //cout << "第" << this.areas[i].blockids[k] /9+1<<"行 "<< this.areas[i].blockids[k] %9+1<<"列填" << j << "\n";
                                fill(this.areas[i].blockids[k], j);
                                return 1;
                            }
                        }
                    }
                }
            }
            return 0;
        }

        public int check()
        {
            int i;
            int over = 1;
            int isnext = 0;
            for (i = 0; i < 81; i++)
            {
                if (this.blocks[i].poss > 0)
                {
                    over = 0;
                    break;
                }
            }
            if (over == 1)
            {
                return 1;
            }
            else
            {
                for (i = 0; i < 81; i++)
                {
                    if (this.blocks[i].poss == 2)
                    {
                        tryi = i;
                        break;
                    }
                }
                for (i = 1; i < 10; i++)
                {
                    if (this.blocks[tryi].numbers[i] == 1)
                    {
                        if (isnext == 0)
                        {
                            tryn[0] = i;
                            isnext = 1;
                        }
                        else
                        {
                            tryn[1] = i;
                            break;
                        }
                    }
                }
            }
            return 0;
        }

        public int work1()
        {
            int changed1 = 0;
            int changed2 = 0;
            do
            {
                do
                {
                    changed1 = this.scanblock();
                    if (changed1 == -1)
                    {
                        return -1;
                    }
                    this.getcount();
                } while (changed1 == 1);
                do
                {
                    changed2 = this.scan();
                    if (changed2 == 1)
                    {
                        changed1 = 1;
                    }
                    this.getcount();
                } while (changed2 == 1);
            } while (changed1 == 1 || changed2 == 1);
            return 0;
        }

        public int solve()
        {

            Table[] tabletry = new Table[200];

            int nOfKeys = 0;
            int error = 0;
            int point = 0;
            this.getcount();
            int exit = 0;
            exit = this.work1();
            int end = 0;
            end = this.check();
            if (end == 0)
            {
                tabletry[point] = new Table();
                tabletry[point].copy(this);
                point++;
                tabletry[point] = new Table();
                tabletry[point] = this.fill(tryi, tryn[0]);
                point--;
                tabletry[point].fill(tryi, tryn[1]);
                point++;
                if (exit == 0)
                {
                    while (true)
                    {
                        error = tabletry[point].work1();

                        if (error == -1)
                        {
                            point--;
                            if (point < 0)
                            {
                                //Console.WriteLine("无解");
                                return nOfKeys;
                            }
                        }
                        else
                        {
                            if (tabletry[point].check() == 1)
                            {

                                //Console.WriteLine("over");
                                //tabletry[point].printtable();
                                nOfKeys++;
                                point--;
                                if (point < 0)
                                {
                                    break;
                                }

                            }
                            else
                            {
                                tryi = tabletry[point].tryi;
                                tryn[0] = tabletry[point].tryn[0];
                                tryn[1] = tabletry[point].tryn[1];
                                point++;
                                tabletry[point] = new Table();
                                tabletry[point].copy(tabletry[point - 1]);
                                point--;
                                tabletry[point].fill(tryi, tryn[1]);
                                point++;
                                tabletry[point].fill(tryi, tryn[0]);


                            }
                        }
                    }
                }
                else
                {
                    //Console.WriteLine("无解");
                    return nOfKeys;
                }

            }
            else
            {
                nOfKeys++;
                //Console.WriteLine("over once");
                //this.printtable();
            }
            return nOfKeys;
        }

    }
}
