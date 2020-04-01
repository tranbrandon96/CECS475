// JoiningTableData.cs
// Using LINQ to perform a join and aggregate data across tables.
using System;
using System.Linq;
using System.Windows.Forms;

namespace JoinQueries
{
    public partial class JoiningTableData : Form
    {
        public JoiningTableData()
        {
            InitializeComponent();
        } // end constructor

        private void JoiningTableData_Load(object sender, EventArgs e)
        {
            // Entity Framework DBContext
            BooksEntities dbcontext =
               new BooksEntities();
            // ------------------------------------------------------------------------------------------------------------------------------
            // A. Get a list of all the titles and the authors who wrote them. Sort the result by title.
            //
            // ------------------------------------------------------------------------------------------------------------------------------
            var titlesAndAuthors =
               from book in dbcontext.Titles
               from author in book.Authors
               orderby book.Title1
               select new
               {
                   Name = author.FirstName + " " + author.LastName,
                   book.Title1
               };

            // Display the result by Title
            outputTextBox.AppendText("\r\n\r\nTitles and Authors:");
            foreach (var element in titlesAndAuthors)
            {
                outputTextBox.AppendText(String.Format("\r\n" + element.Title1 + " " + element.Name));
            }

            // ------------------------------------------------------------------------------------------------------------------------------

            // ------------------------------------------------------------------------------------------------------------------------------
            // B. Get a list of all the titles and the authors who wrote them. Sort the result by title. For each title sort the authors 
            //    alphabetically by last name, then first name.
            //
            // ------------------------------------------------------------------------------------------------------------------------------
            var authorsAndTitles =
               from book in dbcontext.Titles
               from author in book.Authors
               orderby book.Title1, author.LastName, author.FirstName
               select new
               {
                   book.Title1,
                   author.FirstName,
                   author.LastName
               };

            // Display the Authors and Titles with authors sorted for each title.
            outputTextBox.AppendText("\r\n\r\nAuthors and titles with authors sorted for each title:");
            foreach (var element in authorsAndTitles)
            {
                outputTextBox.AppendText(String.Format("\r\n" +element.Title1 + " " + element.FirstName + " " + element.LastName));
            }

            // ----------------------------------------------------------------------------------------------------------------------------

            // ----------------------------------------------------------------------------------------------------------------------------
            // C. Get a list of all the authors grouped by title, sorted by title; for a given title sort the author names alphabetically 
            //    by last name first then first name.
            //
            // ----------------------------------------------------------------------------------------------------------------------------

            var titlesByAuthor =
            from book in dbcontext.Titles
            orderby book.Title1
            select new
            {
                titleOfBook = book.Title1,
                Author =
                    from author in book.Authors
                    orderby author.LastName, author.FirstName
                    select new {author.LastName, author.FirstName}
            };
            
            // Display Titles grouped by author
            outputTextBox.AppendText("\r\n\r\nTitles grouped by author:");
            foreach (var title in titlesByAuthor)
            {
                outputTextBox.AppendText("\r\n" + title.titleOfBook + ":");
                foreach (var author in title.Author)
                {
                    outputTextBox.AppendText("\r\n" + author.FirstName + " " + author.LastName);
                }
            }

            // ----------------------------------------------------------------------------------------------------------------------------
        } // end method JoiningTableData_Load
    } // end class JoiningTableData
} // end namespace JoinQueries

