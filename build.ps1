# original: https://gist.github.com/peters/6812859/raw/a965bdb5ae39dbff351420da64df4e9925e07e36/Build.ps1

param(
    [string]$packageVersion = $null,
    [string]$config = "Release",
    [string[]]$targetFrameworks = @("v4.5"),
    [string[]]$platforms = @("AnyCpu"),
    [ValidateSet("rebuild", "build")]
    [string]$target = "rebuild",
    [ValidateSet("quiet", "minimal", "normal", "detailed", "diagnostic")]
    [string]$verbosity = "minimal",
    [bool]$alwaysClean = $true
)

# Diagnostic 
function Write-Diagnostic {
    param([string]$message)

    Write-Host
    Write-Host $message -ForegroundColor Green
    Write-Host
}

function Die([string]$message, [object[]]$output) {
    if ($output) {
        Write-Output $output
        $message += ". See output above."
    }
    Write-Error $message
    exit 1
}

function Create-Folder-Safe {
    param(
        [string]$folder = $(throw "-folder is required.")
    )

    if(-not (Test-Path $folder)) {
        [System.IO.Directory]::CreateDirectory($folder)
    }

}

# Build
function Build-Clean {
    param(
        [string]$rootFolder = $(throw "-rootFolder is required."),
        [string]$folders = "bin,obj"
    )

    Write-Diagnostic "Build: Clean"

    Get-ChildItem $rootFolder -Include $folders -Recurse | ForEach-Object {
       Remove-Item $_.fullname -Force -Recurse 
    }
}

function Build-Bootstrap {
    param(
        [string]$solutionFile = $(throw "-solutionFile is required."),
        [string]$nugetExe = $(throw "-nugetExe is required.")
    )

    Write-Diagnostic "Build: Bootstrap"

    $solutionFolder = [System.IO.Path]::GetDirectoryName($solutionFile)

    . $nugetExe config -Set Verbosity=quiet
    . $nugetExe restore $solutionFile -NonInteractive

    Get-ChildItem $solutionFolder -filter packages.config -recurse | 
        Where-Object { -not ($_.PSIsContainer) } | 
        ForEach-Object {

        . $nugetExe restore $_.FullName -NonInteractive -SolutionDirectory $solutionFolder

    }
}

function Build-Nupkg {
    param(
        [string]$rootFolder = $(throw "-rootFolder is required."),
        [string]$project = $(throw "-project is required."),
        [string]$nugetExe = $(throw "-nugetExe is required."),
        [string]$outputFolder = $(throw "-outputFolder is required."),
        [string]$config = $(throw "-config is required."),
        [string]$version = $(throw "-version is required."),
        [string]$platform = $(throw "-platform is required.")
    )

    $outputFolder = Join-Path $outputFolder "$config"
    $nuspecFilename = [System.IO.Path]::GetFullPath($project) -ireplace ".csproj$", ".nuspec"

    if(-not (Test-Path $nuspecFilename)) {
        Die("Could not find nuspec: $nuspecFilename")
    }

    Write-Diagnostic "Creating nuget package for platform $platform"

    # http://docs.nuget.org/docs/reference/command-line-reference#Pack_Command
    . $nugetExe pack $nuspecFilename -OutputDirectory $outputFolder -Symbols -NonInteractive `
        -Properties "Configuration=$config;Bin=$outputFolder;Platform=$platform" -Version $version

    if($LASTEXITCODE -ne 0) {
        Die("Build failed: $projectName")
    }

    # Support for multiple build runners
    if(Test-Path env:BuildRunner) {
        $buildRunner = Get-Content env:BuildRunner

        switch -Wildcard ($buildRunner.ToString().ToLower()) {
            "myget" {

                $mygetBuildFolder = Join-Path $rootFolder "Build"

                Create-Folder-Safe -folder $mygetBuildFolder

                Get-ChildItem $outputFolder -filter *.nupkg | 
                Where-Object { -not ($_.PSIsContainer) } | 
                ForEach-Object {
                    $fullpath = $_.FullName
                    $filename = $_.Name

                    cp $fullpath $mygetBuildFolder\$filename
                }

            }
        }
    }
}

function Build-Project {
    param(
        [string]$project = $(throw "-project is required."),
        [string]$outputFolder = $(throw "-outputFolder is required."),
        [string]$nugetExe = $(throw "-nugetExe is required."),
        [string]$config = $(throw "-config is required."),
        [string]$target = $(throw "-target is required."),
        [string[]]$targetFrameworks = $(throw "-targetFrameworks is required."),
        [string[]]$platform = $(throw "-platform is required.")
    )

    $projectPath = [System.IO.Path]::GetFullPath($project)
    $projectName = [System.IO.Path]::GetFileName($projectPath) -ireplace ".csproj$", ""

    Create-Folder-Safe -folder $outputFolder

    if(-not (Test-Path $projectPath)) {
        Die("Could not find csproj: $projectPath")
    }

    $targetFrameworks | foreach-object {
        $targetFramework = $_
        $platformOutputFolder = Join-Path $outputFolder "$config\$targetFramework"

        Create-Folder-Safe -folder $platformOutputFolder

        Write-Diagnostic "Build: $projectName ($platform / $config - $targetFramework)"

        & "$(Get-Content env:windir)\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe" `
            $projectPath `
            /t:$target `
            /p:Configuration=$config `
            /p:OutputPath=$platformOutputFolder `
            /p:TargetFrameworkVersion=$targetFramework `
            /p:Platform=$platform `
            /m `
            /v:M `
            /fl `
            /flp:LogFile=$platformOutputFolder\msbuild.log `
            /nr:false

        if($LASTEXITCODE -ne 0) {
            Die("Build failed: $projectName ($Config - $targetFramework)")
        }
    }
}

function Build-Solution {
    param(
        [string]$rootFolder = $(throw "-rootFolder is required."),
        [string]$solutionFile = $(throw "-solutionFile is required."),
        [string]$outputFolder = $(throw "-outputFolder is required."),
        [string]$version = $(throw "-version is required"),
        [string]$config = $(throw "-config is required."),
        [string]$target = $(throw "-target is required."),
        [bool]$alwaysClean = $(throw "-alwaysclean is required"),
        [string[]]$targetFrameworks = $(throw "-targetFrameworks is required."),
        [string[]]$projects = $(throw "-projects is required."),
        [string[]]$platforms = $(throw "-platforms is required.")
    )

    if(-not (Test-Path $solutionFile)) {
        Die("Could not find solution: $solutionFile")
    }

    $solutionFolder = [System.IO.Path]::GetDirectoryName($solutionFile)
    $nugetExe = if(Test-Path Env:NuGet) { Get-Content env:NuGet } else { Join-Path $solutionFolder ".nuget\nuget.exe" }

    if($alwaysClean) {
        Build-Clean -root $solutionFolder
    }

    Build-Bootstrap -solutionFile $solutionFile -nugetExe $nugetExe

    $projects | ForEach-Object {

        $project = $_

        $platforms | ForEach-Object {
            $platform = $_

            $buildOutputFolder = Join-Path $outputFolder "$version\$platform"

            Build-Project -rootFolder $solutionFolder -project $project -outputFolder $buildOutputFolder `
                -nugetExe $nugetExe -target $target -config $config `
                -targetFrameworks $targetFrameworks -version $version -platform $platform

            Build-Nupkg -rootFolder $rootFolder -project $project -nugetExe $nugetExe -outputFolder $buildOutputFolder `
                -config $config -version $version -platform $platform
        }
    }
}

function TestRunner-Nunit {
    param(
        [string]$outputFolder = $(throw "-outputFolder is required."),
        [string]$config = $(throw "-config is required."),
        [string]$target = $(throw "-target is required."),
        [string[]]$projects = $(throw "-projects is required."),
        [string[]]$platforms = $(throw "-platforms is required.")
    )

    Die("TODO")
}

# Bootstrap
$rootFolder = Split-Path -parent $script:MyInvocation.MyCommand.Definition
$outputFolder = Join-Path $rootFolder "bin"
$testsFolder = Join-Path $outputFolder "tests"

$config = $config.substring(0, 1).toupper() + $config.substring(1)
$version = $config.trim()

# Myget
$currentVersion = if(Test-Path env:PackageVersion) { Get-Content env:PackageVersion } else { $packageVersion }

if($currentVersion -eq "") {
    Die("Package version cannot be empty")
}

# Build
$net35projects = @( `
    "$rootFolder\src\Flaherty.Services.GoogleCharts.Net35\Flaherty.Services.GoogleCharts.Net35.csproj",
    "$rootFolder\src\Flaherty.Services.GoogleCharts.WebControls.Net35\Flaherty.Services.GoogleCharts.WebControls.Net35.csproj"
) 

$net35projects | ForEach-Object {
	$net35project = $_

	$platforms | ForEach-Object {
		$platform = $_

		$buildOutputFolder = Join-Path $outputFolder "$currentVersion\$platform"

		Build-Project -rootFolder $rootFolder `
			-project $net35project `
			-outputFolder $buildOutputFolder `
			-nugetExe $nugetExe `
			-target $target `
			-config $config `
			-targetFrameworks @( "v3.5" ) `
			-version $currentVersion `
			-platform $platform
	}
}

Build-Solution -solutionFile $rootFolder\Flaherty.Services.GoogleCharts.sln `
    -projects @( `
        "$rootFolder\src\Flaherty.Services.GoogleCharts\Flaherty.Services.GoogleCharts.csproj",
        "$rootFolder\src\Flaherty.Services.GoogleCharts.WebControls\Flaherty.Services.GoogleCharts.WebControls.csproj"
    ) `
    -rootFolder $rootFolder `
    -outputFolder $outputFolder `
    -platforms $platforms `
    -version $currentVersion `
    -config $config `
    -target $target `
    -targetFrameworks $targetFrameworks `
    -alwaysClean $alwaysClean