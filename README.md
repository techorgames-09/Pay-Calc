**<h1>PAYCALC</h1>**

**<h2>A super simple prorgam to calculate weekly pay</h2>**

**<h3>How it Works:</h3>**

PayCalc works by the user editing the _config.json_ (or _config.txt_ on windows) file with information on pay, including their hourly rate, any loading bonuses (for casual workers), and a laundry allowances.
If there is another allowance you have, you can add it by editing the code easily enough, however for the timebeing I will not be including a guide, as I will soon create an easier way to do this.
When you open the program, it will read the config file and from it determine the info you gave it, before then getting you to input what shifts you had that week/what shifts you are going to have that week (by hour, for example you could put in working on Monday from 11:00-16:00), and it will then calculate how much you will be paid for your shifts.

_NOTE: THIS PROGRAM CALCULATES AN ESTIMATE OF YOUR PAY, RESULTS MAY BE INACCURATE FOR A LARGE VARIETY OF REASONS._
_NOTE: AI WAS USED TO HELP CREATE THIS PROGRAM. DO WITH THAT INFORMATION WHAT YOU WILL. PLEASE DON'T HATE ME_

**<h3>FLAGS (In terminal - LINUX ONLY):</h3>**

--skip-install: Skips the initial update command and python3 install _(on debian linux ONLY)_

--gui: Launches the GUI version of the application

--cli: Launches the CLI version of the application


**<h3>INSTALLING:</h3>**


**LINUX**

Supported distros: Anything ubuntu/debian based should work. No support for Arch-Based distros as of yet, but will be coming.

STEP 1: Clone this repository.

STEP 2: cd into the repository and run ./paycalc.sh


**WINDOWS:**
_Note: PayCalc on Windows is now coded entirely seperately to the macOS and Linux versions, as it is now based on Visual Basic._

STEP 1: Download the Installer file from the Releases tab.

STEP 2: Open the installer wizard, install, and you're done!

**macOS:**

_Python3 is a REQUIREMENT for this application, please make sure it is installed._

STEP 1: Download/Clone this repository

STEP 2: run paycalc-macOS.command in a terminal window (You may have to edit the properties to make it executable.)

_NOTE: AN EXECUTABLE VERSION OF THIS PROGRAM WILL SOON BE AVAILABLE AND THIS WILL BECOME LEGACY/BUILDING FROM SOURCE CODE._
