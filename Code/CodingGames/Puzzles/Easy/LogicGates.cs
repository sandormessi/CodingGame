namespace CodingGames.Puzzles.Easy;

public class LogicGates
{
   #region Constants and Fields

   private static readonly Dictionary<LogicalGateType, Func<bool, bool, bool>> logicalGateEvaluate = new()
   {
      { LogicalGateType.And, (input1, input2) => input1 && input2 },
      { LogicalGateType.Or, (input1, input2) => input1 || input2 },
      { LogicalGateType.Xor, (input1, input2) => input1 ^ input2 },
      { LogicalGateType.NXor, (input1, input2) => input1 == input2 },
      { LogicalGateType.NAnd, (input1, input2) => !input1 || !input2 },
      { LogicalGateType.Nor, (input1, input2) => !input1 && !input2 }
   };

   #endregion

   #region Enums

   private enum LogicalGateType
   {
      And,

      Or,

      Xor,

      NAnd,

      Nor,

      NXor
   }

   #endregion

   #region Methods

   private static OutputSignal ConvertSignalToBool(char c, string inputName)
   {
      return c switch
      {
         '_' => new OutputSignal(inputName, false),
         '-' => new OutputSignal(inputName, true),
         _ => throw new InvalidOperationException("Unknown signal value.")
      };
   }

   private static bool EvaluateLogicalGate(LogicalGateType type, OutputSignal input1, OutputSignal input2)
   {
      var func = logicalGateEvaluate[type];

      return func(input1.Signal, input2.Signal);
   }

   static void Main()
   {
      string[] inputs;

      var outputSignalCount = int.Parse(ReadInput());
      var logicalGateCount = int.Parse(ReadInput());

      List<OutputSignalCollection> outPutSignals = new(outputSignalCount);
      List<LogicalGate> logicalGateList = new(logicalGateCount);

      int inputSignalCount = 0;
      for (var i = 0; i < outputSignalCount; i++)
      {
         inputs = ReadInput().Split(' ');

         var inputName = inputs[0];
         var inputSignal = inputs[1];

         inputSignalCount = inputSignal.Length;

         outPutSignals.Add(new OutputSignalCollection(inputName, inputSignal.Select(x => ConvertSignalToBool(x, inputName))));
      }

      for (var i = 0; i < logicalGateCount; i++)
      {
         inputs = ReadInput().Split(' ');

         var outputName = inputs[0];
         var type = inputs[1];
         var inputName1 = inputs[2];
         var inputName2 = inputs[3];

         logicalGateList.Add(new LogicalGate(outputName, Enum.Parse<LogicalGateType>(type, true), inputName1, inputName2));
      }

      for (var j = 0; j < logicalGateCount; j++)
      {
         var actualLogicalGate = logicalGateList[j];

         var input1 = outPutSignals.First(x => x.Name.Equals(actualLogicalGate.InputName1, StringComparison.CurrentCultureIgnoreCase)).Signals;
         var input2 = outPutSignals.First(x => x.Name.Equals(actualLogicalGate.InputName2, StringComparison.CurrentCultureIgnoreCase)).Signals;

         Console.Write($"{actualLogicalGate.OutputName} ");
         for (int i = 0; i < inputSignalCount; i++)
         {
            var output = EvaluateLogicalGate(actualLogicalGate.Type, input1[i], input2[i]);
            WriteOutputSignal(output);
         }

         Console.WriteLine();
      }
   }

   private static string ReadInput()
   {
      return Console.ReadLine() ?? throw new InvalidOperationException("There is no input.");
   }

   private static void WriteOutputSignal(bool output)
   {
      if (output)
      {
         Console.Write('-');
      }
      else
      {
         Console.Write('_');
      }
   }

   #endregion

   private sealed class LogicalGate
   {
      #region Constructors and Destructors

      public LogicalGate(string outputName, LogicalGateType type, string inputName1, string inputName2)
      {
         OutputName = outputName;
         Type = type;
         InputName1 = inputName1;
         InputName2 = inputName2;
      }

      #endregion

      #region Public Properties

      public string InputName1 { get; }

      public string InputName2 { get; }

      public string OutputName { get; }

      public LogicalGateType Type { get; }

      #endregion
   }

   private sealed class OutputSignal
   {
      #region Constructors and Destructors

      public OutputSignal(string name, bool signal)
      {
         Name = name;
         Signal = signal;
      }

      #endregion

      #region Public Properties

      public string Name { get; }

      public bool Signal { get; }

      #endregion
   }

   private sealed class OutputSignalCollection
   {
      #region Constructors and Destructors

      public OutputSignalCollection(string name, IEnumerable<OutputSignal> signals)
      {
         Name = name;
         Signals = signals.ToArray();
      }

      #endregion

      #region Public Properties

      public string Name { get; }

      public IReadOnlyList<OutputSignal> Signals { get; }

      #endregion
   }
}