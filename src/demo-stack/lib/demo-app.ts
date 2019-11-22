#!/usr/bin/env node
import cdk = require('@aws-cdk/core');
import { DemoDotnet21Stack } from './demo-dotnet21-stack';

const app = new cdk.App();

const codeBucketName : string = process.env.CODE_BUCKET_NAME!;
const dotnet21functionPackage = process.env.dotnet21functionPackage;
const dotnet30functionPackage = process.env.dotnet30functionPackage;

const appProps = {bucketName: codeBucketName, dotnet21functionPackage, dotnet30functionPackage};

new DemoDotnet21Stack(app, 'DemoDotnet21Stack', appProps);