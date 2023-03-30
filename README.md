# Introduction 
KnowledgeBase is a open source project for everyone. Every member can create new knowledge base record (KB) and share to community.
For each KnowledgeBase, user can vote and comment to below KnowledgeBase.

# Migration
- Add-Migration Initial -OutputDir Data/Migrations


# Technology Stack
1. ASP.NET Core 6.0
2. Angular 13
3. Identity Server 4
4. SQL Server 2019

# How to run this project
1. Clone this source code from Repository
2. Build solution to restore all Nuget Packages
3. Set startup project is KnowledgeSpace.BackendServer
4. Run Update-Database to generate database
5. Set startup project to multiple projects include: KnowledgeSpace.BackendServer and KnowledgeSpace.WebPortal

# Build and Test
TODO: Describe and show how to build your code and run the tests. 

# References
TODO: Explain how other users and developers can contribute to make your code better. 
- [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/getting-started/?view=aspnetcore-7.0)
- [Visual Studio](https://visualstudio.microsoft.com/vs/)
- [Identity Server 4](https://identityserver.io/)
- [Angular](https://angular.io/docs)