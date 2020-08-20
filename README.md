# Readme



To reproduce do the following:

- Run the sqlserver in docker with the following command: 
  `sudo docker run --rm --name sql -p 1433:1433 -e ACCEPT_EULA=Y -e "SA_PASSWORD=<YourStrong@Passw0rd>" -d mcr.microsoft.com/mssql/server:2019-latest`
- Run the SQL Seed Program with `dotnet run` 
  - After it's done it will tell you how long it took to run the script in the console.



Things we also tried:

- `sudo docker run --rm --name sql -p 1433:1433 -e ACCEPT_EULA=Y -e "SA_PASSWORD=<YourStrong@Passw0rd>" --user="root" -v /tmp/mssql:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2019-latest`
- `sudo docker run --rm --name sql -p 1433:1433 -e ACCEPT_EULA=Y -e "SA_PASSWORD=<YourStrong@Passw0rd>" --user="root" -v /tmp/sql/data:/var/opt/mssql/data -v /tmp/sql/log:/var/opt/mssql/log -v /tmp/sql/secrets:/var/opt/mssql/secrets -d mcr.microsoft.com/mssql/server:2019-latest`
- `sudo docker run --rm --name sql -p 1433:1433 -e ACCEPT_EULA=Y -e "SA_PASSWORD=<YourStrong@Passw0rd>" --user="root" -v /mnt/containerTmp:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2019-latest`



