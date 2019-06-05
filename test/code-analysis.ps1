$testProjects = 
    'OffSync.Mapping.Mappert.Practises.Tests',
    'OffSync.Mapping.Mappert.Tests',
    'OffSync.Mapping.Mappert.Reflection.Tests',
    'OffSync.Mapping.Mappert.DynamicMethods.Tests'

$frameworks = 
    'netcoreapp2.2', 
    'net461', 
    'net472'

foreach ($testProject in $testProjects)
{
    pushd $testProject

    foreach ($framework in $frameworks)
    {

        dotnet test --no-build --framework $framework /p:CollectCoverage=true /p:CoverletOutputFormat=opencover "/p:CoverletOutput=..\out\$($testProject)-$($framework).xml"

    }

    popd
}

reportgenerator '-reports:out\*.xml' '-targetdir:out\coveragereport'

ii out\coveragereport\index.htm
