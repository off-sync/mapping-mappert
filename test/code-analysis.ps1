cd 'OffSync.Mapping.Mappert.DynamicMethods.Tests'
dotnet test --no-build /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput='..\out\OffSync.Mapping.Mappert.DynamicMethods.Tests.xml'
cd ..

cd 'OffSync.Mapping.Mappert.Practises.Tests'
dotnet test --no-build /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput='..\out\OffSync.Mapping.Mappert.Practises.Tests.xml'
cd ..

cd 'OffSync.Mapping.Mappert.Reflection.Tests'
dotnet test --no-build /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput='..\out\OffSync.Mapping.Mappert.Reflection.Tests.xml'
cd ..

cd 'OffSync.Mapping.Mappert.Tests'
dotnet test --no-build /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput='..\out\OffSync.Mapping.Mappert.Tests.xml'
cd ..

reportgenerator '-reports:out\*.xml' '-targetdir:out\coveragereport'
