#!/usr/bin/env node
import cdk = require('@aws-cdk/core');
import { DemoDotnet21Stack } from './demo-dotnet21-stack';
import { DemoStackProps } from './demo-stack-props';

const app = new cdk.App();

const stackProps : DemoStackProps = {
    bucketName: process.env.CODE_BUCKET_NAME!, 
    dotnet21FunctionPackage: process.env.dotnet21functionPackage, 
    dotnet30FunctionPackage: process.env.dotnet30functionPackage,
    dotnet21WithEfFunctionPackage: process.env.dotnet21withEfFunctionPackage,
    dotnet30WithEfFunctionPackage: process.env.dotnet30withEfFunctionPackage
};

new DemoDotnet21Stack(app, 'DemoDotnet21Stack', stackProps);