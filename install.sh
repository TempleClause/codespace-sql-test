curl --compressed -o- -L https://yarnpkg.com/install.sh | bash

npm install nswag@13.4.1 -g

dotnet nuget add source https://neolution.pkgs.visualstudio.com/_packaging/Neolution/nuget/v3/index.json -n Neolution

wget -qO- https://aka.ms/install-artifacts-credprovider.sh | bash

dotnet restore --interactive

sudo docker run --name sql -p 1433:1433 -e ACCEPT_EULA=Y -e "SA_PASSWORD=<YourStrong@Passw0rd>" --user="root" -v /mnt/containerTmp:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2019-latest