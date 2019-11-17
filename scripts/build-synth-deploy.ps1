$Env:code_version = Get-Date -Format yyyyMMddHHmmss

& "$PSScriptRoot\init-env.ps1"

& "$PSScriptRoot\build-lambda-images.ps1"

& "$PSScriptRoot\publish-lambda-images.ps1"

& "$PSScriptRoot\build-npm.ps1"

& "$PSScriptRoot\cdk-synth.ps1"

& "$PSScriptRoot\cdk-deploy.ps1"