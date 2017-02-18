# Projekt grupowy

### PG KIO@3 - Platforma do gier zespołowych

### Konfiguracja
- Zedytuj linie odpowiadające za połączenie z bazą danych, znajdujące się w głównym pliku Web.config.
```xml
<connectionStrings>
    <add name="DBContext" connectionString="Data Source=DV6-HP\MSSQLSERVER2012; Initial Catalog=PG2; Integrated Security=SSPI;" providerName="System.Data.SqlClient" />
  </connectionStrings>
 ```

- Przed pierwszym uruchomieniem serwera odkomentuj 3 linie odpowiadające za zbudowanie i zapisanie 3 gier w bazie, znajdujące się w pliku Platform.cs. 
 
 ```C
 //(new SpeedBoatBuilder()).Build();
 //(new BuyAFeatureBuilder()).Build();
 //(new AvaxStormingBuilder()).Build();
```

 Przy pierwszym uruchomieniu baza danych utworzy się automatycznie na podstawie klas, w miejscu wskazanym w pliku Web.config.
 
