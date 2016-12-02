using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4180_final_gui
{
   
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        // Map object
        Map map;

        // Button dimensions and placement
        readonly int BUTTON_SIZE = 20;
        readonly int BUTTON_LEFT_MARGIN = 40;
        readonly int BUTTON_TOP_MARGIN = 80;

        // Generate the button matrix and display it on the screen
        private void Generate_Click(object sender, EventArgs e)
        {
            // Remove map if not null
            if (map != null && map.getButtonArray() != null)
            {
                Button[] buttonArr = map.getButtonArray();
                for (int i = 0; i < buttonArr.Length; i++)
                {
                    // Add the button to the window
                    this.Controls.Remove(buttonArr[i]);
                }
            }

            // Attempt to parse the values from the text input (may switch to dropdown later)
            int rowNum = 0;
            int colNum = 0;
            Int32.TryParse(rowBox.Text, out rowNum);
            Int32.TryParse(colBox.Text, out colNum);

            map = new Map(rowNum, colNum);

            // Values used for button positioning
            int horizontal = BUTTON_LEFT_MARGIN;
            int vertical = BUTTON_TOP_MARGIN;

            Button[] buttonArray = map.getButtonArray();

            // For each button array, initialize it and place it on the form, row major
            for (int i = 0; i < buttonArray.Length; i++)
            {
                // Set size and location
                buttonArray[i].Size = new Size(BUTTON_SIZE, BUTTON_SIZE);
                buttonArray[i].Location = new Point(horizontal, vertical);

                // Go to next row if column value reached
                if ((i + 1) % colNum == 0)
                {
                    horizontal = BUTTON_LEFT_MARGIN;
                    vertical += BUTTON_SIZE;
                }
                else horizontal += BUTTON_SIZE;

                // Add the button to the window
                this.Controls.Add(buttonArray[i]);
            }
        }

        private void Solve_Click(object sender, EventArgs e)
        {
            List<int> path = PathFinder.solve(map);
            map.paintPath(path);
            solve_text.Text = String.Join(",", path.ToArray());
        }

        private void Import_click(object sender, EventArgs e)
        {
            //String textToInput = System.IO.File.ReadAllText(@"C:\Users\Public\Export.txt");
            OpenFileDialog openMapDialog = new OpenFileDialog();
            if (openMapDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(openMapDialog.FileName);
                String textToInput = sr.ReadToEnd();
                sr.Close();

                map = new Map(textToInput);

                // Fill in text boxes
                rowBox.Text = map.getRowNum().ToString();
                colBox.Text = map.getColNum().ToString();

                // Values used for button positioning
                int horizontal = BUTTON_LEFT_MARGIN;
                int vertical = BUTTON_TOP_MARGIN;

                Button[] buttonArray = map.getButtonArray();

                // For each button array, initialize it and place it on the form, row major
                for (int i = 0; i < buttonArray.Length; i++)
                {
                    // Set size and location
                    buttonArray[i].Size = new Size(BUTTON_SIZE, BUTTON_SIZE);
                    buttonArray[i].Location = new Point(horizontal, vertical);

                    // Go to next row if column value reached
                    if ((i + 1) % map.getColNum() == 0)
                    {
                        horizontal = BUTTON_LEFT_MARGIN;
                        vertical += BUTTON_SIZE;
                    }
                    else horizontal += BUTTON_SIZE;

                    // Add the button to the window
                    this.Controls.Add(buttonArray[i]);
                }
            }
        }

        private void Export_click(object sender, EventArgs e)
        {
            // Text to output to the file
            String textToOutput = rowBox.Text + "," + colBox.Text + "," + String.Join(",", map.getIntMap());
            //System.IO.File.WriteAllText(@"C:\Users\Public\Export.txt", textToOutput);

            SaveFileDialog saveMapDialog = new SaveFileDialog();
            saveMapDialog.Filter = "Text File|*.txt";
            saveMapDialog.Title = "Save Map as Text File";

            // If the file name is not an empty string, open it for saving
            if (saveMapDialog.ShowDialog() == DialogResult.OK && saveMapDialog.FileName != "")
            {
                System.IO.StreamWriter outputFile = new System.IO.StreamWriter(saveMapDialog.FileName);
                outputFile.WriteLine(textToOutput);
                outputFile.Close();
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            map.clearPath();
            solve_text.Text = "";
        }
    }

    // Path finding class
    public class Map
    {
        private Button[] btnArray;
        private int rowNum;
        private int colNum;
        private static readonly String BUTTON_WALL_NUM = "1";
        private static readonly String BUTTON_START_NUM = "2";
        private static readonly String BUTTON_FINISH_NUM = "3";
        private static readonly String BUTTON_OPEN_NUM = "0";
        private static readonly String BUTTON_WALL = "W";
        private static readonly String BUTTON_START = "S";
        private static readonly String BUTTON_FINISH = "F";
        private static readonly String BUTTON_OPEN = "";
        private static readonly Color COLOR_OPEN_BUTTON = Color.White;
        private static readonly Color COLOR_WALL_BUTTON = Color.Black;
        private static readonly Color COLOR_START_BUTTON = Color.Blue;
        private static readonly Color COLOR_FINISH_BUTTON = Color.Red;
        private static readonly Color COLOR_PATH = Color.Green;

        public int getRowNum()
        {
            return rowNum;
        }

        public int getColNum()
        {
            return colNum;
        }

        public Button[] getButtonArray()
        {
            return btnArray;
        }
            
        public Map(int rowNum, int colNum) {
            this.rowNum = rowNum;
            this.colNum = colNum;
            this.btnArray = new Button[rowNum * colNum];

            for (int i = 0; i < btnArray.Length; i++)
            {
                // Initialize tiles
                btnArray[i] = new Button();
                btnArray[i].BackColor = Color.White;
                btnArray[i].Click += new System.EventHandler(toggleButton);
            }
        }


        public Map(String mapText)
        {
            char delimiterChar = ',';
            String[] words = mapText.Split(delimiterChar);

            // Copied from Generate_click
            // Attempt to parse the values from the text input (may switch to dropdown later)
            Int32.TryParse(words[0], out rowNum);
            Int32.TryParse(words[1], out colNum);

            // Allocate memory for button array
            this.btnArray = new Button[rowNum * colNum];

            // For each button array, initialize it and place it on the form, row major
            for (int i = 0; i < btnArray.Length; i++)
            {
                // Initialize tiles
                btnArray[i] = new Button();
                if (words[i + 2] == BUTTON_OPEN_NUM) { btnArray[i].BackColor = COLOR_OPEN_BUTTON; btnArray[i].Text = BUTTON_OPEN; }
                else if (words[i + 2] == BUTTON_WALL_NUM) { btnArray[i].BackColor = COLOR_WALL_BUTTON; btnArray[i].Text = BUTTON_WALL; }
                else if (words[i + 2] == BUTTON_START_NUM) { btnArray[i].BackColor = COLOR_START_BUTTON; btnArray[i].Text = BUTTON_START; }
                else if (words[i + 2] == BUTTON_FINISH_NUM) { btnArray[i].BackColor = COLOR_FINISH_BUTTON; btnArray[i].Text = BUTTON_FINISH; }
                btnArray[i].Click += new System.EventHandler(toggleButton);
            }
        }

        // Check if a start square exists in the button array
        public bool startExists()
        {
            foreach (Button btn in btnArray)
            {
                if (btn.Text == BUTTON_START) return true;
            }
            return false;
        }

        // Check if an finish square exists in the button array
        public bool finishExists()
        {
            foreach (Button btn in btnArray)
            {
                if (btn.Text == BUTTON_FINISH) return true;
            }
            return false;
        }

        // Get location with text as parameter
        private int[] getLoc(String s)
        {
            int row = 0;
            int col = 0;
            int[] loc = { -1, -1 };
            foreach (Button btn in btnArray)
            {
                if (btn.Text.Equals(s))
                {
                    loc[0] = row;
                    loc[1] = col;
                    return loc;
                }
                col++;
                if (col % colNum == 0)
                {
                    row++;
                    col = 0;
                }
            }
            return loc;
        }

        // Get location of start
        public int[] getStartLoc()
        {
            return getLoc(BUTTON_START);
        }

        // Get location of finish
        public int[] getFinishLoc()
        {
            return getLoc(BUTTON_FINISH);
        }

        // Paint the path green on the map
        // Path is array of integers
        // [5][4][3]
        // [6][ ][2]
        // [7][0][1]

        public void paintPath(List<int> path)
        {
            int[] currentLoc = getStartLoc();
            foreach (int dir in path)
            {
                switch (dir)
                {
                    case 0: // south
                        currentLoc[0]++;
                        break;
                    case 1: // southeast
                        currentLoc[0]++;
                        currentLoc[1]++;
                        break;
                    case 2: // east
                        currentLoc[1]++;
                        break;
                    case 3: // northeast
                        currentLoc[0]--;
                        currentLoc[1]++;
                        break;
                    case 4: // north
                        currentLoc[0]--;
                        break;
                    case 5: // northwest
                        currentLoc[0]--;
                        currentLoc[1]--;
                        break;
                    case 6: //west
                        currentLoc[1]--;
                        break;
                    case 7: // southwest
                        currentLoc[0]++;
                        currentLoc[1]--;
                        break;
                    default:
                        break;
                    // TODO throw error
                }
                btnArray[colNum * currentLoc[0] + currentLoc[1]].BackColor = COLOR_PATH;
            }
        }

        public void clearPath()
        {
            foreach (Button btn in btnArray)
            {
                if (btn.Text == BUTTON_FINISH)
                {
                    btn.BackColor = COLOR_FINISH_BUTTON;
                }
                else if (btn.BackColor == COLOR_PATH)
                {
                    btn.BackColor = COLOR_OPEN_BUTTON;
                }
            }
        }

        // Parse map and return values
        public int[] getIntMap()
        {
            // Reallocate memory for map
            int[] mapArray = new int[btnArray.Length];

            for (int i = 0; i < btnArray.Length; i++)
            {
                if (btnArray[i].Text == BUTTON_WALL)
                {
                    mapArray[i] = 1;
                }
                else if (btnArray[i].Text == BUTTON_START)
                {
                    mapArray[i] = 2;
                }
                else if (btnArray[i].Text == BUTTON_FINISH)
                {
                    mapArray[i] = 3;
                }
                else
                {
                    mapArray[i] = 0;
                }
            }

            return mapArray;
        }

        // The click handler for each tile
        public void toggleButton(Object sender, System.EventArgs e)
        {
            Button btn = (Button)sender;
            // If white space, set to wall
            if (btn.Text == BUTTON_OPEN)
            {
                btn.Text = BUTTON_WALL;
                btn.BackColor = Color.Black;
            }
            // If wall, set to BUTTON_START if no start, set to BUTTON_FINISH if no finish
            // Else, clear
            else if (btn.Text == BUTTON_WALL)
            {
                // If start square does not exist, set to start
                if (!startExists())
                {
                    btn.BackColor = COLOR_START_BUTTON;
                    btn.Text = BUTTON_START;
                }
                // If end square does not exist, set to end
                else if (!finishExists())
                {
                    btn.BackColor = COLOR_FINISH_BUTTON;
                    btn.Text = BUTTON_FINISH;
                }
                else
                {
                    btn.BackColor = COLOR_OPEN_BUTTON;
                    btn.Text = BUTTON_OPEN;
                }
            }
            // If start, set to finish if no finish
            // Else, clear
            else if (btn.Text == BUTTON_START)
            {
                // If end square does not exist, set to finish
                if (!finishExists())
                {
                    btn.BackColor = COLOR_FINISH_BUTTON;
                    btn.Text = BUTTON_FINISH;
                }
                // Else, set to default value (no wall)
                else
                {
                    btn.BackColor = COLOR_OPEN_BUTTON;
                    btn.Text = BUTTON_OPEN;
                }
            }
            // If finish, clear
            else if (btn.Text == BUTTON_FINISH)
            {
                btn.BackColor = COLOR_OPEN_BUTTON;
                btn.Text = BUTTON_OPEN;
            }
        }


    }
    public class PathFinder
    {
        static int dir = 8;
        static int[] dx = { 1, 1, 0, -1, -1, -1, 0, 1 };
        static int[] dy = { 0, 1, 1, 1, 0, -1, -1, -1 };

        private class Node
        {
            // current position
            private int xPos;
            private int yPos;
            // total distance already travelled to reach the node
            private int level;
            // priority=level+remaining distance estimate
            private int priority;  // smaller: higher priority

            public Node(int xp, int yp, int d, int p)
            { xPos = xp; yPos = yp; level = d; priority = p; }

            public int getxPos() { return xPos; }
            public int getyPos() { return yPos; }
            public int getLevel() { return level; }
            public int getPriority() { return priority; }

            public void updatePriority(int xDest, int yDest)
            {
                priority = level + estimate(xDest, yDest) * 10; //A*
            }

            // give better priority to going strait instead of diagonally
            public void nextLevel(int i) // i: direction
            {
                level += (i % 2 == 0 ? 10 : 14);
            }

            // Estimation function for the remaining distance to the goal.
            public int estimate(int xDest, int yDest)
            {
                int xd, yd, d;
                xd = xDest - xPos;
                yd = yDest - yPos;

                // Euclidian Distance
                d = (int)(Math.Sqrt(xd * xd + yd * yd));

                // Manhattan distance
                //d=abs(xd)+abs(yd);

                // Chebyshev distance
                //d=max(abs(xd), abs(yd));

                return (d);
            }
            public bool compare(Node other)
            {
                return this.getPriority() > other.getPriority();
            }
        }

        public static List<int> solve(Map map)
        {
            int n = map.getColNum(); // horizontal
            int m = map.getRowNum(); // vertical

            int xStart = map.getStartLoc()[0];
            int yStart = map.getStartLoc()[1];
            int xFinish = map.getFinishLoc()[0];
            int yFinish = map.getFinishLoc()[1];

            int[] int_map = map.getIntMap();
            int[] closed_nodes_map = new int[n * m];
            int[] open_nodes_map = new int[n * m];
            int[] dir_map = new int[n * m];

            //srand(time(NULL));

            List<Node>[] pq = new List<Node>[2];// list of open (not-yet-tried) nodes
            pq[0] = new List<Node>();
            pq[1] = new List<Node>();
            int pqi; // pq index
            Node n0;
            Node m0;
            int i, j, x, y, xdx, ydy;
            x = 0;
            char c;
            pqi = 0;

            // reset the node maps
            for (y = 0; y < m; y++)
            {
                for (x = 0; x < n; x++)
                {
                    closed_nodes_map[x * m + y] = 0;
                    open_nodes_map[x * m + y] = 0;
                }
            }

            // create the start node and push into list of open nodes
            n0 = new Node(xStart, yStart, 0, 0);
            n0.updatePriority(xFinish, yFinish);
            pq[pqi].Add(n0);
            open_nodes_map[xStart * m + yStart] = n0.getPriority(); // mark it on the open nodes map

            // A* search
            while (pq[pqi].Any())
            {
                // get the current node w/ the highest priority
                // from the list of open nodes
                pq[pqi].Sort((p1, p2) =>
                {
                    if (p1.getPriority() > p2.getPriority())
                    {
                        return 1;
                    }
                    else if (p1.getPriority() < p2.getPriority())
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                });
                n0 = new Node(pq[pqi].First().getxPos(), pq[pqi].First().getyPos(),
                             pq[pqi].First().getLevel(), pq[pqi].First().getPriority());

                x = n0.getxPos(); y = n0.getyPos();

                pq[pqi].RemoveAt(0); // remove the node from the open list
                open_nodes_map[x * m + y] = 0;
                // mark it on the closed nodes map
                closed_nodes_map[x * m + y] = 1;

                // quit searching when the goal state is reached
                //if((*n0).estimate(xFinish, yFinish) == 0)
                if (x == xFinish && y == yFinish)
                {
                    // generate the path from finish to start
                    // by following the directions
                    List<int> path = new List<int>();
                    while (!(x == xStart && y == yStart))
                    {
                        j = dir_map[x * m + y];
                        c = (char)('0' + (j + dir / 2) % dir);
                        path.Insert(0, (j + dir / 2) % dir);
                        x += dx[j];
                        y += dy[j];
                    }

                    // garbage collection
                    //delete n0;
                    // empty the leftover nodes
                    while (pq[pqi].Any()) pq[pqi].RemoveAt(0);
                    return path;
                }

                // generate moves (child nodes) in all possible directions
                for (i = 0; i < dir; i++)
                {
                    xdx = x + dx[i]; ydy = y + dy[i];

                    if (!(xdx < 0 || xdx > n - 1 || ydy < 0 || ydy > m - 1 || int_map[xdx * m + ydy] == 1
                        || closed_nodes_map[xdx * m + ydy] == 1))
                    {
                        // generate a child node
                        m0 = new Node(xdx, ydy, n0.getLevel(), n0.getPriority());
                        m0.nextLevel(i);
                        m0.updatePriority(xFinish, yFinish);

                        // if it is not in the open list then add into that
                        if (open_nodes_map[xdx * m + ydy] == 0)
                        {
                            open_nodes_map[xdx * m + ydy] = m0.getPriority();
                            pq[pqi].Add(m0);
                            // mark its parent node direction
                            dir_map[xdx * m + ydy] = (i + dir / 2) % dir;
                        }
                        else if (open_nodes_map[xdx * m + ydy] > m0.getPriority())
                        {
                            // update the priority info
                            open_nodes_map[xdx * m + ydy] = m0.getPriority();
                            // update the parent direction info
                            dir_map[xdx * m + ydy] = (i + dir / 2) % dir;

                            // replace the node
                            // by emptying one pq to the other one
                            // except the node to be replaced will be ignored
                            // and the new node will be pushed in instead

                            pq[pqi].Sort((p1, p2) =>
                            {
                                if (p1.getPriority() > p2.getPriority())
                                {
                                    return 1;
                                }
                                else if (p1.getPriority() < p2.getPriority())
                                {
                                    return -1;
                                }
                                else
                                {
                                    return 0;
                                }
                            });

                            while (!(pq[pqi].First().getxPos() == xdx &&
                                   pq[pqi].First().getyPos() == ydy))
                            {
                                pq[1 - pqi].Add(pq[pqi].First());
                                pq[pqi].RemoveAt(0);
                            }
                            pq[pqi].RemoveAt(0); // remove the wanted node

                            // TODO: Sort?
                            // empty the larger size pq to the smaller one
                            if (pq[pqi].Count() > pq[1 - pqi].Count()) pqi = 1 - pqi;
                            while (pq[pqi].Any())
                            {
                                pq[1 - pqi].Add(pq[pqi].First());
                                pq[pqi].RemoveAt(0);
                            }
                            pqi = 1 - pqi;
                            pq[pqi].Add(m0); // add the better node instead
                        }
                        //else delete m0; // garbage collection
                    }
                }
                //delete n0; // garbage collection
            }
            return new List<int>(); // no route found
        }
    }
   
}
