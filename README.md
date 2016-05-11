# TfsMigrationUtility
A free custom opensource utility to migrate a Team foundation server project (or collection) from A to B, written in only C#.
# Releases#
See https://github.com/muziekklas/TfsMigrationUtility/releases for current releases.
*Currently there is a beta/preview version of the utility.*
There is an issue when migrating to VSTS: Branches are shown as Folders. There is a workarround (see issue).
# Goals

- Migrate a project, including history, to another server and/or collection (OK)
- Option to migrate the whole collection (OK, You select which project from the collection)
- Migration logic with abstraction (OK)
- A WPF app (OK)
- A console application (TODO)
- TFS Support: 2013, 2015 (OK), VSTS (OK)
- Migrate shelvesets (Prototyping)
- Auto create projects option, if they don't exist (On hold)
# Collaboration
- You can submit Issues with the application via the issues tab.
- You can request a feature via the Issues tab
- You can change code and commit. I will evaluate the change.
- You can join the collaboration team and assist me with this project (help is welcome, always!)
- 
# Documentation sources
- MSDN: TFS C# API documentation
- http://blog.jessehouwing.nl/2015/11/work-around-now-commercial-features-of.html
- https://blogs.msdn.microsoft.com/granth/2010/02/27/tfs2010-create-a-new-team-project-collection-from-powershell-and-c/
- http://stackoverflow.com/questions/213873/forcing-a-tfs-checkin-of-a-file-via-c-sharp
- https://pstfs.codeplex.com/SourceControl/latest#Development/4.0/PsTFS.CmdLet/Methodes/Methode.cs (to create project)
- *More comming soon*

*Big Thanks to all who helped me directly by showing me the light or indirectly by posting these blogs and stackoverflows!*

#Used technologies/Resources
- Microsoft Unity 4
- Microsoft WPF
- Microsoft Team Foundation Server C# API
- Google Material Design Icons: https://design.google.com/icons/index.html

# Working prototype
https://github.com/muziekklas/TfsMigrationUtility.Prototype

#Testing#
##Successful##
- Migrates correctly the history and branch hierarchy
- Migrates 1 or 2 projects
- Migrates TFS 2015 (express) to TFS 2015 (express)
- Migrates TFS 2015 (express) to VSTS
 
##Planned##
- Migrate TFS 2013 to VSTS
- Migrate after a Branch-to-branch merge
- Test new Progress indicator
- Test why the WriteDebug is not working


