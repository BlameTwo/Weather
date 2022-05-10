Dim WshShell
Set WshShell = CreateObject("Wscript.Shell")
'WshShell.SendKeys "^%{RIGHT}"
key = chr(&h88B3)
WshShell.Sendkeys key