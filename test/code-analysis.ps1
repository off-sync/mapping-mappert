$testProjects = 
    'OffSync.Mapping.Mappert.Practises.Tests',
    'OffSync.Mapping.Mappert.Tests',
    'OffSync.Mapping.Mappert.Reflection.Tests',
    'OffSync.Mapping.Mappert.DynamicMethods.Tests',
	'OffSync.Mapping.Mappert.Benchmarks.Tests'

$frameworks = 
    'netcoreapp2.2'#, 
#    'net461', 
#    'net472'

foreach ($framework in $frameworks)
{
	foreach ($testProject in $testProjects)
    {
		pushd $testProject

        dotnet test --no-build --framework $framework /p:CollectCoverage=true /p:CoverletOutputFormat=opencover "/p:CoverletOutput=..\out\$($framework)\$($testProject).xml"

		popd
    }

	reportgenerator "-reports:out\$($framework)\*.xml" "-targetdir:out\$($framework)\coveragereport"
}

foreach ($framework in $frameworks)
{
	ii out\$framework\coveragereport\index.htm
}
