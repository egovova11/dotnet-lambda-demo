import cdk = require('@aws-cdk/core');
import s3 = require('@aws-cdk/aws-s3');
import lambda = require('@aws-cdk/aws-lambda');
import { DemoStackProps } from './demo-stack-props';
import { CfnParameter } from '@aws-cdk/core';

export class DemoDotnet21Stack extends cdk.Stack {
  constructor(scope: cdk.App, id: string, appProps: DemoStackProps, props?: cdk.StackProps) {
    super(scope, id, props);

    const codeBucketNameParameterId = `${id}-codebucket-parameter`
    const codeBucketNameParameter = new CfnParameter(this, codeBucketNameParameterId, {
      default: appProps.bucketName
    });

    const codeBucketId = `${id}-codebucket`;
    const codeBucketArn = `arn:aws:s3:::${codeBucketNameParameter.value}`;
    const codeBucket = s3.Bucket.fromBucketArn(this, codeBucketId, codeBucketArn);

    const dotnet21CodeKey = appProps.dotnet21functionPackage 
      ? appProps.dotnet21functionPackage
      : "code/dotnetlambda21.zip";

    const dotnet21FunctionId = `${id}-dotnet21function`;
    const dotnet21Function = new lambda.Function(this, dotnet21FunctionId, {
      functionName: 'dotnet21-hello-world-function',
      runtime: lambda.Runtime.DOTNET_CORE_2_1,
      code: lambda.Code.fromBucket(codeBucket, dotnet21CodeKey),
      handler: 'DotnetLambda21::DotnetLambda21.Function::FunctionHandler'
    });

    const layerId = `${id}-layer`
    const layer = lambda.LayerVersion.fromLayerVersionArn(this, layerId, "arn:aws:lambda:eu-central-1:805763676908:layer:common-core:1");


    const dotnet30CodeKey = appProps.dotnet30functionPackage 
      ? appProps.dotnet30functionPackage
      : "code/dotnetlambda30.zip";

    const dotnet30FunctionId = `${id}-dotnet30function`;
    const dotnet30Function = new lambda.Function(this, dotnet30FunctionId, {
      functionName: 'dotnet30-hello-world-function',      
      code: lambda.Code.fromBucket(codeBucket, dotnet30CodeKey),
      handler: 'DotnetLambda30::DotnetLambda30.Function::FunctionHandler',
      runtime: lambda.Runtime.PROVIDED,
      layers: [layer]
    });
  }
}
