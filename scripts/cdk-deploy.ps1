$stage = "CDK DEPLOY"
& "$PSScriptRoot\init-env.ps1"
& "$PSScriptRoot\log-start-stage.ps1" $stage

$initialLocation = Get-Location
cd $Env:stack_dir
& $Env:cdk deploy
Set-Location $initialLocation

& "$PSScriptRoot\log-finish-stage.ps1" $stage