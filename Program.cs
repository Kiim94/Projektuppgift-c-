using ProjektUppgift.Book;

//spectre för att ändra utseendet när man kör, lite trevligare
using Spectre.Console;

namespace ProjektUppgift;
class Program
{
    //test av github
    static void Main(string[] args)
    {
        //om man vill kan man ange argument med dotnet run för att ändra vilken ordning man ser status på böcker.
        //skickas till metoden GetReadStatus
        string argReadStatus = GetReadStatus(args);

        if (argReadStatus == "invalid")
        {
            return;
        }

        //kontrollera om booklist.json finns
        //om den finns, läs in bokloggen
        //finns ingen, returnera en ny tom BookList objekt
        //metoder i BookList går fortfarande att använda
        BookList books = JsonBookStorage.Load();

        //för att använda BookDisplay, skapa objekt
        BookDisplay bookDisplay = new BookDisplay();

        while (true)
        {
            Console.Clear();
            
            AnsiConsole.Write(new FigletText("BOOK TRACKER").Centered().Color(Color.Cyan));
            AnsiConsole.Write(new Rule("[yellow]Din personliga digitala bokhylla[/]").RuleStyle("grey"));
            Console.WriteLine();

            Console.WriteLine("1. Visa alla böcker");
            Console.WriteLine("2. Lägg till bok");
            Console.WriteLine("3. Ta bort bok");
            Console.WriteLine("4. Bokförslag");
            Console.WriteLine("5. Avsluta");

            if (!int.TryParse(Console.ReadLine(), out int userChoice) || userChoice < 1 || userChoice > 5)
            {
                Console.WriteLine("Vänligen välj en giltig siffra från menyn mellan 1 och 5.");
                continue;
            }

            switch (userChoice)
            {
                //visa böcker
                case 1:
                    Console.Clear();
                    //kalla på metod för att visa menyn
                    Menys.ShowBookMenu(books, argReadStatus);
                    break;

                //lägg till bok
                case 2:
                    Console.Clear();
                    AnsiConsole.Write(new Rule(" LÄGG TILL BOK "));
                    Menys.AddBookMenu(books);
                    Menys.ReturnMeny();
                    break;

                //radera en bok från json
                case 3:
                    Console.Clear();
                    AnsiConsole.Write(new Rule(" RADERA BOK "));
                    Menys.DeleteBookMeny(books);
                    break;

                //få random bok förslag
                case 4:
                    AnsiConsole.Write(new Rule(" FÅ BOKFÖRSLAG "));
                    Menys.SuggestBookMeny(books);
                    break;
                //stäng applikationen
                case 5:
                    Console.Clear();
                    Console.WriteLine("Bye!");
                    Thread.Sleep(1500);
                    return;

                default:
                    Console.Clear();
                    Console.WriteLine("Ogiltigt val.");
                    break;
            }
        }
    }

    //argument för terminalkommandon
    static string GetReadStatus(string[] args)
    {
        //möjlighet att ange argument i terminalen. Ange antingen dotnet run -- s 1 eller s 2: oläst eller läst
        //om inga argument, så körs programmet ändå: sortering får göras manuellt
        if (args.Length == 0)
        {
            return "all";
        }
        //om args anges, men det är inte två argument
        if (args.Length != 2)
        {
            Console.WriteLine("Ange inga argument eller skriv: dotnet run -- s 1 eller s 2");
            return "invalid";
        }
        //om första arg inte är status
        if (args[0] != "s")
        {
            Console.WriteLine("Första argumentet måste vara 's'!");
            return "invalid";
        }
        //om andra arg varken är 1 eller 2
        if (args[1] != "1" && args[1] != "2")
        {
            Console.WriteLine("Andra argumentet måste vara 1 eller 2!");
            return "invalid";
        }
        //om arg 2 är 1, visa olästa böcker först, annars returnera lästa
        if (args[1] == "1")
        {
            return "unread";
        }
        return "read";
    }
}