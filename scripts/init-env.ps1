$Env:cdk = "cdk"
$Env:npm = "npm"
$Env:awscli = "aws"
$Env:src_dir = "$PSScriptRoot\..\src"
$Env:output_dir = "$PSScriptRoot\..\src\output"
$Env:stack_dir = "$PSScriptRoot\..\src\demo-stack"
$Env:CODE_BUCKET_NAME = "malaga-serverless-net-demo"
$Env:AWS_ACCOUNT = "805763676908"
$Env:AWS_REGION = "eu-central-1"
if ($Env:code_version -ne $null)
{
    $Env:dotnet21functionPackage = "code/dotnetlambda21-$Env:code_version.zip"
    $Env:dotnet21withEfFunctionPackage = "code/dotnetlambda21withef-$Env:code_version.zip"
    $Env:dotnet30withEfFunctionPackage = "code/dotnetlambda30withef-$Env:code_version.zip"
}
else
{
    $Env:dotnet21functionPackage = "code/dotnetlambda21.zip"
    $Env:dotnet21withEfFunctionPackage = "code/dotnetlambda21withef.zip"
    $Env:dotnet30withEfFunctionPackage = "code/dotnetlambda30withef.zip"
}