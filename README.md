# SDU Tracker Windows
Initially created by Kasper Dissing Bargsteen - kasper@bargsteen.com for the Health department at SDU.

ActivityTracker tracks the computer usage of participants in a [IT-health research study by Syddansk University](https://www.researchgate.net/publication/340106467_Short-term_efficacy_of_reducing_screen_media_use_on_physical_activity_sleep_and_physiological_stress_in_families_with_children_aged_4-14_study_protocol_for_the_SCREENS_randomized_controlled_trial).

It has the following features:
   - Tracking of user sessions, i.e. when a user starts or ends work fx by logging in or out
   - Tracking of app-specific usage, i.e. which apps are in focus and when
   - Uploads all logged data to a SDU database
   - Handles and differentiates between multiple users per computer (even with just one system user)
   - Can be setup using a link or QR-code, which also determines the date for automatic termination
      - The researchers send out invite links to the participants, which sets up their ID, type of tracking, and termination date in ActivityTracker
      - [The links are generated with this webapp](https://github.com/Bargsteen/SDU-Tracker-Setup)
   - Automatically terminates tracking at a given date

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
