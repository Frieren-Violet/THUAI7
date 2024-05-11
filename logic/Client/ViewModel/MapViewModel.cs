﻿using Client.Model;
using Client.Util;
using Protobuf;
using System.Collections.ObjectModel;


namespace Client.ViewModel
{
    public partial class GeneralViewModel : BindableObject
    {

        private List<MessageOfAll> listOfAll;
        private List<MessageOfShip> listOfShip;
        private List<MessageOfBullet> listOfBullet;
        private List<MessageOfBombedBullet> listOfBombedBullet;
        private List<MessageOfFactory> listOfFactory;
        private List<MessageOfFort> listOfFort;
        private List<MessageOfCommunity> listOfCommunity;
        private List<MessageOfWormhole> listOfWormhole;
        private List<MessageOfResource> listOfResource;
        private List<MessageOfHome> listOfHome;

        /* initiate the Lists of Objects and CountList */
        private void InitiateObjects()
        {
            listOfAll = [];
            listOfShip = []; ;
            listOfBullet = [];
            listOfBombedBullet = [];
            listOfFactory = [];
            listOfCommunity = [];
            listOfFort = [];
            listOfResource = [];
            listOfHome = [];
            listOfWormhole = [];
            countMap = [];
        }
        private (int x, int y)[] resourcePositionIndex;
        private (int x, int y)[] FactoryPositionIndex;
        private (int x, int y)[] CommunityPositionIndex;
        private (int x, int y)[] FortPositionIndex;
        private (int x, int y)[] wormHolePositionIndex;
        private Dictionary<int, int> countMap;


        private int[,] defaultMap;
        ///* Get the Map to default map */
        private void GetMap(MessageOfMap obj)
        {
            int[,] map = new int[50, 50];
            try
            {
                for (int i = 0; i < 50; i++)
                {
                    for (int j = 0; j < 50; j++)
                    {
                        switch (obj.Rows[i].Cols[j])
                        {
                            case PlaceType.NullPlaceType:
                                map[i, j] = (int)MapPatchType.Null;
                                break;
                            case PlaceType.Space:
                                map[i, j] = (int)MapPatchType.Space;
                                break;
                            case PlaceType.Ruin:
                                map[i, j] = (int)MapPatchType.Ruin;
                                break;
                            case PlaceType.Shadow:
                                map[i, j] = (int)MapPatchType.Shadow;
                                break;
                            case PlaceType.Asteroid:
                                map[i, j] = (int)MapPatchType.Asteroid;
                                break;
                            case PlaceType.Resource:
                                map[i, j] = (int)MapPatchType.Resource;
                                break;
                            case PlaceType.Construction:
                                map[i, j] = (int)MapPatchType.Factory;
                                break;
                            case PlaceType.Wormhole:
                                map[i, j] = (int)MapPatchType.WormHole;
                                break;
                            case PlaceType.Home:
                                map[i, j] = (int)MapPatchType.RedHome;
                                break;
                            default:
                                map[i, j] = (int)MapPatchType.Null;
                                break;
                        }
                    }
                }
            }
            catch
            {
                getMapFlag = false;
            }
            finally
            {
                defaultMap = map;
                getMapFlag = true;
            }
        }

        //public class XY
        //{
        //    volatile int x = 10;
        //    volatile int y = 10;
        //    public int X 
        //    {
        //        get => Interlocked.CompareExchange(ref x,-1,-1); 
        //        set => Interlocked.Exchange(ref x, value); 
        //    }
        //    public int Y 
        //    {
        //        get => Interlocked.CompareExchange(ref y,-1,-1); 
        //        set => Interlocked.Exchange(ref y, value);
        //    } 
        //    public void AddX(int value) => Interlocked.Add(ref x, value);
        //    public void AddY(int value) => Interlocked.Add(ref y, value);
        //}

        //private XY ballxy = new XY();

        //public void Draw(ICanvas canvas, RectF dirtyRect)
        //{
        //    lock (drawPicLock)
        //    {
        //        myLogger.LogInfo(String.Format("Draw--cou:{0}, coud{1}", cou, Countdow));

        //        myLogger.LogInfo("Draw");
        //        canvas.FillColor = Colors.Red;

        //        // 绘制小球
        //        ballx = ballx_receive;
        //        bally = bally_receive;
        //        canvas.FillEllipse(ballx, bally, 20, 20);
        //        myLogger.LogInfo(String.Format("============= Draw: ballX:{0}, ballY:{1} ================", ballx, bally));
        //        myLogger.LogInfo(String.Format("============= Draw Receive: ballX:{0}, ballY:{1} ================", ballx_receive, bally_receive));

        //        DrawBullet(new MessageOfBullet
        //        {
        //            X = 10,
        //            Y = 10,
        //            Type = BulletType.NullBulletType,
        //            BombRange = 5
        //        }, canvas);

        //        DrawShip(new MessageOfShip
        //        {
        //            X = 10,
        //            Y = 11,
        //            Hp = 100,
        //            TeamId = 0
        //        }, canvas);

        //        DrawBullet(new MessageOfBullet
        //        {
        //            X = 9,
        //            Y = 11,
        //            Type = BulletType.NullBulletType,
        //            BombRange = 5
        //        }, canvas);

        //        DrawShip(new MessageOfShip
        //        {
        //            X = 10,
        //            Y = 12,
        //            Hp = 100,
        //            TeamId = 1
        //        }, canvas);

        //        listOfBullet.Add(new MessageOfBullet
        //        {
        //            X = 20,
        //            Y = 20,
        //            Type = BulletType.NullBulletType,
        //            BombRange = 5
        //        });

        //        listOfShip.Add(new MessageOfShip
        //        {
        //            X = 10,
        //            Y = 12,
        //            Hp = 100,
        //            TeamId = 1
        //        });

        //        if (listOfBullet.Count > 0)
        //        {
        //            foreach (var data in listOfBullet)
        //            {
        //                DrawBullet(data, canvas);
        //            }
        //        }

        //        if (listOfBullet.Count > 0)
        //        {
        //            foreach (var data in listOfShip)
        //            {
        //                DrawShip(data, canvas);
        //            }
        //        }
        //    }
        //}

        private readonly Dictionary<MapPatchType, Color> PatchColorDict = new()
        {
            {MapPatchType.RedHome, Color.FromRgb(237, 49, 47)},
            {MapPatchType.BlueHome, Colors.Blue},
            {MapPatchType.Ruin, Color.FromRgb(181, 122, 88)},
            {MapPatchType.Shadow, Color.FromRgb(73, 177, 82)},
            {MapPatchType.Asteroid, Color.FromRgb(164, 217, 235)},
            {MapPatchType.Resource, Color.FromRgb(160, 75, 166)},
            {MapPatchType.Factory, Color.FromRgb(231, 144, 74)},
            {MapPatchType.Community, Color.FromRgb(131, 144, 74)},
            {MapPatchType.Fort, Color.FromRgb(131, 34, 74)},
            {MapPatchType.WormHole, Color.FromRgb(137, 17, 26)},
            {MapPatchType.Space, Color.FromRgb(255, 255, 255)},
            {MapPatchType.Null, Color.FromRgb(0,0,0)}
        };

        private void PureDrawMap(int[,] Map)
        {
            lock (drawPicLock)
            {
                for (int i = 0; i < 50; i++)
                {
                    for (int j = 0; j < 50; j++)
                    {
                        switch ((MapPatchType)Map[i, j])
                        {
                            case MapPatchType.RedHome:
                                MapPatchesList[UtilFunctions.getCellIndex(i, j)].PatchColor = Color.FromRgb(237, 49, 47); break;  //Red Home
                            case MapPatchType.BlueHome:
                                MapPatchesList[UtilFunctions.getCellIndex(i, j)].PatchColor = Colors.Blue; break; //Blue Home
                            case MapPatchType.Ruin:
                                MapPatchesList[UtilFunctions.getCellIndex(i, j)].PatchColor = Color.FromRgb(181, 122, 88); break; // Ruin
                            case MapPatchType.Shadow:
                                MapPatchesList[UtilFunctions.getCellIndex(i, j)].PatchColor = Color.FromRgb(73, 177, 82); break; // Grass
                            case MapPatchType.Asteroid:
                                MapPatchesList[UtilFunctions.getCellIndex(i, j)].PatchColor = Color.FromRgb(164, 217, 235); break; // River
                            case MapPatchType.Resource:
                                MapPatchesList[UtilFunctions.getCellIndex(i, j)].PatchColor = Color.FromRgb(160, 75, 166); break; //Resource
                            case MapPatchType.Factory:
                                MapPatchesList[UtilFunctions.getCellIndex(i, j)].PatchColor = Color.FromRgb(231, 144, 74); break; //RecycleBank
                            case MapPatchType.Community:
                                MapPatchesList[UtilFunctions.getCellIndex(i, j)].PatchColor = Color.FromRgb(231, 144, 74); break; //ChargeStation
                            case MapPatchType.Fort:
                                MapPatchesList[UtilFunctions.getCellIndex(i, j)].PatchColor = Color.FromRgb(231, 144, 74); break; //SignalTower
                            case MapPatchType.Space:
                                MapPatchesList[UtilFunctions.getCellIndex(i, j)].PatchColor = Color.FromRgb(255, 255, 255); break; //SignalTower
                            case MapPatchType.WormHole:
                                MapPatchesList[UtilFunctions.getCellIndex(i, j)].PatchColor = Color.FromRgb(137, 17, 26); break; //SignalTower
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private void DrawShip()
        {
            for (int i = 0; i < ShipCircList.Count; i++)
            {
                //ShipCircList[i].Color = Colors.Transparent;
                ShipCircList[i].X = 51;
                ShipCircList[i].Y = 51;
                ShipCircList[i].Text = "";
            }
            myLogger.LogInfo(String.Format("listOfShip.Count:{0}", listOfShip.Count));
            myLogger.LogInfo(String.Format("ShipCircList.Count:{0}", ShipCircList.Count));
            for (int i = 0; i < listOfShip.Count; i++)
            {
                MessageOfShip data = listOfShip[i];
                //DrawCircLabel shipinfo = ShipCircList[i];
                PointF point = UtilFunctions.Grid2CellPoint(data.X, data.Y);
                ShipCircList[i].X = point.X;
                ShipCircList[i].Y = point.Y;
                myLogger.LogInfo(String.Format("shipinfo.X:{0}", ShipCircList[i].X));
                myLogger.LogInfo(String.Format("shipinfo.Y:{0}", ShipCircList[i].Y));
                long team_id = data.TeamId;
                myLogger.LogInfo(String.Format("shipTeamid{0}", team_id));
                switch (team_id)
                {
                    case 0:
                        myLogger.LogInfo("shipinfo.color = red");
                        ShipCircList[i].Color = Colors.DarkRed;
                        break;

                    case 1:
                        myLogger.LogInfo("shipinfo.color = blue");

                        ShipCircList[i].Color = Colors.DarkBlue;
                        break;

                    default:
                        myLogger.LogInfo("shipinfo.color = black");

                        ShipCircList[i].Color = Colors.DarkGreen;
                        break;
                }
                //shipinfo.Radius = 4.5F;
                //shipinfo.FontSize = 5.5F;
                //shipinfo.TextColor = Colors.White;
                //ShipCircList.Add(shipinfo);
            }
            //for (int i = 0; i < ShipCircList.Count; i++)
            //{
            //    myLogger.LogInfo(String.Format("DrawnShip{0}.X:{1}", i, ShipCircList[i].X));
            //    myLogger.LogInfo(String.Format("DrawnShip{0}.Y:{1}", i, ShipCircList[i].Y));
            //    myLogger.LogInfo(String.Format("DrawnShip{0}.Color:{1}", i, ShipCircList[i].Color));
            //}
        }

        private void DrawBullet()
        {
            for (int i = 0; i < BulletCircList.Count; i++)
            {
                //BulletCircList[i].Color = Colors.Transparent;
                BulletCircList[i].X = 51;
                BulletCircList[i].Y = 51;
                BulletCircList[i].Text = "";
            }
            myLogger.LogInfo(String.Format("listOfBullet.Count:{0}", listOfBullet.Count));
            myLogger.LogInfo(String.Format("BulletCircList.Count:{0}", BulletCircList.Count));
            for (int i = 0; i < listOfBullet.Count; i++)
            {
                MessageOfBullet data = listOfBullet[i];
                DrawCircLabel bulletinfo = BulletCircList[i];
                PointF point = UtilFunctions.Grid2CellPoint(data.X, data.Y);
                bulletinfo.X = point.X;
                bulletinfo.Y = point.Y;
                long team_id = data.TeamId;
                myLogger.LogInfo(String.Format("bulletinfo.X:{0}", bulletinfo.X));
                myLogger.LogInfo(String.Format("bulletinfo.Y:{0}", bulletinfo.Y));
                //myLogger.LogInfo(String.Format("Bullet{0}.Teamid:{1}", i, data.TeamId));
                switch (team_id)
                {
                    case 0:
                        myLogger.LogInfo("bulletinfo.color = red");
                        bulletinfo.Color = Colors.DarkRed;
                        break;

                    case 1:
                        myLogger.LogInfo("bulletinfo.color = blue");
                        bulletinfo.Color = Colors.DarkBlue;
                        break;

                    default:
                        myLogger.LogInfo("bulletinfo.color = black");
                        bulletinfo.Color = Colors.DarkGreen;
                        break;
                }
                //shipinfo.Radius = 4.5F;
                //shipinfo.FontSize = 5.5F;
                //shipinfo.TextColor = Colors.White;
                //ShipCircList.Add(shipinfo);
            }
        }


        //private void DrawMap()
        //{
        //    //resourceArray = new Label[countMap[(int)MapPatchType.Resource]];
        //    resourcePositionIndex = new (int x, int y)[countMap[(int)MapPatchType.Resource]];
        //    //FactoryArray = new Label[countMap[(int)MapPatchType.Factory]];
        //    FactoryPositionIndex = new (int x, int y)[countMap[(int)MapPatchType.Factory]];
        //    //CommunityArray = new Label[countMap[(int)MapPatchType.Community]];
        //    CommunityPositionIndex = new (int x, int y)[countMap[(int)MapPatchType.Community]];
        //    //FortArray = new Label[countMap[(int)MapPatchType.Fort]];
        //    FortPositionIndex = new (int x, int y)[countMap[(int)MapPatchType.Fort]];


        //    int counterOfResource = 0;
        //    int counterOfFactory = 0;
        //    int counterOfCommunity = 0;
        //    int counterOfFort = 0;


        //    int[,] todrawMap;
        //    todrawMap = defaultMap;
        //    for (int i = 0; i < todrawMap.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < todrawMap.GetLength(1); j++)
        //        {
        //            MapPatchType mapPatchType = (MapPatchType)todrawMap[i, j];
        //            switch (mapPatchType)
        //            {
        //                case MapPatchType.RedHome:
        //                    mapPatches[i, j].PatchColor = Colors.Red; break;  //Red Home
        //                case MapPatchType.BlueHome:
        //                    mapPatches[i, j].PatchColor = Colors.Blue; break; //Blue Home
        //                case MapPatchType.Ruin:
        //                    mapPatches[i, j].PatchColor = Colors.Black; break; // Ruin
        //                case MapPatchType.Shadow:
        //                    mapPatches[i, j].PatchColor = Colors.Gray; break; // Shadow
        //                case MapPatchType.Asteroid:
        //                    mapPatches[i, j].PatchColor = Colors.Brown; break; // Asteroid
        //                case MapPatchType.Resource:
        //                    mapPatches[i, j].PatchColor = Colors.Yellow; //Resource
        //                    resourcePositionIndex[counterOfResource] = (i, j);
        //                    mapPatches[i, j].Text = "R";
        //                    //resourceArray[counterOfResource] = new Label()
        //                    //{
        //                    //    FontSize = unitFontSize,
        //                    //    WidthRequest = unitWidth,
        //                    //    HeightRequest = unitHeight,
        //                    //    Text = Convert.ToString(-1),
        //                    //    HorizontalOptions = LayoutOptions.Start,
        //                    //    VerticalOptions = LayoutOptions.Start,
        //                    //    HorizontalTextAlignment = TextAlignment.Center,
        //                    //    VerticalTextAlignment = TextAlignment.Center,
        //                    //    BackgroundColor = Colors.Transparent
        //                    //};
        //                    counterOfResource++;
        //                    break;

        //                case MapPatchType.Factory:
        //                    mapPatches[i, j].PatchColor = Colors.Orange; //Factory
        //                    FactoryPositionIndex[counterOfFactory] = (i, j);
        //                    mapPatches[i, j].Text = "F";
        //                    //FactoryArray[counterOfFactory] = new Label()
        //                    //{
        //                    //    FontSize = unitFontSize,
        //                    //    WidthRequest = unitWidth,
        //                    //    HeightRequest = unitHeight,
        //                    //    Text = Convert.ToString(100),
        //                    //    HorizontalOptions = LayoutOptions.Start,
        //                    //    VerticalOptions = LayoutOptions.Start,
        //                    //    HorizontalTextAlignment = TextAlignment.Center,
        //                    //    VerticalTextAlignment = TextAlignment.Center,
        //                    //    BackgroundColor = Colors.Transparent
        //                    //};
        //                    counterOfFactory++;
        //                    break;

        //                case MapPatchType.Community:
        //                    mapPatches[i, j].PatchColor = Colors.Chocolate; //Community
        //                    CommunityPositionIndex[counterOfCommunity] = (i, j);
        //                    mapPatches[i, j].Text = "C";
        //                    //FactoryArray[counterOfCommunity] = new Label()
        //                    //{
        //                    //    FontSize = unitFontSize,
        //                    //    WidthRequest = unitWidth,
        //                    //    HeightRequest = unitHeight,
        //                    //    Text = Convert.ToString(100),
        //                    //    HorizontalOptions = LayoutOptions.Start,
        //                    //    VerticalOptions = LayoutOptions.Start,
        //                    //    HorizontalTextAlignment = TextAlignment.Center,
        //                    //    VerticalTextAlignment = TextAlignment.Center,
        //                    //    BackgroundColor = Colors.Transparent
        //                    //};
        //                    counterOfCommunity++;
        //                    break;

        //                case MapPatchType.Fort:
        //                    mapPatches[i, j].PatchColor = Colors.Azure; //Fort
        //                    FortPositionIndex[counterOfFort] = (i, j);
        //                    mapPatches[i, j].Text = "Fo";
        //                    //FortArray[counterOfFort] = new Label()
        //                    //{
        //                    //    FontSize = unitFontSize,
        //                    //    WidthRequest = unitWidth,
        //                    //    HeightRequest = unitHeight,
        //                    //    Text = Convert.ToString(-1),
        //                    //    HorizontalOptions = LayoutOptions.Start,
        //                    //    VerticalOptions = LayoutOptions.Start,
        //                    //    HorizontalTextAlignment = TextAlignment.Center,
        //                    //    VerticalTextAlignment = TextAlignment.Center,
        //                    //    BackgroundColor = Colors.Transparent
        //                    //};
        //                    counterOfFort++;
        //                    break;

        //                default:
        //                    break;
        //            }
        //            //MapGrid.Children.Add(mapPatches[i, j]);
        //        }
        //    }
        //}



        //private int FindIndexOfResource(MessageOfResource obj)
        //{
        //    for (int i = 0; i < listOfResource.Count; i++)
        //    {
        //        if (resourcePositionIndex[i].x == obj.X && resourcePositionIndex[i].y == obj.Y)
        //        {
        //            return i;
        //        }
        //    }
        //    return -1;
        //}

        //private int FindIndexOfFactory(MessageOfFactory obj)
        //{
        //    for (int i = 0; i < listOfFactory.Count; i++)
        //    {
        //        if (FactoryPositionIndex[i].x == obj.X && FactoryPositionIndex[i].y == obj.Y)
        //        {
        //            return i;
        //        }
        //    }
        //    return -1;
        //}

        //private int FindIndexOfCommunity(MessageOfCommunity obj)
        //{
        //    for (int i = 0; i < listOfCommunity.Count; i++)
        //    {
        //        if (CommunityPositionIndex[i].x == obj.X && CommunityPositionIndex[i].y == obj.Y)
        //        {
        //            return i;
        //        }
        //    }
        //    return -1;
        //}

        //private int FindIndexOfFort(MessgaeOfFort obj)
        //{
        //    for (int i = 0; i < listOfFort.Count; i++)
        //    {
        //        if (FortPositionIndex[i].x == obj.X && FortPositionIndex[i].y == obj.Y)
        //        {
        //            return i;
        //        }
        //    }
        //    return -1;
        //}



        private void DrawHome(MessageOfHome data)
        {
            int x = data.X / 1000;
            int y = data.Y / 1000;
            int hp = data.Hp;
            long team_id = data.TeamId;
            int index = UtilFunctions.getCellIndex(x, y);
            myLogger.LogInfo(String.Format("Draw Home index: {0}", index));

            MapPatchesList[index].Text = Convert.ToString(hp);
            switch (team_id)
            {
                // case (long)PlayerTeam.Red:
                case 0:
                    MapPatchesList[index].PatchColor = PatchColorDict[MapPatchType.RedHome];
                    MapPatchesList[index].TextColor = Colors.White;
                    break;

                // case (long)PlayerTeam.Blue:
                case 1:
                    MapPatchesList[index].PatchColor = PatchColorDict[MapPatchType.BlueHome];
                    MapPatchesList[index].TextColor = Colors.White;
                    break;

                default:
                    MapPatchesList[index].PatchColor = Colors.Black;
                    MapPatchesList[index].TextColor = Colors.White;
                    break;
            }
        }

        private void DrawFactory(MessageOfFactory data)
        {
            int x = data.X;
            int y = data.Y;
            int hp = data.Hp;
            long team_id = data.TeamId;
            int index = UtilFunctions.getGridIndex(x, y);
            MapPatchesList[index].Text = Convert.ToString(hp);
            switch (team_id)
            {
                // case (long)PlayerTeam.Red:
                case 0:
                    MapPatchesList[index].PatchColor = PatchColorDict[MapPatchType.Factory];
                    MapPatchesList[index].TextColor = Colors.Red;
                    break;

                // case (long)PlayerTeam.Blue:
                case 1:
                    MapPatchesList[index].PatchColor = PatchColorDict[MapPatchType.Factory];
                    MapPatchesList[index].TextColor = Colors.Blue;
                    break;

                default:
                    MapPatchesList[index].PatchColor = Colors.Black;
                    MapPatchesList[index].TextColor = Colors.White;
                    break;
            }
        }

        private void DrawCommunity(MessageOfCommunity data)
        {
            int x = data.X;
            int y = data.Y;
            int hp = data.Hp;
            long team_id = data.TeamId;
            int index = UtilFunctions.getGridIndex(x, y);
            MapPatchesList[index].Text = Convert.ToString(hp);
            switch (team_id)
            {
                // case (long)PlayerTeam.Red:
                case 0:
                    MapPatchesList[index].PatchColor = PatchColorDict[MapPatchType.Community];
                    MapPatchesList[index].TextColor = Colors.Red;
                    break;

                // case (long)PlayerTeam.Blue:
                case 1:
                    MapPatchesList[index].PatchColor = PatchColorDict[MapPatchType.Community];
                    MapPatchesList[index].TextColor = Colors.Blue;
                    break;

                default:
                    MapPatchesList[index].PatchColor = Colors.Black;
                    MapPatchesList[index].TextColor = Colors.White;
                    break;
            }
        }

        private void DrawFort(MessageOfFort data)
        {
            int x = data.X;
            int y = data.Y;
            int hp = data.Hp;
            long team_id = data.TeamId;
            int index = UtilFunctions.getGridIndex(x, y);
            MapPatchesList[index].Text = Convert.ToString(hp);
            switch (team_id)
            {
                // case (long)PlayerTeam.Red:
                case 0:
                    MapPatchesList[index].PatchColor = PatchColorDict[MapPatchType.Fort];
                    MapPatchesList[index].TextColor = Colors.Red;
                    break;

                // case (long)PlayerTeam.Blue:
                case 1:
                    MapPatchesList[index].PatchColor = PatchColorDict[MapPatchType.Fort];
                    MapPatchesList[index].TextColor = Colors.Blue;
                    break;

                default:
                    MapPatchesList[index].PatchColor = Colors.Black;
                    MapPatchesList[index].TextColor = Colors.White;
                    break;
            }
        }

        private void DrawWormHole(MessageOfWormhole data)
        {
            int x = data.X;
            int y = data.Y;
            int hp = data.Hp;
            int index = UtilFunctions.getGridIndex(x, y);
            MapPatchesList[index].Text = Convert.ToString(hp);
            MapPatchesList[index].PatchColor = PatchColorDict[MapPatchType.WormHole];
            MapPatchesList[index].TextColor = Colors.White;
            int id = data.Id;
            switch (id)
            {
                case 0:
                    WormHole1HP = hp;
                    break;
                case 1:
                    WormHole2HP = hp;
                    break;
                case 2:
                    WormHole3HP = hp;
                    break;
            }
        }

        private void DrawResource(MessageOfResource data)
        {
            int x = data.X;
            int y = data.Y;
            int hp = data.Progress;
            int index = UtilFunctions.getGridIndex(x, y);
            MapPatchesList[index].Text = Convert.ToString(hp);
            MapPatchesList[index].PatchColor = PatchColorDict[MapPatchType.Resource];
            MapPatchesList[index].TextColor = Colors.White;
        }

        private bool isClientStocked = false;
        private bool hasDrawn = false;
        private bool getMapFlag = false;
        public readonly object drawPicLock = new();
        //private bool isPlaybackMode;
        //private double unit;
        //private double unitFontSize = 10;
        //private double unitHeight = 10.6;
        //private double unitWidth = 10.6;



        public ObservableCollection<MapPatch> mapPatchesList;
        public ObservableCollection<MapPatch> MapPatchesList
        {
            get
            {
                return mapPatchesList ??= [];
            }
            set
            {
                mapPatchesList = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DrawCircLabel> shipCircList;
        public ObservableCollection<DrawCircLabel> ShipCircList
        {
            get
            {
                return shipCircList ??= [];
            }
            set
            {
                shipCircList = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DrawCircLabel> bulletCircList;
        public ObservableCollection<DrawCircLabel> BulletCircList
        {
            get
            {
                return bulletCircList ??= [];
            }
            set
            {
                bulletCircList = value;
                OnPropertyChanged();
            }
        }





        //private MapPatch[] mapPatches_ = new MapPatch[2500];
        //public MapPatch[] MapPatches_
        //{
        //    get
        //    {
        //        return mapPatches_ ??= new MapPatch[2500];
        //    }
        //    set
        //    {
        //        mapPatches_ = value;
        //        OnPropertyChanged();
        //    }
        //}

        private MapPatch[,] mapPatches_ = new MapPatch[50, 50];
        public MapPatch[,] MapPatches_
        {
            get
            {
                return mapPatches_;
            }
            set
            {
                mapPatches_ = value;
                OnPropertyChanged();
            }
        }

        //private readonly BoxView[,] mapPatches = new BoxView[50, 50];
        private readonly double characterRadiusTimes = 400;
        private readonly double bulletRadiusTimes = 200;

        private int mapHeight = 500;
        public int MapHeight
        {
            get
            {
                return mapHeight;
            }
            set
            {
                mapHeight = value;
                OnPropertyChanged();
            }
        }

        private int mapWidth = 500;
        public int MapWidth
        {
            get
            {
                return mapWidth;
            }
            set
            {
                mapWidth = value;
                OnPropertyChanged();
            }
        }

        public Command MoveUpCommand { get; }
        public Command MoveDownCommand { get; }
        public Command MoveLeftCommand { get; }
        public Command MoveRightCommand { get; }
        public Command MoveLeftUpCommand { get; }
        public Command MoveLeftDownCommand { get; }
        public Command MoveRightUpCommand { get; }
        public Command MoveRightDownCommand { get; }
        public Command AttackCommand { get; }
        public Command RecoverCommand { get; }
        public Command ProduceCommand { get; }
        public Command ConstructCommand { get; }
        public Command RebuildCommand { get; }
    }
}
