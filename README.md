# BrowserChooser
Browser Chooser is a small utility made in .NET that when set as the default browser inside Windows Settings, prompt you for which browser to use to open any link you open outside of a browser.

### Supported Opeating Systems:

- Windows 10
- Windows 8.1
- Windows 7 (on Windows 7 the Windows API used for selecting the browser doesn't actually show a list of all browsers but a list of all programs installed on the PC)

### Known Issues

- Browser Chooser doesn't show in the Uninstall a program list inside Windows Settings or Windows Control Panel
- Right now the v0.1a pre-release doesn't actually download Uninstall.exe so you can't uninstall the program if you install it 
  - A workaround for this would be downloading Uninstall.exe from the files inside the development branch, but I would assure that also won't be broken
- Right now the v0.1a pre-release doesn't work on Windows 7 (BrowserChooser.exe actually work, it'a problem with the way the Installer works in this version, will be fixed in the next version)
