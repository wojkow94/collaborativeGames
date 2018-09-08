# Collaborative Games

### PG KIO@3 - Platforma do gier zespołowych

### Configuration
- Restore locally DB from collaborativeGamesDB file

- Edit Web.config. for correct connection string
```xml
<connectionStrings>
    <add name="DBContext" connectionString="Data Source=DV6-HP\MSSQLSERVER2012; Initial Catalog=PG2; Integrated Security=SSPI;" providerName="System.Data.SqlClient" />
  </connectionStrings>
 ```
