#!/usr/bin/env node
import cdk = require('@aws-cdk/core');
import { DemoStack } from './demo-stack';
import { DemoStackProps } from './demo-stack-props';

const app = new cdk.App();

const stackProps: DemoStackProps = {
    bucketName: process.env.CODE_BUCKET_NAME!,
    dotnet21FunctionPackage: process.env.dotnet21functionPackage,
    dotnet21WithEfFunctionPackage: process.env.dotnet21withEfFunctionPackage,
    dotnet30WithEfFunctionPackage: process.env.dotnet30withEfFunctionPackage
};

new DemoStack(app, 'DemoStack', stackProps, { env: { account: process.env.AWS_ACCOUNT, region: process.env.AWS_REGION } });