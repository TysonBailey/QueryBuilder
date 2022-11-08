using QueryBuilder.Models;
using QueryBuilder;

string DataB = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString() + "\\Data\\Lab5Database.db";
List<Author> authors;
using(var qb = new QueryBuilder.QueryBuilder(DataB))
{
    
    var WilliamShakespeare = new Author(20, "William", "Shakespeare");

    qb.Create<Author>(WilliamShakespeare);

    authors = qb.ReadAll<Author>();

    var readOne = qb.Read<Author>(20);

    WilliamShakespeare.FirstName = "William";
    WilliamShakespeare.Surname = "Shakespeare";
    qb.Update<Author>(WilliamShakespeare);

    qb.Delete<Author>(WilliamShakespeare);

}

foreach(var author in authors)
{
    Console.WriteLine(author.ID + " " + author.FirstName + " " + author.Surname);
}

