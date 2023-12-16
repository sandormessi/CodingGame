namespace CodingGames.Events.WinterChallenge2023;

using System;
using System.Collections.Generic;
using System.Linq;

internal class FifthTask
{
   #region Constants and Fields

   private const string AlphaOperation = "ALPHA";

   private const string NeutronOperation = "NEUTRON";

   private const string ProtonOperation = "PROTON";

   #endregion

   #region Enums

   private enum Operations
   {
      Alpha,

      Neutron,

      Proton
   }

   #endregion

   #region Public Methods and Operators

   public static List<string> Solve(int protonsStart, int neutronsStart, int protonsTarget, int neutronsTarget, List<List<int>> unstableIsotopes)
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

   private static void ExecuteOperation(List<string> instructions, Isotope isotope, Operations operation)
   {
      switch (operation)
      {
         case Operations.Alpha:

            isotope.Neutron -= 2;
            isotope.Proton -= 2;
            instructions.Add(AlphaOperation);

            break;
         case Operations.Neutron:

            isotope.Neutron++;
            instructions.Add(NeutronOperation);

            break;
         case Operations.Proton:

            isotope.Proton++;
            instructions.Add(ProtonOperation);

            break;
         default:
            throw new ArgumentOutOfRangeException(nameof(operation), operation, null);
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

   private static bool IsUnstableIsotope(List<List<int>> unstableIsotopes, Isotope isotope)
   {
      return unstableIsotopes.Any(x => x[0] == isotope.Proton && x[1] == isotope.Neutron);
   }

   private static void UndoOperation(List<string> instructions, Isotope isotope, Operations operation)
   {
      switch (operation)
      {
         case Operations.Alpha:

            isotope.Neutron += 2;
            isotope.Proton += 2;

            break;
         case Operations.Neutron:

            isotope.Neutron--;

            break;
         case Operations.Proton:

            isotope.Proton--;

            break;
         default:
            throw new ArgumentOutOfRangeException(nameof(operation), operation, null);
      }

      instructions.RemoveAt(instructions.Count - 1);
   }

   #endregion

   private sealed class Isotope
   {
      #region Constants and Fields

      public int Neutron;

      public int Proton;

      #endregion
   }
}