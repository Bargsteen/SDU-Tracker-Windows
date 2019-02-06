# SDU Tracker Windows
Initially created by Kasper Dissing Bargsteen - kasper@bargsteen.com for the Health department at SDU.

## Notes

### Building the installers
- Set the build type to `Release`
- Build the project
- Build the `TrackerInstaller` project
- The installer is now located in `TrackerInstaller/Release`


### Tests
The tests for the database cannot currently be executed due to a change in the Persistence layer. The change was needed to alleviate problems during installation. Further investigation required.

### Versions
The version of the application is changed in the `AssemblyInfo.cs` file. The installer postfix is added manually.