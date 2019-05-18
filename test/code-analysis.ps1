cd 'OffSync.Mapping.Mappert.Tests'
dotnet test --no-build /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput='..\out\OffSync.Mapping.Mappert.Tests.xml'
cd ..

cd 'OffSync.Mapping.Mappert.DynamicMethods.Tests'
dotnet test --no-build /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput='..\out\OffSync.Mapping.Mappert.DynamicMethods.Tests.xml'
cd ..

reportgenerator '-reports:out\*.xml' '-targetdir:out\coveragereport'
