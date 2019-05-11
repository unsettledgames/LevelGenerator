using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RoomGenerator
{
    class Level
    {
        /* Dati sulla generazione delle stanze */
        private int minRoomWidth;
        private int maxRoomWidth;
        private int minRoomHeight;
        private int maxRoomHeight;

        /* Dati sulla generazione dei corridoi */
        private int minCorridorWidth;
        private int maxCorridorWidth;
        private int minCorridorHeight;
        private int maxCorridorHeight;

        /* Indica il numero massimo di corridoi per lato della stanza */
        private int maxCorridorsPerSide;
        /* Indica la probabilità che una stanza abbia un corridoio (ricalcolata per ogni lato) */
        private int corridorProbability;
        /* Indica la probabilità che, nella generazione di una stanza si cambi il punto di attacco dei corridoi (
         * tendenzialmente serve a rendere un livello più casuale, cercando di distribuire il numero dei corridoi */
        private int roomChangeProbability;
        /* Indica il numero di stanze che compone il livello */
        private int nRooms;

        /* Matrice in cui verrà salvato il livello generato (sottoforma di 1 e di 0) */
        private int[][] level;
        /* Bitmap in cui verrà salvato il livello generato (con i colori impostati) */
        private Bitmap bitmap;
        /* Colore di sfondo */
        private Color backgroundColor;
        /* Colore delle stanze */
        private Color foregroundColor;

        /* In queste due liste salvo stanze e corridoi */
        private List<Room> rooms;
        private List<Corridor> corridors;

        public Level(int minRoomWidth, int maxRoomWidth, int minRoomHeight, int maxRoomHeight, int minCorridorWidth,
                     int maxCorridorWidth, int minCorridorHeight, int maxCorridorHeight, int maxCorridorsPerSide, 
                     int corridorProbability, int nRooms, Color backgroundColor, Color foregroundColor, int roomChangeProbability)
        {
            /* Imposto i dati sulle stanze */
            this.minRoomWidth = minRoomWidth;
            this.minRoomHeight = minRoomHeight;
            this.maxRoomWidth = maxRoomWidth;
            this.maxRoomHeight = maxRoomHeight;

            /* Imposto i dati sui corridoi */
            this.maxCorridorHeight = maxCorridorHeight;
            this.minCorridorHeight = minCorridorHeight;
            this.maxCorridorWidth = maxCorridorWidth;
            this.minCorridorWidth = minCorridorWidth;

            /* Imposto i dati sulla generazione */
            this.maxCorridorsPerSide = maxCorridorsPerSide;
            this.corridorProbability = corridorProbability;
            this.nRooms = nRooms;
            this.backgroundColor = backgroundColor;
            this.foregroundColor = foregroundColor;
            this.roomChangeProbability = roomChangeProbability;

            /* Creo le liste */
            this.rooms = new List<Room>();
            this.corridors = new List<Corridor>();

            /* Creo la bitmap (teoricamente deve essere cancellata una volta che vengono applicate le textures) */
            this.bitmap = new Bitmap(Consts.MAX_LEVEL_WIDTH, Consts.MAX_LEVEL_HEIGHT);

            InitializeMatrix();
        }

        private void InitializeMatrix()
        {
            this.level = new int[Consts.MAX_LEVEL_WIDTH][];

            for (int i=0; i<Consts.MAX_LEVEL_WIDTH; i++)
            {
                this.level[i] = new int[Consts.MAX_LEVEL_HEIGHT];
            }

            for (int i=0; i<Consts.MAX_LEVEL_WIDTH; i++)
            {
                for (int j=0; j<Consts.MAX_LEVEL_HEIGHT; j++)
                {
                    this.level[i][j] = 0;
                }
            }
        }

        public void GenerateMap()
        {
            /* Generatore di numeri casuali */
            Random random = new Random();
            /* Lista delle stanze espandibili */
            List<Room> expandableRooms;

            /* Genero la prima stanza */
            Room start = new Room(random.Next(minRoomWidth, maxRoomWidth), random.Next(minRoomHeight, maxRoomHeight), new Corner(0, 0));
            /* La aggiungo alla lista */
            rooms.Add(start);

            /* Finché non ho generato tutte le stanze che mi servono */
            while (rooms.Count < nRooms)
            {
                /* Prendo la stanza a cui aggiungere tutto */
                Room toAddTo = GetFirstExpandableRoom();

                /* Finché non ho generato tutte le stanze che mi servono o non posso più aggiungere corridoi */
                while ((rooms.Count < nRooms) && (toAddTo.GetNCorridors() < (maxCorridorsPerSide * Consts.SIDE_COUNT)) && (toAddTo.GetNCorridors() < toAddTo.GetMaxCorridors()))
                {
                    /* Entro nel loop di aggiunta dei corridoi */
                    /* Provo a cambiare stanza */
                    if (random.Next(0, 101) < roomChangeProbability)
                    {
                        /* Se la probailità è tale da permettermi di cambiare stanza, ne prendo una a caso tra quelle espandibili */
                        expandableRooms = GetExpandableRooms();
                        toAddTo = expandableRooms[random.Next(0, expandableRooms.Count)];
                    }

                    /* Ciclo lungo tutti i lati */
                    for (int sideIndex=Consts.NORTH; sideIndex <= Consts.WEST; sideIndex++)
                    {
                        /* Provo ad aggiungere i corridoi */
                        for (int corridorIndex=0; corridorIndex < maxCorridorsPerSide; corridorIndex++)
                        {
                            /* Angoli della stanza */
                            List<Corner> currentRoomCorners = toAddTo.GetCorners();
                            /* Corridoio da aggiungere poi alla lista */
                            Corridor toAddCorridor = null;
                            /* Stanza da aggiungere poi alla lista */
                            Room toAddRoom = null;
                            /* Larghezza e altezza del corridoio */
                            /* ATTENZIONE! Si suppone che l'altezza sia il lato più corto, la larghezza quello più lungo:
                             * in caso di corridoi attaccati al lato destro o sinistro della stanza, altezza e larghezza 
                             * saranno scambiati, così che il lato corto sia sempre quello attaccato al lato della stanza
                             */
                            int corridorWidth = random.Next(minCorridorWidth, maxCorridorWidth);
                            int corridorHeight = random.Next(minCorridorHeight, maxCorridorHeight);
                            /* Larghezza e altezza della stanza */
                            int roomWidth = random.Next(minRoomWidth, maxRoomWidth);
                            int roomHeight = random.Next(minRoomHeight, maxRoomHeight);

                            /* Se posso aggiungere un corridoio */
                            if (random.Next(0, 100) >= corridorProbability)
                            {
                                toAddCorridor = GenerateCorridor(sideIndex, toAddTo, corridorHeight, corridorWidth);

                                if (toAddCorridor != null)
                                {
                                    toAddRoom = GenerateRoom(sideIndex, toAddCorridor, roomWidth, roomHeight);
                                }

                                if (toAddRoom != null && toAddCorridor != null)
                                {
                                    /* Aggiungo il corridoio alla stanza */
                                    toAddRoom.AddCorridor(toAddCorridor);
                                    /* Aggiungo sia corridoio che stanza alla lista dei corridoi e delle stanze */
                                    this.corridors.Add(toAddCorridor);
                                    this.rooms.Add(toAddRoom);
                                }
                            }
                        }
                    }
                }
            }

            FillMatrix();
            ExportBitmap();
        }

        private Corridor GenerateCorridor(int sideIndex, Room toAddTo, int corridorHeight, int corridorWidth)
        {
            Console.WriteLine("Aggiungo corridoio");
            List<Corner> currentRoomCorners = toAddTo.GetCorners();
            Corner corridorTopLeftCorner;
            Corridor toAddCorridor = null;
            Random random = new Random();
            /* A seconda del lato in cui mi trovo, devo posizionare in modo differente il corridoio e la nuova stanza */
            switch (sideIndex)
            {
                case Consts.NORTH:
                    /* La y del corridoio è la stessa della cima della stanza, la x è a caso
                     * tra la minima e la massima del lato: alla massima va tolta la larghezza del corridoio */

                    if (!toAddTo.hasOnTop)
                    {
                        corridorTopLeftCorner = new Corner(
                            random.Next(currentRoomCorners[Consts.TOP_LEFT].GetX(),
                                        currentRoomCorners[Consts.TOP_RIGHT].GetX() - corridorHeight),
                            currentRoomCorners[Consts.TOP_LEFT].GetY() + corridorWidth);

                        /* Instanzio il corridoio */
                        toAddCorridor = new Corridor(corridorHeight, corridorWidth, corridorTopLeftCorner);

                        toAddTo.hasOnTop = true;
                    }
                    break;
                case Consts.EAST:
                    if (!toAddTo.hasOnRight)
                    {
                        /* La x del corridoio è quella dell'angolo destro della stanza, la y è a caso tra la minima
                         * e la massima del lato: alla massima va aggiunta l'altezza del corridoio */
                        corridorTopLeftCorner = new Corner(
                            currentRoomCorners[Consts.TOP_RIGHT].GetX(),
                            random.Next(currentRoomCorners[Consts.BOTTOM_RIGHT].GetY() + corridorHeight,
                                        currentRoomCorners[Consts.TOP_RIGHT].GetY()));

                        /* Instanzio il corridoio */
                        toAddCorridor = new Corridor(corridorWidth, corridorHeight, corridorTopLeftCorner);
                        toAddTo.hasOnRight = true;
                    }
                    break;
                case Consts.SOUTH:
                    if (!toAddTo.hasOnBottom)
                    {
                        /* La y del corridoio è la stessa del fondo della stanza, la x è a caso
                         * tra la minima e la massima del lato: alla massima va tolta la larghezza del corridoio */
                        corridorTopLeftCorner = new Corner(
                            random.Next(currentRoomCorners[Consts.BOTTOM_LEFT].GetX(),
                                        currentRoomCorners[Consts.BOTTOM_RIGHT].GetX() - corridorHeight),
                            currentRoomCorners[Consts.BOTTOM_LEFT].GetY());

                        /* Instanzio il corridoio */
                        toAddCorridor = new Corridor(corridorHeight, corridorWidth, corridorTopLeftCorner);
                        toAddTo.hasOnBottom = true;

                    }
                    break;
                case Consts.WEST:
                    if (!toAddTo.hasOnLeft)
                    {
                        /* La x del corridoio è quella dell'angolo sinistro della stanza più la larghezza, la y è a caso 
                         * tra la minima e la massima del lato: alla massima va aggiunta l'altezza del corridoio */
                        corridorTopLeftCorner = new Corner(
                            currentRoomCorners[Consts.TOP_LEFT].GetX() - corridorWidth,
                            random.Next(currentRoomCorners[Consts.BOTTOM_LEFT].GetY() + corridorHeight,
                                        currentRoomCorners[Consts.TOP_LEFT].GetY()));

                        /* Instanzio il corridoio */
                        toAddCorridor = new Corridor(corridorWidth, corridorHeight, corridorTopLeftCorner);
                        toAddTo.hasOnLeft = true;
                    }
                    break;
                default:
                    Console.WriteLine("INDICE DEL LATO SCONOSCIUTO (" + sideIndex + ")");
                    toAddCorridor = new Corridor(-1, -1, new Corner(-1, -1));
                    break;
            }

            return toAddCorridor;
        }

        private Room GenerateRoom(int sideIndex, Corridor toAddCorridor, int roomWidth, int roomHeight)
        {
            Room toAddRoom;
            List<Corner> corridorCorners = toAddCorridor.GetCorners();
            Corner roomTopLeftCorner;

            Console.WriteLine("Aggiungo stanza");

            switch (sideIndex)
            {
                case Consts.NORTH:
                    /* Sulla base del corridoio appena creato, genero la relativa stanza */
                    /* Mi servono prima gli angoli del corriodoio */
                    corridorCorners = toAddCorridor.GetCorners();
                    roomTopLeftCorner = new Corner(
                                 /* La x è data dalla x del punto medio della larghezza del corridoio 
                                  * più metà della larghezza della stanza */
                                 (corridorCorners[Consts.TOP_LEFT].GetX() + corridorCorners[Consts.TOP_RIGHT].GetX()) / 2 -
                                 roomWidth / 2,
                                 /* La y, invece, è data dalla y del lato nord del corridoio più l'altezza della stanza */
                                 corridorCorners[Consts.TOP_LEFT].GetY() + roomHeight
                            );
                    /* Ora che ho l'angolo posso generare la stanza */
                    toAddRoom = new Room(roomWidth, roomHeight, roomTopLeftCorner);

                    toAddRoom.hasOnBottom = true;
                    break;
                case Consts.EAST:
                    /* Sulla base del corridoio appena creato, genero la relativa stanza */
                    /* Mi servono prima gli angoli del corriodoio */
                    corridorCorners = toAddCorridor.GetCorners();

                    roomTopLeftCorner = new Corner(
                             /* La x è data dalla x dell'angolo destro del corridoio */
                             corridorCorners[Consts.TOP_RIGHT].GetX(),
                             /* La y, invece, è la metà dell'altezza della stanza più metà della y del
                              * punto medio dell'altezza del corridoio */
                             (corridorCorners[Consts.TOP_RIGHT].GetY() + corridorCorners[Consts.BOTTOM_RIGHT].GetY()) / 2 +
                             roomHeight / 2
                        );
                    /* Ora che ho l'angolo posso generare la stanza */
                    toAddRoom = new Room(roomWidth, roomHeight, roomTopLeftCorner);

                    toAddRoom.hasOnLeft = true;

                    break;
                case Consts.SOUTH:
                    /* Sulla base del corridoio appena creato, genero la relativa stanza */
                    /* Mi servono prima gli angoli del corriodoio */
                    corridorCorners = toAddCorridor.GetCorners();

                    roomTopLeftCorner = new Corner(
                             /* La x è data dalla x del punto medio della larghezza del corridoio 
                              * più metà della larghezza della stanza */
                             (corridorCorners[Consts.TOP_LEFT].GetX() + corridorCorners[Consts.TOP_RIGHT].GetX()) / 2 -
                             roomWidth / 2,
                             /* La y, invece, è data dalla y del lato sud del corridoio */
                             corridorCorners[Consts.BOTTOM_RIGHT].GetY()
                        );
                    /* Ora che ho l'angolo posso generare la stanza */
                    toAddRoom = new Room(roomWidth, roomHeight, roomTopLeftCorner);

                    toAddRoom.hasOnTop = true;

                    break;
                case Consts.WEST:

                    /* Sulla base del corridoio appena creato, genero la relativa stanza */
                    /* Mi servono prima gli angoli del corriodoio */
                    corridorCorners = toAddCorridor.GetCorners();

                    roomTopLeftCorner = new Corner(
                             /* La x è data dalla x dell'angolo destro del corridoio */
                             corridorCorners[Consts.TOP_LEFT].GetX() - roomWidth,
                             /* La y, invece, è la metà dell'altezza della stanza più metà della y del
                              * punto medio dell'altezza del corridoio */
                             (corridorCorners[Consts.TOP_RIGHT].GetY() + corridorCorners[Consts.BOTTOM_RIGHT].GetY()) / 2 +
                             roomHeight / 2
                        );
                    /* Ora che ho l'angolo posso generare la stanza */
                    toAddRoom = new Room(roomWidth, roomHeight, roomTopLeftCorner);

                    toAddRoom.hasOnRight = true;

                    break;
                default:
                    toAddRoom = new Room(-1, -1, new Corner(-1, -1));
                    break;
            }

            return toAddRoom;
        }

        private void FillMatrix()
        {
            for (int i = 0; i < corridors.Count; i++)
            {
                corridors[i].AddToMatrix(level);
                Console.WriteLine(i + ") Aggiungo corridoio alla matrice");
            }

            for (int i=0; i<rooms.Count; i++)
            {
                rooms[i].AddToMatrix(level);
                Console.WriteLine(i + ") Aggiungo stanza alla matrice");
            }

            
        }

        private void ExportBitmap()
        {
            for (int i=0; i<Consts.MAX_LEVEL_WIDTH; i++)
            {
                for (int j=0; j<Consts.MAX_LEVEL_HEIGHT; j++)
                {
                    if (level[i][j] == 1)
                    {
                        bitmap.SetPixel(i, j, foregroundColor);
                    }
                    else if (level[i][j] == 2)
                    {
                        bitmap.SetPixel(i, j, Color.FromArgb(255, 255, 0, 0));
                    }
                    else 
                    {
                        bitmap.SetPixel(i, j, backgroundColor);
                    }
                }
            }

            Console.WriteLine("Finito, salvo su file...");
            bitmap.Save("generated.bmp");
        }

        public Room GetFirstExpandableRoom()
        {
            for (int i=0; i<rooms.Count; i++)
            {
                if (rooms[i].GetNCorridors() < maxCorridorsPerSide * Consts.SIDE_COUNT && rooms[i].GetNCorridors() < rooms[i].GetMaxCorridors())
                {
                    return rooms[i];
                }
            }

            return null;
        }

        public List<Room> GetExpandableRooms()
        {
            List<Room> ret = new List<Room>();

            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].GetNCorridors() < maxCorridorsPerSide * Consts.SIDE_COUNT)
                {
                    ret.Add(rooms[i]);
                }
            }

            return ret;
        }
    }
}
