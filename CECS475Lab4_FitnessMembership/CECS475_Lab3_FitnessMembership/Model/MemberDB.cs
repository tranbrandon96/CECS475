using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECS475Lab4_FitnessMembership.Model
{
    /// <summary>
    /// A class that uses a text file to store information about the gym members long-term.
    /// </summary>
    class MemberDB : ObservableObject
    {
        /// <summary>
        /// The list of members to be saved.
        /// </summary>
        private ObservableCollection<Member> members;
        /// <summary>
        /// Where the database is stored.
        /// </summary>
        private const string filepath = "../members.txt";

        /// <summary>
        /// Creates a new member database.
        /// </summary>
        /// <param name="m">The list to saved from or written to.</param>
        public MemberDB(ObservableCollection<Member> m)
        {
            members = m;
        }

        /// <summary>
        /// Reads the saved text file database into the program's list of members.
        /// </summary>
        /// <returns>The list containing the text file data read in.</returns>
        public ObservableCollection<Member> GetMemberships()
        {
            try
            {
                StreamReader input = new StreamReader(new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Read));
                string[] separatingStrings = { "|" };

                /* References: https://docs.microsoft.com/en-us/dotnet/csharp/how-to/parse-strings-using-split */
                while (input.Peek() != -1)
                {
                    var line = input.ReadLine();
                    string[] words = line.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
                    members.Add(new Member(words[0], words[1], words[2]));
                }

                input.Close();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid e-mail address format.");
            }
            return members;
        }

        /// <summary>
        /// Saves the program's list of members into the text file database.
        /// </summary>
        public void SaveMemberships()
        {
            StreamWriter output = new StreamWriter(new FileStream(filepath, FileMode.Create, FileAccess.Write));

            foreach (Member m in members)
            {
                output.Write(m.FirstName + "|");
                output.Write(m.LastName + "|");
                output.Write(m.Email + "|");
                output.WriteLine();
            }
            output.Close();
        }
    }
}
