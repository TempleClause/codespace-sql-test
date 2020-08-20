#Add Neolution nuget package source
dotnet nuget add source https://neolution.pkgs.visualstudio.com/_packaging/Neolution/nuget/v3/index.json -n Neolution

#Install credential provider (for dotnet restore --interactive to work)
wget -qO- https://aka.ms/install-artifacts-credprovider.sh | bash

#Install Yarn
curl --compressed -o- -L https://yarnpkg.com/install.sh | bash

#Install nswag globally
npm install nswag@13.4.1 -g

#Start SQLServer docker
sudo docker run --name sql -p 1433:1433 -e ACCEPT_EULA=Y -e "SA_PASSWORD=<YourStrong@Passw0rd>" --user="root" -v /mnt/containerTmp:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2019-latest

#Change directory to project
cd SQLSeed

#Restore packages (requires user to authenticate Neolution nuget source)
dotnet restore --interactive

#Run initializer
