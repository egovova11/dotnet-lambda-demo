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

    const dotnet21FunctionId = `${id}-myfunction`;
    const dotnet21Function = new lambda.Function(this, dotnet21FunctionId, {
      functionName: 'dotnet21-hello-world-function',
      runtime: lambda.Runtime.DOTNET_CORE_2_1,
      code: lambda.Code.fromBucket(codeBucket, dotnet21CodeKey),
      handler: 'DotnetLambda21::DotnetLambda21.Function::FunctionHandler'
    })
  }
}
