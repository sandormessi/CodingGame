namespace CodingGames.Events.WinterChallenge2023;

internal class SecondTask
{
   #region Constants and Fields

   private const string AlphaOperation = "ALPHA";

   private const string NeutronOperation = "NEUTRON";

   private const string ProtonOperation = "PROTON";

   #endregion

   #region Public Methods and Operators

   public static List<string> Solve(int protonsStart, int neutronsStart, int protonsTarget, int neutronsTarget)
   {
      List<string> instructions = new List<string>();

      var actualProton = protonsStart;
      var actualNeutrons = neutronsStart;

      if (protonsTarget < actualProton)
      {
         while (actualProton > protonsTarget)
         {
            actualProton -= 2;
            actualNeutrons -= 2;

            instructions.Add(AlphaOperation);
         }
      }

      if (neutronsTarget <= actualNeutrons)
      {
         while (actualNeutrons > protonsTarget)
         {
            actualProton -= 2;
            actualNeutrons -= 2;

            instructions.Add(AlphaOperation);
         }
      }

      ExecuteProtonOperations(actualProton, protonsTarget, instructions);
      ExecuteNeutronOperations(actualNeutrons, neutronsTarget, instructions);

      return instructions;
   }

   #endregion

   #region Methods

   private static void ExecuteNeutronOperations(int neutronsStart, int neutronsTarget, List<string> instructions)
   {
      var actualNeutrons = neutronsStart;
      while (actualNeutrons < neutronsTarget)
      {
         actualNeutrons++;
         instructions.Add(NeutronOperation);
      }
   }

   private static void ExecuteProtonOperations(int protonsStart, int protonsTarget, ICollection<string> instructions)
   {
      var actualProton = protonsStart;
      while (actualProton < protonsTarget)
      {
         actualProton++;
         instructions.Add(ProtonOperation);
      }
   }

   #endregion
}