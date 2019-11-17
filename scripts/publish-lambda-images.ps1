& "$PSScriptRoot\init-env.ps1"

aws2 s3 cp "$Env:output_dir\dotnetlambda21.zip" "s3://$Env:CODE_BUCKET_NAME/$Env:dotnet21functionPackage"