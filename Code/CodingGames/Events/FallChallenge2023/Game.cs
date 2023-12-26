namespace CodingGames.Events.FallChallenge2023;

using System;
using System.Collections.Generic;
using System.Linq;

public class Game
{
   #region Methods

   private static void ExecuteGameLogic(GameData gameData)
   {
      for (var i = 0; i < gameData.MyDrones.Count; i++)
      {
         Console.WriteLine("WAIT 1"); // MOVE <x> <y> <light (1|0)> | WAIT <light (1|0)>
      }
   }

   private static GameData GetGameData(IReadOnlyList<Animal> animals)
   {
      int myScore = int.Parse(ReadInput());
      int foeScore = int.Parse(ReadInput());

      GameStatistics statistics = new(myScore, foeScore);

      int myScanCount = int.Parse(ReadInput());
      List<Animal> myScans = new(myScanCount);
      for (int i = 0; i < myScanCount; i++)
      {
         int creatureId = int.Parse(ReadInput());

         Animal foundAnimal = animals.First(x => x.Id == creatureId);
         myScans.Add(foundAnimal);
      }

      int foeScanCount = int.Parse(ReadInput());
      List<Animal> opponentScans = new(myScanCount);
      for (int i = 0; i < foeScanCount; i++)
      {
         int creatureId = int.Parse(ReadInput());

         Animal foundAnimal = animals.First(x => x.Id == creatureId);
         opponentScans.Add(foundAnimal);
      }

      int myDroneCount = int.Parse(ReadInput());
      string[] inputs;

      List<Drone> myDrones = new(myDroneCount);
      for (int i = 0; i < myDroneCount; i++)
      {
         inputs = ReadInput().Split(' ');
         int droneId = int.Parse(inputs[0]);
         int droneX = int.Parse(inputs[1]);
         int droneY = int.Parse(inputs[2]);
         int emergency = int.Parse(inputs[3]);
         int battery = int.Parse(inputs[4]);

         Drone myDrone = new(droneId, new Coordinate2D(droneX, droneY), emergency, battery);
         myDrones.Add(myDrone);
      }

      int foeDroneCount = int.Parse(ReadInput());
      List<Drone> opponentDrones = new(myDroneCount);
      for (int i = 0; i < foeDroneCount; i++)
      {
         inputs = ReadInput().Split(' ');
         int droneId = int.Parse(inputs[0]);
         int droneX = int.Parse(inputs[1]);
         int droneY = int.Parse(inputs[2]);
         int emergency = int.Parse(inputs[3]);
         int battery = int.Parse(inputs[4]);

         Drone opponentDrone = new(droneId, new Coordinate2D(droneX, droneY), emergency, battery);
         opponentDrones.Add(opponentDrone);
      }

      int droneScanCount = int.Parse(ReadInput());
      List<Scan> scans = new(droneScanCount);
      for (int i = 0; i < droneScanCount; i++)
      {
         inputs = ReadInput().Split(' ');
         int droneId = int.Parse(inputs[0]);
         int creatureId = int.Parse(inputs[1]);

         Scan scan = new(myDrones.Concat(opponentDrones).First(x => x.Id == droneId), animals.First(x => x.Id == creatureId));
         scans.Add(scan);
      }

      int visibleCreatureCount = int.Parse(ReadInput());
      List<Fish> fishes = new(visibleCreatureCount);
      for (int i = 0; i < visibleCreatureCount; i++)
      {
         inputs = ReadInput().Split(' ');
         int creatureId = int.Parse(inputs[0]);
         int creatureX = int.Parse(inputs[1]);
         int creatureY = int.Parse(inputs[2]);
         int creatureVx = int.Parse(inputs[3]);
         int creatureVy = int.Parse(inputs[4]);

         Animal foundAnimal = animals.Single(x => x.Id == creatureId);
         Fish fish = new(foundAnimal.Id, foundAnimal.Color, foundAnimal.Type, new Coordinate2D(creatureX, creatureY), creatureVy, creatureVx);
         fishes.Add(fish);
      }

      int radarBlipCount = int.Parse(ReadInput());
      for (int i = 0; i < radarBlipCount; i++)
      {
         inputs = ReadInput().Split(' ');
         int droneId = int.Parse(inputs[0]);
         int creatureId = int.Parse(inputs[1]);
         string radar = inputs[2];
      }

      return new GameData(animals, statistics, myScans, opponentScans, myDrones, opponentDrones, scans, fishes);
   }

   private static void Main()
   {
      string[] inputs;

      int creatureCount = int.Parse(ReadInput());
      List<Animal> animals = new(creatureCount);
      for (int i = 0; i < creatureCount; i++)
      {
         inputs = ReadInput().Split(' ');
         int creatureId = int.Parse(inputs[0]);
         int color = int.Parse(inputs[1]);
         int type = int.Parse(inputs[2]);

         Animal animal = new(creatureId, color, type);
         animals.Add(animal);
      }

      // game loop
      while (true)
      {
         GameData gameData = GetGameData(animals);

         ExecuteGameLogic(gameData);
      }
   }

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is no input.");
   }

   #endregion

   private class Animal
   {
      #region Constructors and Destructors

      public Animal(int id, int color, int type)
      {
         Id = id;
         Color = color;
         Type = type;
      }

      #endregion

      #region Public Properties

      public int Color { get; }

      public int Id { get; }

      public int Type { get; }

      #endregion
   }

   private sealed class Coordinate2D
   {
      #region Constructors and Destructors

      public Coordinate2D(int x, int y)
      {
         X = x;
         Y = y;
      }

      #endregion

      #region Public Properties

      public int X { get; }

      public int Y { get; }

      #endregion
   }

   private sealed class Drone
   {
      #region Constructors and Destructors

      public Drone(int id, Coordinate2D position, int emergency, int battery)
      {
         Id = id;
         Position = position;
         Emergency = emergency;
         Battery = battery;
      }

      #endregion

      #region Public Properties

      public int Battery { get; }

      public int Emergency { get; }

      public int Id { get; }

      public Coordinate2D Position { get; }

      #endregion
   }

   private sealed class Fish : Animal
   {
      #region Constructors and Destructors

      public Fish(int id, int color, int type, Coordinate2D position, int verticalSpeed, int horizontalSpeed)
         : base(id, color, type)
      {
         Position = position;
         VerticalSpeed = verticalSpeed;
         HorizontalSpeed = horizontalSpeed;
      }

      #endregion

      #region Public Properties

      public int HorizontalSpeed { get; }

      public Coordinate2D Position { get; }

      public int VerticalSpeed { get; }

      #endregion
   }

   private sealed class GameData
   {
      #region Constructors and Destructors

      public GameData(IReadOnlyList<Animal> animals, GameStatistics statistics, IReadOnlyList<Animal> myScans, IReadOnlyList<Animal> opponentScans,
         IReadOnlyList<Drone> myDrones, IReadOnlyList<Drone> opponentDrones, IReadOnlyList<Scan> scans, IReadOnlyList<Fish> fishes)
      {
         Animals = animals ?? throw new ArgumentNullException(nameof(animals));
         Statistics = statistics ?? throw new ArgumentNullException(nameof(statistics));
         MyScans = myScans ?? throw new ArgumentNullException(nameof(myScans));
         OpponentScans = opponentScans ?? throw new ArgumentNullException(nameof(opponentScans));
         MyDrones = myDrones ?? throw new ArgumentNullException(nameof(myDrones));
         OpponentDrones = opponentDrones ?? throw new ArgumentNullException(nameof(opponentDrones));
         Scans = scans ?? throw new ArgumentNullException(nameof(scans));
         Fishes = fishes ?? throw new ArgumentNullException(nameof(fishes));
      }

      #endregion

      #region Public Properties

      public IReadOnlyList<Animal> Animals { get; }

      public IReadOnlyList<Fish> Fishes { get; }

      public IReadOnlyList<Drone> MyDrones { get; }

      public IReadOnlyList<Animal> MyScans { get; }

      public IReadOnlyList<Drone> OpponentDrones { get; }

      public IReadOnlyList<Animal> OpponentScans { get; }

      public IReadOnlyList<Scan> Scans { get; }

      public GameStatistics Statistics { get; }

      #endregion
   }

   private sealed class GameStatistics
   {
      #region Constructors and Destructors

      public GameStatistics(int myScore, int opponentScore)
      {
         MyScore = myScore;
         OpponentScore = opponentScore;
      }

      #endregion

      #region Public Properties

      public int MyScore { get; }

      public int OpponentScore { get; }

      #endregion
   }

   private sealed class Scan
   {
      #region Constructors and Destructors

      public Scan(Drone droneId, Animal fishId)
      {
         DroneId = droneId ?? throw new ArgumentNullException(nameof(droneId));
         FishId = fishId ?? throw new ArgumentNullException(nameof(fishId));
      }

      #endregion

      #region Public Properties

      public Drone DroneId { get; }

      public Animal FishId { get; }

      #endregion
   }
}