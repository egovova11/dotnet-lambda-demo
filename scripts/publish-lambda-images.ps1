$stage = "PUBLISH LAMBDA IMAGES"
& "$PSScriptRoot\init-env.ps1"
& "$PSScriptRoot\log-start-stage.ps1" $stage

& $Env:awscli s3 cp "$Env:output_dir\dotnetlambda21.zip" "s3://$Env:CODE_BUCKET_NAME/$Env:dotnet21functionPackage"
& $Env:awscli s3 cp "$Env:output_dir\dotnetlambda21withef.zip" "s3://$Env:CODE_BUCKET_NAME/$Env:dotnet21withEfFunctionPackage"
& $Env:awscli s3 cp "$Env:output_dir\dotnetlambda30withef.zip" "s3://$Env:CODE_BUCKET_NAME/$Env:dotnet30withEfFunctionPackage"

& "$PSScriptRoot\log-finish-stage.ps1" $stage