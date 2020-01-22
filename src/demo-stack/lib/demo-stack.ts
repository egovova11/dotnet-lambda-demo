import cdk = require('@aws-cdk/core');
import s3 = require('@aws-cdk/aws-s3');
import lambda = require('@aws-cdk/aws-lambda');
import { DemoStackProps } from './demo-stack-props';
import { CfnParameter } from '@aws-cdk/core';
import { } from "@aws-cdk/aws-rds";

export class DemoStack extends cdk.Stack {
  constructor(scope: cdk.App, id: string, appProps: DemoStackProps, props?: cdk.StackProps) {
    super(scope, id, props);

    const ssmParameters = {
      "AWS:SSM:Region": "eu-central-1",
      "AWS:SSM:Path": "/malaga-serverless-net-demo/vars/"
    }

    const codeBucketNameParameterId = `${id}-codebucket-parameter`
    const codeBucketNameParameter = new CfnParameter(this, codeBucketNameParameterId, {
      default: appProps.bucketName
    });

    const codeBucketId = `${id}-codebucket`;
    const codeBucketArn = `arn:aws:s3:::${codeBucketNameParameter.value}`;
    const codeBucket = s3.Bucket.fromBucketArn(this, codeBucketId, codeBucketArn);

    const dotnet21CodeKey = appProps.dotnet21FunctionPackage 
      ? appProps.dotnet21FunctionPackage
      : "code/dotnetlambda21.zip";

    const dotnet21FunctionId = `${id}-dotnet21function`;
    const dotnet21Function = new lambda.Function(this, dotnet21FunctionId, {
      functionName: 'dotnet21-hello-world-function',
      runtime: lambda.Runtime.DOTNET_CORE_2_1,
      code: lambda.Code.fromBucket(codeBucket, dotnet21CodeKey),
      handler: 'DotnetLambda21::DotnetLambda21.Function::FunctionHandler'
    }); 

    const dotnet21WithEfCodeKey = appProps.dotnet21WithEfFunctionPackage 
      ? appProps.dotnet21WithEfFunctionPackage
      : "code/dotnetlambda21withef.zip";

    const dotnet21WithEfFunctionId = `${id}-dotnet21witheffunction`;
    const dotnet21WithEfFunction = new lambda.Function(this, dotnet21WithEfFunctionId, {
      functionName: 'dotnet21-with-ef-function',
      runtime: lambda.Runtime.DOTNET_CORE_2_1,
      code: lambda.Code.fromBucket(codeBucket, dotnet21WithEfCodeKey),
      handler: 'DotnetLambda21WithEf::DotnetLambda21WithEf.Function::FunctionHandler',
      environment: {
        ...ssmParameters
      }
    });

    const dotnet30WithEfCodeKey = appProps.dotnet30WithEfFunctionPackage 
      ? appProps.dotnet30WithEfFunctionPackage
      : "code/dotnetlambda30withef.zip";

    const dotnet30WithEfFunctionId = `${id}-dotnet30witheffunction`;
    const dotnet30WithEfFunction = new lambda.Function(this, dotnet30WithEfFunctionId, {
      functionName: 'dotnet30-with-ef-function',      
      code: lambda.Code.fromBucket(codeBucket, dotnet30WithEfCodeKey),
      handler: 'DotnetLambda30WithEf::DotnetLambda30WithEf.Function::FunctionHandler',
      runtime: lambda.Runtime.PROVIDED,
      environment: {
        ...ssmParameters
      }
    });
  }
}
