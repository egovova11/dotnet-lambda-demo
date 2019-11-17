#!/usr/bin/env node
import cdk = require('@aws-cdk/core');
import { DemoDotnet21Stack } from './demo-dotnet21-stack';

const app = new cdk.App();

const codeBucketName : string = process.env.CODE_BUCKET_NAME!;
const dotnet21functionPackage = process.env.dotnet21functionPackage;
new DemoDotnet21Stack(app, 'DemoDotnet21Stack', {bucketName: codeBucketName, dotnet21functionPackage});