# Wuh Eatz
Wuh Loves To Eatz!

## Components doko??
This project uses another project called WizWebComponents, which is a project I've made to create a re-useable set of Blazor web components.
Get it [here](https://github.com/BasicallyWiz/WizWebComponents)

```
ProjectsFolder/
    WizWebComponents/
      WizWebComponents.sln
      WizWebComponents.csproj
    WuhEatz/
      WuhEatz.sln
      WuhEatz/
        WuhEatz.csproj
```
By default, the project structure should look similar to this, with both projects having their root folder in the same location.

This can, however be easily changed by setting a new path in the WuhEatz project file.

## I'd Like to contribute, but I don't know where to start...
If you're using Visual Studio, open the Tasks view, and try wokring on some tasks.

The Tasks view searches the entire solution for comments that start with `//TODO`, which are tasks that need to be completed.
That's a good place to start. Then, when you're more comfortable, you can work on issues, or add features you'd like to see implemented.

## Introduction to the file-structure
The folders might seem daunting at first, but let me lay them out for you, you'll get it in no time.
### wwwroot
This folder is the content folder, where files like images, videos, audio, and styles are contained. you can navigate to the files
using url navigation: `wuheatz.ca/images/wuh.webp` == `wwwroot/images/wuh.webp`
### Components
This is the folder that holds all blazor-written components. These include basic components, as well as entire pages. (Pages are just components, with an `@page ""` directive)
### Controllers
This folder contains all of the API controllers.
### Services
This folder contains all of the services that run behind the serverr, such as the Twitch api client.

## If you do contribute, no matter how small, add your name to the credits.razor page!

## Planned Features:
### Database integration
This should allow for many more features down the road that require data to be recorded.

Would probably be best to use MariaDB? (WuhEatz is being run on a raspberry pi. I've already tried Mongo) Use this [EFCore Package](https://www.nuget.org/packages/Pomelo.EntityFrameworkCore.MySql) for implementation.

### Denpa Archive
This will be a library of images and other media (or something) that can be used for archiving art and media for and by Denpa.

