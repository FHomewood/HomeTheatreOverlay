Dim WSHShell
Set WSHShell = WScript.CreateObject("WScript.Shell")
WSHShell.SendKeys "^{ESC}"
Set WSHShell = Nothing
WScript.Quit(0)