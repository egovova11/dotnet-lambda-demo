#!/usr/bin/env bash

AWS_ACCOUNT=123456789012
AWS_REGION=us-east-1
export AWS_ACCESS_KEY_ID=foo
export AWS_SECRET_ACCESS_KEY=foo

until awslocal ssm describe-parameters
do
  echo "Waiting for localstack to initialize..."
  sleep 2
done

awslocal ssm put-parameter --name "lambda-params/DbConnectionString" --value "Server=host.docker.internal,8085;Initial Catalog=AdventureWorksLT2017;User Id=sa;Password=PaSSw0rd;" --type String