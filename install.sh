dotnet nuget add source https://neolution.pkgs.visualstudio.com/_packaging/Neolution/nuget/v3/index.json -n Neolution

wget -qO- https://aka.ms/install-artifacts-credprovider.sh | bash

cd SQLSeed

dotnet restore --interactive

curl --compressed -o- -L https://yarnpkg.com/install.sh | bash

npm install nswag@13.4.1 -g