#!/bin/bash

projectName='Yozian.Validation'
version=$1

if [ "$version" == "" ];then
   echo "version should be provided!"
   exit;
fi

commit=`git rev-parse --short HEAD`

echo "pack with commit: $commit"

# change commit hash
sed -i -e "s/commit=\"*\"/commit=\"$commit\"/g" nuget/$projectName.nuspec

cd nuget

nuget pack -version $1

cd ..


# recover
git checkout nuget/$projectName.nuspec

