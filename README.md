# Projektuppgift i C#
I denna uppgift har jag tagit fram en konsolapplikation som är menad att fungera som ett digitalt verktyg där användaren kan registrera lästa böcker och böcker som planeras att läsas. Det går:
- Att söka böcker
- Att betygsätta och ändra lässtatus
- Att radera och lägga till böcker
- Att slumpa fram en oläst bok

I applikationen har **Spectre.Console** använts för att göra den mer tilltalande att använda och navigera igenom.

## Använda tekniker
- Programmeringsspråket C#
- .NET 10
- Visual Studio
- Spectre.Console
## Konsolkommandon
Applikationen låter användaren skriva kommandoargument i terminalen för att ändra vilken ordning böckerna visas (olästa eller lästa först).
Observera att sorteringsordningen inte kan ändras medan applikationen körs. Man måste lämna applikationen och använda motsatta argumentet eller starta det helt utan argument.

Dessa argument är:
- Olästa först: **`dotnet run -- s 1`**
- Lästa först: **`dotnet run -- s 2`**
- Ingen ordning, tillåter manuell ändring: **`dotnet run`**

