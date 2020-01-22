$stage = "BUILD->SYNTH->DEPLOY"
& "$PSScriptRoot\init-env.ps1"
& "$PSScriptRoot\log-start-stage.ps1" $stage

$Env:code_version = Get-Date -Format yyyyMMddHHmmss

& "$PSScriptRoot\build-lambda-images.ps1"
& "$PSScriptRoot\publish-lambda-images.ps1"
& "$PSScriptRoot\build-npm.ps1"
& "$PSScriptRoot\cdk-synth.ps1"
& "$PSScriptRoot\cdk-deploy.ps1"

& "$PSScriptRoot\log-finish-stage.ps1" $stage