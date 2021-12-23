# pull down the image from docker-hub , it is used for build the main part of our application
# empty container, it is used for build the project
# get base SDK Image from microsoft
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env 
WORKDIR /app

# what dependencies we actually need to work with in order to build the image
# copy the csproj file from pc into the working container
COPY *.csproj ./

# pulling down the dependencies / 'packages' that we need to build the image
# run restore to resolve any dependencies (via nuget?)
RUN dotnet restore 

# copy the rest of the files into the container/ into the working directory.
COPY . ./

# Release : configuration flag ; 
# output of the publish is placed in folder 'out'
RUN dotnet publish -c Release -o out

# generate runtime image
# build runtime version
# containers are to be/has to be small and efficient , we are gonna just use the runtime version inside the container
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
# called multi-part building, combining parts from the previous build steps and putting it into the working directory
COPY --from=build-env /app/out .

# the image is running in this point, 'what do we want to run,basically.'
# how do we want the container to start , a container starts what is it going to do; 
# in our case it uses the dotnet command to run our dll which is our app
ENTRYPOINT [ "dotnet" , "PlatformService.dll" ]