# Taken from psake https://github.com/psake/psake

<#
.SYNOPSIS
  This is a helper function that runs a scriptblock and checks the PS variable $lastexitcode
  to see if an error occcured. If an error is detected then an exception is thrown.
  This function allows you to run command-line programs without having to
  explicitly check the $lastexitcode variable.
.EXAMPLE
  exec { svn info $repository_trunk } "Error executing SVN. Please verify SVN command-line client is installed"
#>
function Exec
{
    [CmdletBinding()]
    param(
        [Parameter(Position=0,Mandatory=1)][scriptblock]$cmd,
        [Parameter(Position=1,Mandatory=0)][string]$errorMessage = ($msgs.error_bad_command -f $cmd)
    )
    & $cmd
    if ($lastexitcode -ne 0) {
        throw ("Exec: " + $errorMessage)
    }
}

if(Test-Path .\src\UnidecodeSharpCore\artifacts) { Remove-Item .\src\UnidecodeSharpCore\artifacts -Force -Recurse }

exec { & dotnet restore }

exec { & dotnet build UnidecodeSharpCore.sln -c Release -v q /nologo }

Push-Location -Path .\test\UnidecodeSharpCore.Test

exec { & dotnet test -c Release }

Pop-Location

$samples = Get-ChildItem .\samples\UnidecodeSharpCore.Examples.*

foreach ($sample in $samples) {
    Push-Location -Path $sample

    exec { & dotnet run -c Release --no-build }

    Pop-Location
}

exec { & dotnet pack .\src\UnidecodeSharpCore\UnidecodeSharpCore.csproj -c Release -o .\artifacts --include-symbols --no-build }