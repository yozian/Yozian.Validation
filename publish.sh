#!/bin/bash
version=$1
projectName='Yozian.Validation'
cd nuget

if [ "$version" == "" ];then
   echo "version should be provided!"
   exit;
fi

nuget push $projectName.$1.nupkg  -source https://api.nuget.org/v3/index.json

cd ..