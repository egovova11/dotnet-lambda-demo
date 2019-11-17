$initialLocation = Get-Location
& "$PSScriptRoot\init-env.ps1"

cd $Env:stack_dir
& $Env:cdk synth

Set-Location $initialLocation