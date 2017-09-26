# connect4

To win, connect 4 pieces in a row, column or diagonally.
By deafult, this is a 5 rows x 5 columns board.

Below is an example of parameters you could change in the App.config
```
  <appSettings>
    <add key="Rows" value="5" />
    <add key="Columns" value="5" />
    <add key="DiscsToWin" value="4"/>   <!-- number of discs in a row, column or diagonally to win -->
    <add key="Gamer1Symbol" value="R"/> <!-- Red discs -->
    <add key="Gamer2Symbol" value="Y"/> <!-- Yellow discs -->
    <add key="EmptySymbol" value="-"/>
  </appSettings>
```
