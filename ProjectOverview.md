# About this page #
This should help to give a overview over the whole project. For me while I'm writing it and for everybody who want to work on Pandoras Box 3 in future. But it should also help to clean up the project a bit. It is very confusing where to find which parts. Our goal is not only to improve pandora but also to refactor it.

# Projects #
The list represents the Directory structure. Every directory contains one project of the whole solution to name it by Visual Studio names.

## Root ##
Here we have the "Pandora 3.0.sln" which can be opened with Visual Studio to compile Pandoras box from scratch.  The compile button should work on every PC. Otherwise you can post it as an issue.
The output will be generated in the directory "./Build/Release" and "./Build/Debug"

## ArtViewer ##

## BoxCapture ##

## BoxCommonLibrary ##
BoxCL.dll

Contains some basic parts of Pandoras Box, like the travel system, network code, hues and more.

## BoxEdit ##

## BoxRemote ##

## BoxServerSetup ##

## Data ##

## Datafiles ##

## Documentation ##

## Images ##

## Localizer ##

## log4net ##

## MapViewer ##

## Pandora ##
This is the Main project. Which contains the GUI and control logic of Pandoras Box 3

## SoundExplorer ##

## Tester ##
Just a little test project. Free to use for any tests.

## TravelAgent ##
Dedicated Travel tool. Using the BoxCL.dll (BoxCommonLibrary )