$Env:cdk = "cdk"
$Env:npm = "npm"
$Env:src_dir = "$PSScriptRoot\..\src"
$Env:output_dir = "$PSScriptRoot\..\src\output"
$Env:stack_dir = "$PSScriptRoot\..\src\demo-stack"
$Env:CODE_BUCKET_NAME = "malaga-serverless-net-demo"
if ($Env:code_version -ne $null)
{
    $Env:dotnet21functionPackage = "code/dotnetlambda21-$Env:code_version.zip"
    $Env:dotnet30functionPackage = "code/dotnetlambda30-$Env:code_version.zip"
}
else
{
    $Env:dotnet21functionPackage = "code/dotnetlambda21.zip"
    $Env:dotnet30functionPackage = "code/dotnetlambda30.zip"
}