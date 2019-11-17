import cdk = require('@aws-cdk/core');
import s3 = require('@aws-cdk/aws-s3');
import lambda = require('@aws-cdk/aws-lambda');
import { DemoStackProps } from './demo-stack-props';

export class DemoDotnet21Stack extends cdk.Stack {
  constructor(scope: cdk.App, id: string, appProps: DemoStackProps, props?: cdk.StackProps) {
    super(scope, id, props);

    const codeBucketId = `${id}-codebucket`;
    const codeBucketArn = `arn:aws:s3:::${appProps.bucketName}`;
    const codeBucket = s3.Bucket.fromBucketArn(this, codeBucketId, codeBucketArn);

    const myFunctionId = `${id}-myfunction`;
    const myFunction = new lambda.Function(this, myFunctionId, {
      functionName: 'node8-hello-world-function',
      runtime: lambda.Runtime.NODEJS_8_10,
      code: lambda.Code.fromInline(`exports.handler = async function(event, ctx) { return { body:JSON.stringify("hello world!"), statusCode: 200}; }`),
      handler: 'index.handler'
    })
  }
}
