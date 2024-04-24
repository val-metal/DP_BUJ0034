using DP_BUJ0034.Engine;
using DP_BUJ0034.Engine.Generator;


namespace DP_BUJ0034.Game{

    public class GameBoard{

        public float height;
        public float width;
        public Player[] player { get; set; }
        public int num_paths { get; set; }
        public int difficulty { get; set; }
        public Paths[] path { get; set; }
        public Points[] start { get; set; }
        public Points[] end { get; set; }

        public List<Dots> playerHistory { get; }

        public bool saveHistory;

        public GameBoard(float height, float width, int num_paths,int difficulty){

            loadSettings();
            this.height = height;
            this.width = width;
            path = new Paths[num_paths];
            player=new Player[num_paths];
            start=new Points[num_paths];
            end=new Points[num_paths];
            playerHistory = new List<Dots>();
            this.num_paths = num_paths;
            this.difficulty = difficulty;
        }
        private async Task loadSettings()
        {
            SettingSave sw=await SettingLoader.load();
            if (sw.moveView)
            { 
                saveHistory = true;
            }
        }
        public void addUserMove(float x,float y)
        {
            if (saveHistory)
            {
                playerHistory.Add(new Dots(x, y));
            }
        }


        public bool isAllVisited()
        {
            if (end.Length > path.Length)
            {
                end.Last().isVisited = false;
                for (int i = 0; i < end.Length; i++)
                {
                    if (end[i].isVisited == true)
                    {
                        return true;
                    }

                }
            }
            else
            {
                for (int i = 0; i < end.Length; i++)
                {
                    if (end[i].isVisited == false)
                    {
                        return false;
                    }

                }
                return true;
            }
            return false;
        }

        public void generate_paths(float width, float height, int currentPath,IGenerator generator)
        {
            
            this.height = height;
            this.width = width;
            start[currentPath] = new Points(true, false, width / 16 + 15, height/(this.num_paths+1)*(currentPath+1));
            start[currentPath].df = difficulty;
            end[currentPath] = new Points(false, true, width / 16 * (float)(14.5), height /(this.num_paths + 1) * (currentPath + 1));

            path[currentPath] = generator.generatePath(start[currentPath], end[currentPath], width, height);
            if (path[currentPath].readyToConnect)
            {
                path[currentPath].dot.Reverse();
                for (int i = 0; i < path[currentPath].dot.Count; i++)
                {
                    path[0].dot.Add(path[currentPath].dot[i]);
                }
                for (int i = 0; i < path[0].dot.Count - 1; i++)
                {
                    Dots point1 = path[0].dot[i];
                    Dots point2 = path[0].dot[i + 1];

                    CalculateBezier_Points(point1, point2, i, 0);
                }
                Generate_dots_for_back_path_with_t(0);
                return;
            }
            if (!path[currentPath].noback)
            {
                for (int i = 0; i < path[currentPath].dot.Count - 1; i++)
                {
                    Dots point1 = path[currentPath].dot[i];
                    Dots point2 = path[currentPath].dot[i + 1];

                    CalculateBezier_Points(point1, point2, i, currentPath);
                }
                Generate_dots_for_back_path_with_t(currentPath);
            }
        }

       
        public void Generate_dots_for_back_path_with_t(int currentPath)
        {

            int distance = 20;
            if (difficulty == 1)
            { 
                distance = 35;
            }    
            for (int i = 0; i < path[currentPath].dot.Count() - 1; i++)
            {
                Dots p1 = path[currentPath].dot[i];
                Dots p2 = path[currentPath].controldots[i].Item1;
                Dots p3 = path[currentPath].controldots[i].Item2;
                Dots p4 = path[currentPath].dot[i + 1];

                for (float t = 0; t <= 0.95; t = t + (float)0.05)
                {
                    Dots point = getPointAt(t, p1, p2, p3, p4);
                    Dots normal = getNormalAt(t, p1, p2, p3, p4);

                    Dots offsetPoint = new Dots(
                x: point.x + normal.x * distance,
                y: point.y + normal.y * distance);


                    path[currentPath].backdot_with_t.Add(offsetPoint);
                    path[currentPath].t_path.Add(false);
                }
            }

        }


        public Dots getPointAt(float t, Dots p1, Dots p2, Dots p3, Dots p4)
        {
            float x = (float)Math.Pow(1 - t, 3) * p1.x +
                    3 * (float)Math.Pow(1 - t, 2) * t * p2.x +
                    3 * (1 - t) * (float)Math.Pow(t, 2) * p3.x +
                   (float)Math.Pow(t, 3) * p4.x;
            float y = (float)Math.Pow(1 - t, 3) * p1.y +
                    3 * (float)Math.Pow(1 - t, 2) * t * p2.y +
                    3 * (1 - t) * (float)Math.Pow(t, 2) * p3.y +
                    (float)Math.Pow(t, 3) * p4.y;

            Dots send = new Dots(x, y);
            return send;
        }

        public Dots getNormalAt(float t, Dots p1, Dots p2, Dots p3, Dots p4)
        {
            Dots d = getDerivateAt(t, p1, p2, p3, p4);
            float q = (float)Math.Sqrt(d.x * d.x + d.y * d.y);

            float x = -d.y / q;
            float y = d.x / q;

            Dots send = new Dots(x, y);
            return send;
        }
        public Dots getDerivateAt(float t, Dots p1, Dots p2, Dots p3, Dots p4)
        {
            float x = -3 * (float)Math.Pow(1 - t, 2) * p1.x +
            (3 * (float)Math.Pow(1 - t, 2) - 6 * (1 - t) * t) * p2.x +
            (6 * (1 - t) * t - 3 * (float)Math.Pow(t, 2)) * p3.x +
            3 * (float)Math.Pow(t, 2) * p4.x;
            float y = -3 * (float)Math.Pow(1 - t, 2) * p1.y +
                    (3 * (float)Math.Pow(1 - t, 2) - 6 * (1 - t) * t) * p2.y +
                    (6 * (1 - t) * t - 3 * (float)Math.Pow(t, 2)) * p3.y +
                    3 * (float)Math.Pow(t, 2) * p4.y;


            Dots send = new Dots(x, y);
            return send;
        }
        public void Generate_dor_for_3to1_path(float width, float height)
        {
            this.height = height;
            this.width = width;
            path[0] = new Paths();
            path[1] = new Paths();
            path[2] = new Paths();
            Paths temp = new Paths();
            float x = 0;
            float y = 0;
            Random random = new Random();
            x = (float)random.Next((int)(width / 16 * 10), (int)(width / 16 * 12));
            y = (float)random.Next((int)(height / 9 * 3), (int)(height / 9 * 6));

            start[0] = new Points(true, false, width / 16 + 15, height / 4);
            start[1] = new Points(true, false, width / 16 + 15, height / 4 * 2);
            start[2] = new Points(true, false, width / 16 + 15, height / 4 * 3);
            end[0] = new Points(false, true, width / 16 * 15, height / 2);
            //zpětný bod
            Points middle = new Points(false, false, x, y);

            //Generate_dot_for_path_1star(start[0], middle, 0,true);
            //Generate_dot_for_path_1star(start[2], middle, 2,true);

            path[2].dot.Reverse();

            for (int i = 0; i < path[2].dot.Count; i++)
            {
                path[0].dot.Add(path[2].dot[i]);
            }

            //Generate_dots1(start[1], end[0],1);
            int count_3to1 = 2;
            for(int j=0; j<count_3to1; j++)
            {
                for (int i = 0; i < path[j].dot.Count - 1; i++)
                {
                    Dots point1 = path[j].dot[i];
                    Dots point2 = path[j].dot[i + 1];

                    CalculateBezier_Points(point1, point2, i, j);
                }
                
                Generate_dots_for_back_path_with_t(j);
            }
            Paths[] paths = new Paths[2]; // Vytvoříme nové pole se dvěma prvky
            paths[0] = path[0]; // První prvek nového pole bude první prvek původního pole
            paths[1] = path[1]; // Druhý prvek nového pole bude druhý prvek původního pole
            path = new Paths[2];
            path = paths;


        }
       
       
        //Funkční výpočet kontrolních bodů
        public void CalculateBezier_Points(Dots point1, Dots point2,int i, int currentPath){  
            Dots controlPoint1;
            Dots controlPoint2;

            //Pokud ještě neexistuje kontrolní bod
            if (i == 0){
                Dots midPoint1 = Dots.Add(point1, Dots.Multiply(Dots.Subtract(point2, point1), (float)0.333)); 
                Dots midPoint2 = Dots.Add(point1, Dots.Multiply(Dots.Subtract(point2, point1), (float)0.666));            

                Random random = new Random();
                float random1 = random.Next(10, 31);
                float random2 = random.Next(10, 31);

                Dots vector1 = Dots.Subtract(point1, midPoint1);
                Dots vector2 = Dots.Subtract(point1, midPoint2);

                Dots normalizedVector1 = Dots.Normalize(vector1);
                controlPoint1 = Dots.Add(midPoint2, Dots.Multiply(normalizedVector1, random1));

                Dots normalizedVector2 = Dots.Normalize(vector2);
                controlPoint2 = Dots.Add(midPoint2, Dots.Multiply(normalizedVector2, random2));
            }
            //první kontrolní bod se vypočte 2xpočáteční bod-poslední kontrolní bod
            //druhý již náhodně
            else{
                Random random = new Random();
                var lastItem = path[currentPath].controldots.Last();
                Dots LastControlDot = lastItem.Item2;
                controlPoint1 = Dots.Subtract(Dots.Multiply(point1, 2), LastControlDot);

                float random_distance = (float)(random.NextDouble() * 0.2 + 0.5);
                Dots midPoint = Dots.Add(point1, Dots.Multiply(Dots.Subtract(point2, point1), random_distance));  

                float randomR = random.Next(10, 21);
                                
                // Vypočítání vektoru mezi středním bodem a controlPoint1
                Dots vector = Dots.Subtract(controlPoint1, midPoint);

                // Normalizace vektoru a násobení náhodnou hodnotou r
                Dots normalizedVector = Dots.Normalize(vector);
                controlPoint2 = Dots.Add(midPoint, Dots.Multiply(normalizedVector, randomR));

            }
            path[currentPath].controldots.Add((controlPoint1, controlPoint2));
        }
    }
}
