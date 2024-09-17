

```bash
mkdir FullStackPoC
cd FullStackPoC

dotnet new sln --name FullStackPoC
dotnet new webapi -o Backend
dotnet sln add .\Backend\
dotnet new gitignore

cd ..
git init
git add .
git commit -m "Init"
git remote add origin "https://github.com/BenTaylor25/APIAuthTest.git"
git push origin master
```
```bash
cd APIAuthTest

dotnet add package ErrorOr --version 0.1.0
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```
